#!/usr/bin/env python3
"""
prepare_lf2_assets.py

Converts a LittleFighter/ extracted folder into a Unity-ready asset tree:
- Character sprite sheets: BMP -> PNG with alpha (black = transparent)
- Mirror sprites: skipped (Unity flips at runtime)
- Face/icon sprites: BMP -> PNG without alpha (full images)
- Background tiles: BMP -> PNG with heuristic chroma key (top-left pixel)
- Sound effects: copied as-is

Usage:
    python prepare_lf2_assets.py --src LittleFighter/ --dst Assets/Art/lf2_ref/

Requires: Pillow (`pip install Pillow`)

Author: project tooling. Run once per LF2 update.
"""
from __future__ import annotations

import argparse
import json
import shutil
import sys
from collections import Counter
from pathlib import Path

try:
    from PIL import Image
except ImportError:
    sys.exit("Pillow is required. Install with: pip install Pillow")


# Convention from the LF2 asset analysis (see lf2_assets_guide.md §2.5 and §B):
# Character sprite sheets always use pure black as the transparent chroma key.
CHARACTER_CHROMA = (0, 0, 0)

# Files we explicitly skip (mirrored sheets, system/UI sprites we don't need).
SKIP_PATTERNS = ("_mirror.bmp",)
# Files we never want (LF2 ad banners, never used in our game).
SKIP_PREFIXES = ("ad0", "ad1")


# ---------- helpers ----------

def is_full_image(name: str) -> bool:
    """Faces (_f) and small icons (_s) are full RGB images, no transparency."""
    stem = Path(name).stem.lower()
    return stem.endswith("_f") or stem.endswith("_s") or stem == "face"


def detect_chroma_from_corners(img: Image.Image) -> tuple[int, int, int] | None:
    """For backgrounds: vote among the 4 corner pixels. If 2+ agree, that's the key."""
    rgb = img.convert("RGB")
    w, h = rgb.size
    corners = [
        rgb.getpixel((0, 0)),
        rgb.getpixel((w - 1, 0)),
        rgb.getpixel((0, h - 1)),
        rgb.getpixel((w - 1, h - 1)),
    ]
    most_common, count = Counter(corners).most_common(1)[0]
    return most_common if count >= 2 else None


def convert_with_chroma(src: Path, dst: Path, chroma: tuple[int, int, int]) -> None:
    """Convert a BMP to RGBA PNG, mapping `chroma` color to alpha=0."""
    img = Image.open(src).convert("RGBA")
    px = img.load()
    w, h = img.size
    cr, cg, cb = chroma
    for y in range(h):
        for x in range(w):
            r, g, b, _ = px[x, y]
            if r == cr and g == cg and b == cb:
                px[x, y] = (0, 0, 0, 0)
    dst.parent.mkdir(parents=True, exist_ok=True)
    img.save(dst, "PNG", optimize=True)


def copy_full_image(src: Path, dst: Path) -> None:
    """Copy a BMP to PNG without modifying alpha (face/icon)."""
    img = Image.open(src).convert("RGB")
    dst.parent.mkdir(parents=True, exist_ok=True)
    img.save(dst, "PNG", optimize=True)


# ---------- conversion logic per category ----------

def convert_characters(src_root: Path, dst_root: Path, manifest: dict) -> None:
    """sprite/sys/*.bmp -> dst_root/characters/*.png"""
    src_dir = src_root / "sprite" / "sys"
    if not src_dir.is_dir():
        print(f"  [warn] No sprite/sys at {src_dir}")
        return
    dst_dir = dst_root / "characters"
    n_converted = n_skipped = n_failed = 0
    for bmp in sorted(src_dir.glob("*.bmp")):
        if any(p in bmp.name for p in SKIP_PATTERNS):
            n_skipped += 1
            continue
        if any(bmp.stem.startswith(p) for p in SKIP_PREFIXES):
            n_skipped += 1
            continue
        png = dst_dir / (bmp.stem + ".png")
        try:
            if is_full_image(bmp.name):
                copy_full_image(bmp, png)
                kind = "portrait"
            else:
                convert_with_chroma(bmp, png, CHARACTER_CHROMA)
                kind = "spritesheet"
            info = Image.open(png).size
        except Exception as e:
            print(f"  [warn] failed: {bmp.name}: {e}")
            n_failed += 1
            continue
        manifest["characters"].append({
            "src": str(bmp.relative_to(src_root)),
            "dst": str(png.relative_to(dst_root)),
            "kind": kind,
            "size": list(info),
        })
        n_converted += 1
    print(f"  characters: {n_converted} converted, {n_skipped} skipped, {n_failed} failed")


def convert_backgrounds(src_root: Path, dst_root: Path, manifest: dict) -> None:
    """bg/<theme>/<stage>/*.bmp -> dst_root/backgrounds/<theme>/<stage>/*.png"""
    src_dir = src_root / "bg"
    if not src_dir.is_dir():
        print(f"  [warn] No bg/ at {src_dir}")
        return
    n_converted = n_failed = 0
    for bmp in sorted(src_dir.rglob("*.bmp")):
        rel = bmp.relative_to(src_dir)
        png = dst_root / "backgrounds" / rel.with_suffix(".png")
        try:
            img_pil = Image.open(bmp)
            chroma = detect_chroma_from_corners(img_pil)
            if chroma is None:
                copy_full_image(bmp, png)
                kind = "opaque"
            else:
                convert_with_chroma(bmp, png, chroma)
                kind = f"alpha(key=rgb{chroma})"
        except Exception as e:
            print(f"  [warn] failed: {bmp.name}: {e}")
            n_failed += 1
            continue
        manifest["backgrounds"].append({
            "src": str(bmp.relative_to(src_root)),
            "dst": str(png.relative_to(dst_root)),
            "kind": kind,
        })
        n_converted += 1
    print(f"  backgrounds: {n_converted} converted, {n_failed} failed")


def copy_audio(src_root: Path, dst_root: Path, manifest: dict) -> None:
    """data/*.wav -> dst_root/../Audio/lf2_ref/*.wav (sibling of Art/)"""
    src_dir = src_root / "data"
    if not src_dir.is_dir():
        return
    # Audio goes next to Art/ in Unity convention. The user passes
    # --dst Assets/Art/lf2_ref, so audio target is Assets/Audio/lf2_ref.
    audio_dst = dst_root.parent.parent / "Audio" / "lf2_ref"
    audio_dst.mkdir(parents=True, exist_ok=True)
    n = 0
    for wav in sorted(src_dir.glob("*.wav")):
        target = audio_dst / wav.name
        shutil.copy2(wav, target)
        manifest["audio"].append({
            "src": str(wav.relative_to(src_root)),
            "dst": str(target),
        })
        n += 1
    print(f"  audio: {n} files copied to {audio_dst}")


def copy_data_txt(src_root: Path, dst_root: Path, manifest: dict) -> None:
    """data/data.txt -> dst_root/data.txt (the only unencrypted index file)"""
    src = src_root / "data" / "data.txt"
    if not src.is_file():
        return
    dst = dst_root / "data.txt"
    shutil.copy2(src, dst)
    manifest["index_file"] = str(dst.relative_to(dst_root))
    print(f"  data.txt: copied (LF2 object ID -> file mapping)")


# ---------- entrypoint ----------

def main() -> int:
    ap = argparse.ArgumentParser(
        description="Convert LF2 assets to Unity-ready PNGs/WAVs.",
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog=__doc__.split("Usage:")[1] if "Usage:" in __doc__ else "",
    )
    ap.add_argument("--src", required=True, type=Path,
                    help="Path to extracted LittleFighter/ folder.")
    ap.add_argument("--dst", required=True, type=Path,
                    help="Path to Assets/Art/lf2_ref/ inside Unity project.")
    ap.add_argument("--no-audio", action="store_true",
                    help="Skip copying .wav files.")
    args = ap.parse_args()

    src: Path = args.src.resolve()
    dst: Path = args.dst.resolve()

    if not src.is_dir():
        print(f"error: --src {src} is not a directory", file=sys.stderr)
        return 1
    if not (src / "sprite").is_dir() or not (src / "data").is_dir():
        print(f"error: --src {src} doesn't look like a LittleFighter/ folder "
              "(missing sprite/ or data/)", file=sys.stderr)
        return 1

    print(f"Converting LF2 assets:")
    print(f"  src: {src}")
    print(f"  dst: {dst}")
    dst.mkdir(parents=True, exist_ok=True)

    manifest: dict = {
        "source": str(src),
        "characters": [],
        "backgrounds": [],
        "audio": [],
        "index_file": None,
        "convention": {
            "character_chroma_key": list(CHARACTER_CHROMA),
            "spritesheet_grid": {"cols": 10, "rows": 7, "cell_size": 80},
            "pivot": "bottom_center",
            "pixels_per_unit_recommended": 80,
        },
        "skipped_patterns": list(SKIP_PATTERNS),
    }

    convert_characters(src, dst, manifest)
    convert_backgrounds(src, dst, manifest)
    copy_data_txt(src, dst, manifest)
    if not args.no_audio:
        copy_audio(src, dst, manifest)

    manifest_path = dst / "manifest.json"
    manifest_path.write_text(json.dumps(manifest, indent=2))
    print(f"\nDone. Manifest: {manifest_path}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
