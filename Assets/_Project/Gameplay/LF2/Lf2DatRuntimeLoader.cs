using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public static class Lf2DatRuntimeLoader
    {
        private static readonly Regex FileLineRx = new Regex(
            @"^\s*file\s*\(\s*(?<a>\d+)\s*-\s*(?<b>\d+)\s*\)\s*:\s*(?<path>\S+)\s+w\s*:\s*(?<w>\d+)\s+h\s*:\s*(?<h>\d+)\s+row\s*:\s*(?<row>\d+)\s+col\s*:\s*(?<col>\d+)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex FrameHeaderRx = new Regex(
            @"^\s*<frame>\s*(?<id>\d+)\s+(?<name>.+?)\s*$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private static readonly Regex KvRx = new Regex(
            @"\b(?<key>[a-zA-Z_][a-zA-Z0-9_]*)\s*:\s*(?<val>\S+)",
            RegexOptions.Compiled);

        public const string Key37 = "odBearBecauseHeIsVeryGoodSiuHungIsAGo";
        public const string Key32 = "odBearBecauseHeIsVeryGoodSiuMan!";

        public static Lf2CharacterData LoadFromBytes(byte[] raw)
        {
            var text = DecryptToText(raw);
            return ParseText(text);
        }

        public static Lf2CharacterData LoadFromText(string text)
        {
            return ParseText(text);
        }

        private static string DecryptToText(byte[] raw)
        {
            if (raw == null || raw.Length == 0)
                return string.Empty;

            if (LooksPlaintext(raw))
                return NormalizeNewlines(Encoding.GetEncoding(28591).GetString(raw));

            var best = "";
            var bestScore = -1;

            TryVariant("kit32_trim123", DecryptKitStyle, raw, ref best, ref bestScore);
            TryVariant("trae37_head123", DecryptTraeHeadOnlyKey37, raw, ref best, ref bestScore);
            TryVariant("head123_key32", DecryptHeadOnlyKey32, raw, ref best, ref bestScore);

            if (bestScore < 0)
                best = Encoding.GetEncoding(28591).GetString(raw);

            return NormalizeNewlines(best);
        }

        private static void TryVariant(string label, Func<byte[], string> fn, byte[] data,
            ref string best, ref int bestScore)
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
            catch
            {
                // ignore failed variants
            }
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
                throw new InvalidOperationException("file too small for kit-style trim");
            var innerLen = data.Length - header * 2;
            var key = Encoding.ASCII.GetBytes(Key32);
            var outb = new byte[innerLen];
            for (var i = 0; i < innerLen; i++)
                outb[i] = (byte)((data[header + i] - key[i % key.Length] + 256) % 256);
            return Encoding.GetEncoding(28591).GetString(outb);
        }

        private static string DecryptTraeHeadOnlyKey37(byte[] data)
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

        private static string NormalizeNewlines(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            return s.Replace("\r\n", "\n").Replace("\r", "\n");
        }

        private static Lf2CharacterData ParseText(string text)
        {
            var character = new Lf2CharacterData
            {
                Frames = new Dictionary<int, Lf2FrameData>(),
                BmpEntries = new List<Lf2BmpEntry>(),
                Movement = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase)
            };

            if (string.IsNullOrEmpty(text))
                return character;

            var lines = text.Split('\n');
            var i = 0;

            while (i < lines.Length)
            {
                var line = lines[i];
                if (line.IndexOf("<bmp_begin>", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    i = ParseBmpBegin(lines, i, character);
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
                    i = ParseOneFrame(lines, i, character);
                    continue;
                }

                i++;
            }

            character.RoleIds = Lf2FrameRoleIds.BuildFromCharacterData(character);

            return character;
        }

        private static int ParseBmpBegin(string[] lines, int start, Lf2CharacterData character)
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
                    character.Name = AfterColon(line);
                else if (line.StartsWith("head:", StringComparison.OrdinalIgnoreCase))
                    character.Head = AfterColon(line);
                else if (line.StartsWith("small:", StringComparison.OrdinalIgnoreCase))
                    character.Small = AfterColon(line);
                else
                {
                    var m = FileLineRx.Match(line);
                    if (m.Success)
                    {
                        character.BmpEntries.Add(new Lf2BmpEntry
                        {
                            Start = int.Parse(m.Groups["a"].Value),
                            End = int.Parse(m.Groups["b"].Value),
                            Path = m.Groups["path"].Value.Trim().Replace('\\', '/'),
                            CellW = int.Parse(m.Groups["w"].Value),
                            CellH = int.Parse(m.Groups["h"].Value),
                            Row = int.Parse(m.Groups["row"].Value),
                            Col = int.Parse(m.Groups["col"].Value)
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
                    character.Movement[key] = fv;
            }

            return i;
        }

        private static int ParseOneFrame(string[] lines, int start, Lf2CharacterData character)
        {
            var header = lines[start].Trim();
            var mh = FrameHeaderRx.Match(header);
            if (!mh.Success)
                return start + 1;

            var frameId = int.Parse(mh.Groups["id"].Value);
            var frameName = mh.Groups["name"].Value.Trim();

            var props = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var bdyBlocks = new List<Dictionary<string, string>>();
            var itrBlocks = new List<Dictionary<string, string>>();
            var opointBlocks = new List<Dictionary<string, string>>();

            var i = start + 1;
            for (; i < lines.Length; i++)
            {
                var raw = lines[i];
                var trimmed = raw.Trim();

                if (trimmed.StartsWith("<frame_end>", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    break;
                }

                if (trimmed.Equals("bdy:", StringComparison.OrdinalIgnoreCase))
                {
                    i = ReadBlock(lines, i + 1, "bdy_end:", bdyBlocks);
                    continue;
                }

                if (trimmed.Equals("itr:", StringComparison.OrdinalIgnoreCase))
                {
                    i = ReadBlock(lines, i + 1, "itr_end:", itrBlocks);
                    continue;
                }

                if (trimmed.Equals("opoint:", StringComparison.OrdinalIgnoreCase))
                {
                    i = ReadBlock(lines, i + 1, "opoint_end:", opointBlocks);
                    continue;
                }

                MergePropsLine(props, trimmed);
            }

            var frame = Lf2FrameDataConverter.ConvertFrame(
                props, itrBlocks, bdyBlocks, opointBlocks, frameId, frameName);

            character.Frames[frameId] = frame;
            return i;
        }

        private static int ReadBlock(string[] lines, int start, string endToken,
            List<Dictionary<string, string>> sink)
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
    }
}
