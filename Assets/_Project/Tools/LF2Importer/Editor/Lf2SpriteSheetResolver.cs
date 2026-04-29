using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace LF2Importer.EditorTools
{
    public static class Lf2SpriteSheetResolver
    {
        private static readonly Dictionary<string, Sprite[]> _cache = new Dictionary<string, Sprite[]>();

        public static void ClearCache() => _cache.Clear();

        public static bool TryGetSpriteForPic(
            Lf2ParsedDat dat,
            string convertedSpritesRootUnity,
            int pic,
            out Sprite sprite,
            out string resolvedUnityPath)
        {
            sprite = null;
            resolvedUnityPath = null;
            if (dat == null || string.IsNullOrEmpty(convertedSpritesRootUnity))
                return false;

            foreach (var e in dat.bmpEntries)
            {
                if (pic < e.start || pic > e.end)
                    continue;

                var baseName = Path.GetFileNameWithoutExtension(e.path.Replace('\\', '/'));
                var local = pic - e.start;
                var unityPath = Path.Combine(convertedSpritesRootUnity, baseName + ".png")
                    .Replace('\\', '/');
                if (!unityPath.StartsWith("Assets/", System.StringComparison.OrdinalIgnoreCase))
                    unityPath = ("Assets/" + unityPath.TrimStart('/')).Replace('\\', '/');
                resolvedUnityPath = unityPath;

                if (!_cache.TryGetValue(unityPath, out var sprites))
                {
                    sprites = AssetDatabase.LoadAllAssetsAtPath(unityPath).OfType<Sprite>().ToArray();
                    _cache[unityPath] = sprites;
                }

                if (sprites.Length == 0)
                    return false;

                var wantName = $"{baseName}_{local}";
                foreach (var s in sprites)
                {
                    if (s != null && s.name == wantName)
                    {
                        sprite = s;
                        return true;
                    }
                }

                return false;
            }

            return false;
        }

        public static bool TryResolvePicToSheet(Lf2ParsedDat dat, int pic, out Lf2BmpEntry entry, out int localIndex)
        {
            entry = null;
            localIndex = 0;
            foreach (var e in dat.bmpEntries)
            {
                if (pic >= e.start && pic <= e.end)
                {
                    entry = e;
                    localIndex = pic - e.start;
                    return true;
                }
            }

            return false;
        }
    }
}
