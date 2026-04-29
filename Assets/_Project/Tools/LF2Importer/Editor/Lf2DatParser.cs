using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LF2Importer.EditorTools
{
    public sealed class Lf2BmpEntry
    {
        public int start;
        public int end;
        public string path;
        public int cellW;
        public int cellH;
        public int row;
        public int col;
    }

    public sealed class Lf2ParsedFrame
    {
        public int id;
        public string frameName = "";
        public readonly Dictionary<string, string> props = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        public readonly List<Dictionary<string, string>> bdyBlocks = new List<Dictionary<string, string>>();
        public readonly List<Dictionary<string, string>> itrBlocks = new List<Dictionary<string, string>>();
        public readonly List<Dictionary<string, string>> opointBlocks = new List<Dictionary<string, string>>();
    }

    public sealed class Lf2ParsedDat
    {
        public string name = "";
        public string head = "";
        public string small = "";
        public readonly List<Lf2BmpEntry> bmpEntries = new List<Lf2BmpEntry>();
        public readonly Dictionary<string, float> movement = new Dictionary<string, float>(StringComparer.OrdinalIgnoreCase);
        public readonly Dictionary<int, Lf2ParsedFrame> frames = new Dictionary<int, Lf2ParsedFrame>();
        public readonly List<string> parseWarnings = new List<string>();
    }

    public static class Lf2DatParser
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

        public static Lf2ParsedDat ParseFromFile(string datPath)
        {
            var text = Lf2DatDecryptor.ReadDatAsText(datPath);
            return ParseText(text, datPath);
        }

        public static Lf2ParsedDat ParseText(string text, string contextPathForLogs = "")
        {
            var dat = new Lf2ParsedDat();
            if (string.IsNullOrEmpty(text))
            {
                dat.parseWarnings.Add("Empty .dat text.");
                return dat;
            }

            var lines = text.Split(new[] { '\n' }, StringSplitOptions.None);
            var i = 0;
            while (i < lines.Length)
            {
                var line = lines[i];
                if (line.IndexOf("<bmp_begin>", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    i = ParseBmpBegin(lines, i, dat);
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
                    i = ParseOneFrame(lines, i, dat);
                    continue;
                }

                i++;
            }

            ValidateNextPointers(dat, contextPathForLogs);
            return dat;
        }

        private static int ParseBmpBegin(string[] lines, int start, Lf2ParsedDat dat)
        {
            var i = start + 1;
            for (; i < lines.Length; i++)
            {
                var raw = lines[i];
                var line = raw.Trim();

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
                        dat.bmpEntries.Add(new Lf2BmpEntry
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

        private static int ParseOneFrame(string[] lines, int start, Lf2ParsedDat dat)
        {
            var header = lines[start].Trim();
            var mh = FrameHeaderRx.Match(header);
            if (!mh.Success)
            {
                return start + 1;
            }

            var frame = new Lf2ParsedFrame
            {
                id = int.Parse(mh.Groups["id"].Value),
                frameName = mh.Groups["name"].Value.Trim()
            };

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

            if (dat.frames.ContainsKey(frame.id))
                dat.parseWarnings.Add($"Duplicate frame id {frame.id}; last wins.");
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

        private static void ValidateNextPointers(Lf2ParsedDat dat, string ctx)
        {
            foreach (var kv in dat.frames)
            {
                if (!kv.Value.props.TryGetValue("next", out var ns))
                    continue;
                if (!int.TryParse(ns, out var next))
                    continue;
                if (next == 999)
                    continue;
                if (!dat.frames.ContainsKey(next))
                    dat.parseWarnings.Add($"{ctx}: frame {kv.Key} next -> {next} missing.");
            }
        }

        public static int GetPicMax(Lf2ParsedDat dat)
        {
            var max = 0;
            foreach (var f in dat.frames.Values)
            {
                if (f.props.TryGetValue("pic", out var ps) && int.TryParse(ps, out var pic))
                    max = Mathf.Max(max, pic);
            }

            return max;
        }

        public static bool IsPicInRange(Lf2ParsedDat dat, int pic)
        {
            foreach (var e in dat.bmpEntries)
            {
                if (pic >= e.start && pic <= e.end)
                    return true;
            }

            return false;
        }
    }
}
