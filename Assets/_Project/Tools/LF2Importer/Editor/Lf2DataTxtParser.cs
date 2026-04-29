using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace LF2Importer.EditorTools
{
    public sealed class Lf2ObjectEntry
    {
        public int id;
        public int type;
        public string file;
    }

    public static class Lf2DataTxtParser
    {
        private static readonly Regex LineRx = new Regex(
            @"^\s*id:\s*(?<id>\d+)\s+type:\s*(?<type>\d+)\s+file:\s*(?<file>.+?)\s*(#.*)?$",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static List<Lf2ObjectEntry> ReadObjects(string dataTxtPath)
        {
            var list = new List<Lf2ObjectEntry>();
            if (!File.Exists(dataTxtPath))
            {
                Debug.LogWarning($"[LF2Importer] data.txt not found: {dataTxtPath}");
                return list;
            }

            foreach (var raw in File.ReadAllLines(dataTxtPath))
            {
                var line = raw.Trim();
                if (line.Length == 0 || line.StartsWith("#"))
                    continue;
                var m = LineRx.Match(line);
                if (!m.Success)
                    continue;
                list.Add(new Lf2ObjectEntry
                {
                    id = int.Parse(m.Groups["id"].Value),
                    type = int.Parse(m.Groups["type"].Value),
                    file = m.Groups["file"].Value.Trim().Replace('\\', '/')
                });
            }

            return list;
        }

        public static IEnumerable<Lf2ObjectEntry> GetCharacters(IEnumerable<Lf2ObjectEntry> all)
        {
            foreach (var e in all)
            {
                if (e.type == 0)
                    yield return e;
            }
        }

        public static bool TryResolveDatPath(string lf2Root, Lf2ObjectEntry entry, out string absoluteDatPath)
        {
            absoluteDatPath = null;
            if (string.IsNullOrEmpty(lf2Root) || string.IsNullOrEmpty(entry.file))
                return false;
            var rel = entry.file.Replace('\\', '/');
            var combined = Path.Combine(lf2Root, rel.Replace('/', Path.DirectorySeparatorChar));
            if (!File.Exists(combined))
                return false;
            absoluteDatPath = combined;
            return true;
        }
    }
}
