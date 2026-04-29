#!/usr/bin/env python3
"""
Wrapper local para o decoder do kit LF2 (uso humano/debug).
"""
from pathlib import Path
import runpy
import sys


def main() -> int:
    root = Path(__file__).resolve().parents[1]
    target = root / "doc" / "ArquivosOpus" / "completos" / "files" / "lf2_unity_kit_full" / "tools" / "decode_lf2_dat.py"
    if not target.is_file():
        print(f"Script nao encontrado: {target}", file=sys.stderr)
        return 1
    runpy.run_path(str(target), run_name="__main__")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
