#!/usr/bin/env python3
"""
decode_lf2_dat.py

Decoder for Little Fighter 2 .dat files (encrypted character/object data).
This is for HUMAN INSPECTION ONLY. We do not parse .dat files at runtime — see
lf2_assets_guide.md §5.3 for why.

Algorithm:
    1. Strip 123 bytes from the start AND 123 bytes from the end of the file.
    2. For each remaining byte at position i, decoded = (byte - KEY[i % 32]) mod 256.

Usage:
    # Print to stdout:
    python decode_lf2_dat.py LittleFighter/data/bandit.dat

    # Save to file:
    python decode_lf2_dat.py LittleFighter/data/bandit.dat -o bandit.txt

    # Just dump the first frame block (text after first <frame>):
    python decode_lf2_dat.py LittleFighter/data/bandit.dat --grep '<frame>' -A 10

WARNING: the decoder is imperfect — some bytes come out as garbage even on a
correct decode (the format itself contains binary fields mixed with text).
You'll see readable section markers (<bmp_begin>, <frame>, name:, pic:, wait:,
etc.) and readable strings, with garbage in between. That's expected — read
the markers, ignore the rest. DO NOT try to feed this output into a parser.
"""
from __future__ import annotations

import argparse
import re
import sys
from pathlib import Path

KEY    = b"odBearBecauseHeIsVeryGoodSiuMan!"
HEADER = 123  # bytes stripped from each end


def decode(path: Path) -> str:
    """Return the decoded text. Errors map to U+FFFD."""
    data = path.read_bytes()
    if len(data) < 2 * HEADER:
        raise ValueError(f"file too small ({len(data)} bytes)")
    inner = data[HEADER:-HEADER]
    out = bytearray(len(inner))
    for i, b in enumerate(inner):
        out[i] = (b - KEY[i % len(KEY)]) & 0xFF
    return out.decode("latin-1", errors="replace")


def grep_context(text: str, pattern: str, after: int) -> str:
    """grep -A <after> equivalent on the decoded text."""
    rx = re.compile(pattern)
    lines = text.splitlines()
    keep: set[int] = set()
    for i, line in enumerate(lines):
        if rx.search(line):
            for j in range(i, min(len(lines), i + after + 1)):
                keep.add(j)
    return "\n".join(lines[i] for i in sorted(keep))


def main() -> int:
    ap = argparse.ArgumentParser(
        description="Decode an LF2 .dat file for human inspection.",
        formatter_class=argparse.RawDescriptionHelpFormatter,
        epilog="See lf2_assets_guide.md §5 for what to do with the output "
               "(spoiler: read it, don't parse it).",
    )
    ap.add_argument("file", type=Path, help="Path to a .dat file.")
    ap.add_argument("-o", "--output", type=Path,
                    help="Write to file instead of stdout.")
    ap.add_argument("--grep", type=str,
                    help="Only show lines matching this regex (and -A lines after).")
    ap.add_argument("-A", "--after", type=int, default=5,
                    help="Lines of context after each --grep match. Default: 5.")
    args = ap.parse_args()

    if not args.file.is_file():
        print(f"error: {args.file} not found", file=sys.stderr)
        return 1

    try:
        text = decode(args.file)
    except Exception as e:
        print(f"error decoding {args.file}: {e}", file=sys.stderr)
        return 1

    if args.grep:
        text = grep_context(text, args.grep, args.after)

    if args.output:
        args.output.write_text(text, encoding="utf-8", errors="replace")
        print(f"wrote {len(text)} chars to {args.output}", file=sys.stderr)
    else:
        sys.stdout.write(text)

    return 0


if __name__ == "__main__":
    sys.exit(main())
