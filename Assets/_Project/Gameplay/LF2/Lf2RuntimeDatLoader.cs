using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using LF2Importer;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public static class Lf2RuntimeDatLoader
    {
        public const string Key37 = "odBearBecauseHeIsVeryGoodSiuHungIsAGo";
        public const string Key32 = "odBearBecauseHeIsVeryGoodSiuMan!";

        private static readonly Regex FileLineRx = new Regex(
            @"^\s*file\s*\(\s*(?<a>\d+)\s*-\s*(?<b>\d+)\s*\)\s*:\s*(?<path>\S+)\s+w\s*:\s*(?<w>\d+)\s+h\s*:\s*(?<h>\d+)\s+row\s*:\s*(?<row>\d+)\s+col\s*:\s*(?<col>\d+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex FrameHeaderRx = new Regex(
            @"^\s*<frame>\s*(?<id>\d+)\s+(?<name>.+?)\s*$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex KvRx = new Regex(
            @"\b(?<key>[a-zA-Z_][a-zA-Z0-9_]*)\s*:\s*(?<val>\S+)",
            RegexOptions.Compiled);

        public static Lf2CharacterData LoadFromTextAsset(TextAsset textAsset)
        {
            if (textAsset == null)
            {
                Debug.LogError("[Lf2RuntimeDatLoader] TextAsset is null.");
                return null;
            }

            var raw = textAsset.bytes;
            if (raw == null || raw.Length == 0)
            {
                Debug.LogError("[Lf2RuntimeDatLoader] TextAsset has no bytes.");
                return null;
            }

            string text;
            if (LooksPlaintext(raw))
            {
                text = Encoding.GetEncoding(28591).GetString(raw);
            }
            else
            {
                text = TryDecrypt(raw);
            }

            return ParseToCharacterData(text);
        }

        public static Lf2CharacterData LoadFromText(string text)
        {
            return ParseToCharacterData(text);
        }

        private static bool LooksPlaintext(byte[] headBytes)
        {
            if (headBytes == null || headBytes.Length == 0)
                return false;
            var n = Mathf.Min(headBytes.Length, 4096);
            var s = Encoding.ASCII.GetString(headBytes, 0, n);
            return s.IndexOf("<bmp_begin>", StringComparison.OrdinalIgnoreCase) >= 0
                   || s.IndexOf("<frame>", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static string TryDecrypt(byte[] data)
        {
            var best = "";
            var bestScore = -1;

            TryVariant(data, "kit32", DecryptKitStyle, ref best, ref bestScore);
            TryVariant(data, "key37", DecryptHeadOnlyKey37, ref best, ref bestScore);
            TryVariant(data, "key32head", DecryptHeadOnlyKey32, ref best, ref bestScore);

            if (bestScore < 0)
                best = Encoding.GetEncoding(28591).GetString(data);

            return best;
        }

        private static void TryVariant(byte[] data, string label, Func<byte[], string> fn, ref string best, ref int bestScore)
        {
            try
            {
                var text = fn(data);
                var score = ScoreDecoded(text);
                if (score > bestScore)
                {
                    bestScore = score;
                    best = text;
                }
            }
            catch (Exception)
            {
                // Skip failed variant
            }
        }

        private static int ScoreDecoded(string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;
            var score = 0;
            for (var i = 0; i < text.Length; i++)
            {
                if (i + 10 < text.Length && string.Compare(text, i, "<bmp_begin>", 0, 11, StringComparison.OrdinalIgnoreCase) == 0)
                    score += 100;
                if (i + 6 < text.Length && string.Compare(text, i, "<frame>", 0, 7, StringComparison.OrdinalIgnoreCase) == 0)
                    score += 10;
            }
            return score;
        }

        private static string DecryptKitStyle(byte[] data)
        {
            const int header = 123;
            if (data.Length <= header * 2)
                throw new InvalidOperationException("file too small");
            var innerLen = data.Length - header * 2;
            var key = Encoding.ASCII.GetBytes(Key32);
            var outb = new byte[innerLen];
            for (var i = 0; i < innerLen; i++)
                outb[i] = (byte)((data[header + i] - key[i % key.Length] + 256) % 256);
            return Encoding.GetEncoding(28591).GetString(outb);
        }

        private static string DecryptHeadOnlyKey37(byte[] data)
        {
            const int skip = 123;
            if (data.Length <= skip)
                throw new InvalidOperationException("file too small");
            var innerLen = data.Length - skip;
            var key = Encoding.ASCII.GetBytes(Key37);
            var outb = new byte[innerLen];
            for (var i = 0; i < innerLen; i++)
                outb[i] = (byte)((data[skip + i] - key[i % key.Length] + 256) % 256);
            return Encoding.GetEncoding(28591).GetString(outb);
        }

        private static string DecryptHeadOnlyKey32(byte[] data)
        {
            const int skip = 123;
            if (data.Length <= skip)
                throw new InvalidOperationException("file too small");
            var innerLen = data.Length - skip;
            var key = Encoding.ASCII.GetBytes(Key32);
            var outb = new byte[innerLen];
            for (var i = 0; i < innerLen; i++)
                outb[i] = (byte)((data[skip + i] - key[i % key.Length] + 256) % 256);
            return Encoding.GetEncoding(28591).GetString(outb);
        }

        private static Lf2CharacterData ParseToCharacterData(string text)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            var parsed = ParseDat(text);
            return ConvertToCharacterData(parsed);
        }

        private struct ParsedDat
        {
            public string name;
            public string head;
            public string small;
            public List<ParsedBmpEntry> bmpEntries;
            public Dictionary<string, float> movement;
            public Dictionary<int, ParsedFrame> frames;
        }

        private struct ParsedBmpEntry
        {
            public int start;
            public int end;
            public string path;
            public int cellW;
            public int cellH;
            public int row;
            public int col;
        }

        private struct ParsedFrame
        {
            public int id;
            public string frameName;
            public Dictionary<string, string> props;
            public List<Dictionary<string, string>> itrBlocks;
            public List<Dictionary<string, string>> bdyBlocks;
            public List<Dictionary<string, string>> opointBlocks;
        }

        private static ParsedDat ParseDat(string text)
        {
            var dat = new ParsedDat
            {
                name = "",
                head = "",
                small = "",
                bmpEntries = new List<ParsedBmpEntry>(),
                movement = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase),
                frames = new Dictionary<int, ParsedFrame>()
            };

            var lines = text.Split(new[] { '\n' }, StringSplitOptions.None);
            var i = 0;

            while (i < lines.Length)
            {
                var line = lines[i];
                if (line.IndexOf("<bmp_begin>", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    i = ParseBmpBegin(lines, i, ref dat);
                    continue;
                }

                if (line.TrimStart().StartsWith("<frame>", StringComparison.OrdinalIgnoreCase))
                    break;

                i++;
            }

            while (i < lines.Length)
            {
                var t = lines[i].TrimStart();
                if (t.StartsWith("<frame>", StringComparison.OrdinalIgnoreCase))
                {
                    i = ParseOneFrame(lines, i, ref dat);
                    continue;
                }

                i++;
            }

            return dat;
        }

        private static int ParseBmpBegin(string[] lines, int start, ref ParsedDat dat)
        {
            var i = start + 1;
            for (; i < lines.Length; i++)
            {
                var line = lines[i].Trim();

                if (line.IndexOf("<bmp_end>", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    i++;
                    break;
                }

                if (line.StartsWith("name:", StringComparison.OrdinalIgnoreCase))
                    dat.name = AfterColon(line);
                else if (line.StartsWith("head:", StringComparison.OrdinalIgnoreCase))
                    dat.head = AfterColon(line);
                else if (line.StartsWith("small:", StringComparison.OrdinalIgnoreCase))
                    dat.small = AfterColon(line);
                else
                {
                    var m = FileLineRx.Match(line);
                    if (m.Success)
                    {
                        dat.bmpEntries.Add(new ParsedBmpEntry
                        {
                            start = int.Parse(m.Groups["a"].Value),
                            end = int.Parse(m.Groups["b"].Value),
                            path = m.Groups["path"].Value.Trim().Replace('\\', '/'),
                            cellW = int.Parse(m.Groups["w"].Value),
                            cellH = int.Parse(m.Groups["h"].Value),
                            row = int.Parse(m.Groups["row"].Value),
                            col = int.Parse(m.Groups["col"].Value)
                        });
                    }
                }
            }

            for (; i < lines.Length; i++)
            {
                var line = lines[i].Trim();
                if (line.StartsWith("<frame>", StringComparison.OrdinalIgnoreCase))
                    break;

                if (line.Length == 0 || line.StartsWith("#"))
                    continue;
                if (line.StartsWith("<", StringComparison.Ordinal))
                    continue;

                var spaceIdx = line.IndexOf(' ');
                if (spaceIdx <= 0)
                    continue;
                var key = line.Substring(0, spaceIdx).Trim();
                var rest = line.Substring(spaceIdx).Trim();
                if (float.TryParse(rest, NumberStyles.Float, CultureInfo.InvariantCulture, out var fv))
                    dat.movement[key] = fv;
            }

            return i;
        }

        private static int ParseOneFrame(string[] lines, int start, ref ParsedDat dat)
        {
            var header = lines[start].Trim();
            var mh = FrameHeaderRx.Match(header);
            if (!mh.Success)
                return start + 1;

            var frame = new ParsedFrame
            {
                id = int.Parse(mh.Groups["id"].Value),
                frameName = mh.Groups["name"].Value.Trim(),
                props = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase),
                itrBlocks = new List<Dictionary<string, string>>(),
                bdyBlocks = new List<Dictionary<string, string>>(),
                opointBlocks = new List<Dictionary<string, string>>()
            };

            var i = start + 1;
            for (; i < lines.Length; i++)
            {
                var trimmed = lines[i].Trim();

                if (trimmed.StartsWith("<frame_end>", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    break;
                }

                if (trimmed.Equals("bdy:", StringComparison.OrdinalIgnoreCase))
                {
                    i = ReadBlock(lines, i + 1, "bdy_end:", frame.bdyBlocks);
                    continue;
                }

                if (trimmed.Equals("itr:", StringComparison.OrdinalIgnoreCase))
                {
                    i = ReadBlock(lines, i + 1, "itr_end:", frame.itrBlocks);
                    continue;
                }

                if (trimmed.Equals("opoint:", StringComparison.OrdinalIgnoreCase))
                {
                    i = ReadBlock(lines, i + 1, "opoint_end:", frame.opointBlocks);
                    continue;
                }

                MergePropsLine(frame.props, trimmed);
            }

            dat.frames[frame.id] = frame;
            return i;
        }

        private static int ReadBlock(string[] lines, int start, string endToken, List<Dictionary<string, string>> sink)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            sink.Add(dict);
            var i = start;
            for (; i < lines.Length; i++)
            {
                var t = lines[i].Trim();
                if (t.StartsWith(endToken, StringComparison.OrdinalIgnoreCase))
                    return i;
                MergePropsLine(dict, t);
            }
            return i;
        }

        private static void MergePropsLine(Dictionary<string, string> dict, string line)
        {
            foreach (Match m in KvRx.Matches(line))
            {
                var k = m.Groups["key"].Value;
                var v = m.Groups["val"].Value;
                if (k.Length > 0)
                    dict[k] = v;
            }
        }

        private static string AfterColon(string line)
        {
            var idx = line.IndexOf(':');
            return idx < 0 ? "" : line.Substring(idx + 1).Trim();
        }

        private static Lf2CharacterData ConvertToCharacterData(ParsedDat parsed)
        {
            var character = new Lf2CharacterData
            {
                Name = parsed.name,
                Head = parsed.head,
                Small = parsed.small,
                Movement = new Dictionary<string, float>(parsed.movement, StringComparer.OrdinalIgnoreCase),
                Frames = new Dictionary<int, Lf2FrameData>(),
                BmpEntries = new List<Lf2BmpEntry>()
            };

            foreach (var b in parsed.bmpEntries)
            {
                character.BmpEntries.Add(new Lf2BmpEntry
                {
                    Start = b.start,
                    End = b.end,
                    Path = b.path,
                    CellW = b.cellW,
                    CellH = b.cellH,
                    Row = b.row,
                    Col = b.col
                });
            }

            foreach (var kv in parsed.frames)
            {
                var pf = kv.Value;
                var frame = Lf2FrameDataConverter.ConvertFrame(
                    pf.props,
                    pf.itrBlocks,
                    pf.bdyBlocks,
                    pf.opointBlocks,
                    pf.id,
                    pf.frameName);
                character.Frames[kv.Key] = frame;
            }

            return character;
        }

        public static Lf2CharacterData LoadFromImportedCharacter(ImportedLf2Character imported)
        {
            if (imported == null)
            {
                Debug.LogError("[Lf2RuntimeDatLoader] ImportedLf2Character is null.");
                return null;
            }

            var character = new Lf2CharacterData
            {
                Name = imported.displayName,
                Head = imported.headSpritePath,
                Small = imported.smallSpritePath,
                Movement = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase),
                Frames = new Dictionary<int, Lf2FrameData>(),
                BmpEntries = new List<Lf2BmpEntry>()
            };

            if (imported.movementParams != null)
            {
                foreach (var mp in imported.movementParams)
                    character.Movement[mp.key] = mp.amount;
            }

            if (imported.bmpSheets != null)
            {
                foreach (var b in imported.bmpSheets)
                {
                    character.BmpEntries.Add(new Lf2BmpEntry
                    {
                        Start = b.picStart,
                        End = b.picEnd,
                        Path = b.sourcePath,
                        CellW = b.cellW,
                        CellH = b.cellH,
                        Row = b.row,
                        Col = b.col
                    });
                }
            }

            if (imported.frameRows != null)
            {
                foreach (var row in imported.frameRows)
                {
                    var frame = new Lf2FrameData
                    {
                        Id = row.id,
                        Name = row.frameName ?? "",
                        Pic = row.pic,
                        State = (Lf2State)row.state,
                        Wait = row.wait,
                        Next = row.next,
                        Dvx = row.dvx,
                        Dvy = row.dvy,
                        SoundId = ParseSoundPath(row.soundPath),
                        ExtraProps = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                    };

                    if (row.hitboxes != null && row.hitboxes.Count > 0)
                    {
                        frame.Itrs = new Lf2ItrData[row.hitboxes.Count];
                        for (int i = 0; i < row.hitboxes.Count; i++)
                            frame.Itrs[i] = ConvertRectBlockToItr(row.hitboxes[i]);
                    }
                    else
                    {
                        frame.Itrs = Array.Empty<Lf2ItrData>();
                    }

                    if (row.hurtboxes != null && row.hurtboxes.Count > 0)
                    {
                        frame.Bdy = new Lf2BdyData[row.hurtboxes.Count];
                        for (int i = 0; i < row.hurtboxes.Count; i++)
                        {
                            var h = row.hurtboxes[i];
                            frame.Bdy[i] = new Lf2BdyData(new Rect(h.x, h.y, h.w, h.h));
                        }
                    }
                    else
                    {
                        frame.Bdy = Array.Empty<Lf2BdyData>();
                    }

                    if (row.opoints != null && row.opoints.Count > 0)
                    {
                        frame.Opoints = new Lf2OpointData[row.opoints.Count];
                        for (int i = 0; i < row.opoints.Count; i++)
                        {
                            var op = row.opoints[i];
                            var parsed = ParseRawProps(op.raw);
                            var dvx = GetFloat(parsed, "dvx", 0f);
                            var dvy = GetFloat(parsed, "dvy", 0f);
                            var x = GetFloat(parsed, "x", 0f);
                            var y = GetFloat(parsed, "y", 0f);
                            var facing = GetInt(parsed, "facing", 0);
                            frame.Opoints[i] = new Lf2OpointData(op.oid, new Vector2(x, y), new Vector2(dvx, dvy), facing);
                        }
                    }
                    else
                    {
                        frame.Opoints = Array.Empty<Lf2OpointData>();
                    }

                    character.Frames[row.id] = frame;
                }
            }

            Debug.Log($"[Lf2RuntimeDatLoader] Converted ImportedLf2Character '{character.Name}': {character.Frames.Count} frames.");
            return character;
        }

        private static Lf2ItrData ConvertRectBlockToItr(LF2Importer.Lf2RectBlock block)
        {
            var parsed = ParseRawProps(block.raw);
            return new Lf2ItrData(
                (Lf2ItrKind)GetInt(parsed, "kind", block.kind),
                new Rect(block.x, block.y, block.w, block.h),
                GetFloat(parsed, "dvx", 0f),
                GetFloat(parsed, "dvy", 0f),
                GetInt(parsed, "fall", 70),
                GetInt(parsed, "arest", 0),
                GetInt(parsed, "vrest", 0),
                GetInt(parsed, "injury", 0),
                (Lf2EffectType)GetInt(parsed, "effect", 0),
                GetInt(parsed, "bdefend", 0)
            );
        }

        private static Dictionary<string, string> ParseRawProps(string raw)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (string.IsNullOrEmpty(raw))
                return dict;

            foreach (Match m in KvRx.Matches(raw))
            {
                var k = m.Groups["key"].Value;
                var v = m.Groups["val"].Value;
                if (k.Length > 0)
                    dict[k] = v;
            }

            return dict;
        }

        private static int GetInt(Dictionary<string, string> d, string key, int def)
        {
            if (d.TryGetValue(key, out var v) && int.TryParse(v, NumberStyles.Integer, CultureInfo.InvariantCulture, out var r))
                return r;
            return def;
        }

        private static float GetFloat(Dictionary<string, string> d, string key, float def)
        {
            if (d.TryGetValue(key, out var v) && float.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out var r))
                return r;
            return def;
        }

        private static int ParseSoundPath(string soundPath)
        {
            if (string.IsNullOrEmpty(soundPath)) return 0;

            int slash = soundPath.LastIndexOf('/');
            int dot = soundPath.LastIndexOf('.');
            if (slash < 0) slash = -1;

            string numStr = dot > slash
                ? soundPath.Substring(slash + 1, dot - slash - 1)
                : soundPath.Substring(slash + 1);

            return int.TryParse(numStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out var id) ? id : 0;
        }
    }
}