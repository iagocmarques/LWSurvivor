#!/usr/bin/env python3
from __future__ import annotations

from pathlib import Path
from PIL import Image

ROOT = Path(__file__).resolve().parents[2]
LF2_DIR = ROOT / "Assets" / "_Project" / "Resources" / "LF2"


def convert_with_key(src: Path, dst: Path, chroma: tuple[int, int, int]) -> None:
    img = Image.open(src).convert("RGBA")
    pix = img.load()
    w, h = img.size
    cr, cg, cb = chroma
    for y in range(h):
        for x in range(w):
            r, g, b, _ = pix[x, y]
            if r == cr and g == cg and b == cb:
                pix[x, y] = (0, 0, 0, 0)
    img.save(dst, "PNG", optimize=True)


def top_left_key(src: Path) -> tuple[int, int, int]:
    img = Image.open(src).convert("RGB")
    return img.getpixel((0, 0))


def main() -> int:
    if not LF2_DIR.is_dir():
        raise SystemExit(f"Pasta nao encontrada: {LF2_DIR}")

    converted = 0
    for bmp in sorted(LF2_DIR.glob("*.bmp")):
        stem = bmp.stem
        dst = bmp.with_name(stem + "_alpha.png")
        name = stem.lower()

        if name.startswith("bg_"):
            convert_with_key(bmp, dst, top_left_key(bmp))
        else:
            # LF2 character sheets and effects: black chroma key.
            convert_with_key(bmp, dst, (0, 0, 0))
        converted += 1

    print(f"Convertidos: {converted} arquivo(s) para *_alpha.png em {LF2_DIR}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
