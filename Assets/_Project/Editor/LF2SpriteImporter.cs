using System.IO;
using UnityEditor;
using UnityEngine;

namespace Project.EditorTools
{
    public sealed class LF2SpriteImporter : AssetPostprocessor
    {
        private const string Root = "Assets/_Project/Resources/LF2/";
        private const int Cell = 80;
        private const int Cols = 10;
        private const int Rows = 7;

        private void OnPreprocessTexture()
        {
            var path = assetPath.Replace('\\', '/');
            if (!path.StartsWith(Root))
                return;

            var importer = (TextureImporter)assetImporter;
            importer.textureType = TextureImporterType.Sprite;
            importer.filterMode = FilterMode.Point;
            importer.mipmapEnabled = false;
            importer.sRGBTexture = true;
            importer.alphaIsTransparency = true;
            importer.spritePixelsPerUnit = 80f;
            importer.isReadable = true;

            var platform = importer.GetDefaultPlatformTextureSettings();
            platform.textureCompression = TextureImporterCompression.Uncompressed;
            importer.SetPlatformTextureSettings(platform);

            if (IsCharacterSheet(path))
            {
                importer.spriteImportMode = SpriteImportMode.Multiple;
                importer.spritePivot = new Vector2(0.5f, 0f);

                // Set sprite sheet metadata here (not in OnPostprocessTexture)
                var metas = new SpriteMetaData[Cols * Rows];
                var baseName = Path.GetFileNameWithoutExtension(path);

                for (var row = 0; row < Rows; row++)
                {
                    for (var col = 0; col < Cols; col++)
                    {
                        var i = row * Cols + col;
                        var x = col * Cell;
                        var y = (Rows - 1 - row) * Cell;
                        metas[i] = new SpriteMetaData
                        {
                            name = $"{baseName}_{i:D3}",
                            rect = new Rect(x, y, Cell, Cell),
                            alignment = (int)SpriteAlignment.Custom,
                            pivot = new Vector2(0.5f, 0f)
                        };
                    }
                }

#pragma warning disable CS0618
                importer.spritesheet = metas;
#pragma warning restore CS0618
            }
            else
            {
                importer.spriteImportMode = SpriteImportMode.Single;
                importer.spritePivot = new Vector2(0.5f, 0f);
            }
        }

private static bool IsCharacterSheet(string path)
        {
            if (!path.EndsWith("_alpha.png"))
                return false;
            var name = Path.GetFileNameWithoutExtension(path).ToLowerInvariant();
            if (name.StartsWith("bg_"))
                return false;

            // Extract base name (strip _alpha suffix)
            var baseName = name;
            if (baseName.EndsWith("_alpha"))
                baseName = baseName.Substring(0, baseName.Length - 6);

            // Must match character sheet pattern: ends with _digit or _digit_b
            if (!System.Text.RegularExpressions.Regex.IsMatch(baseName, @"_\d+b?$"))
                return false;

            // Must be large enough for at least one 80x80 cell
            try
            {
                var fullPath = System.IO.Path.GetFullPath(path);
                if (!System.IO.File.Exists(fullPath))
                    return false;
                var header = new byte[26];
                using (var fs = System.IO.File.OpenRead(fullPath))
                {
                    if (fs.Read(header, 0, 26) < 26)
                        return false;
                }
                // PNG: width@16 big-endian, height@20 big-endian
                if (header[0] == 0x89 && header[1] == 0x50)
                {
                    var w = (header[16] << 24) | (header[17] << 16) | (header[18] << 8) | header[19];
                    var h = (header[20] << 24) | (header[21] << 16) | (header[22] << 8) | header[23];
                    return w >= Cell && h >= Cell;
                }
                // BMP: width@18 little-endian, height@22 little-endian
                if (header[0] == 'B' && header[1] == 'M')
                {
                    var w = header[18] | (header[19] << 8) | (header[20] << 16) | (header[21] << 24);
                    var h = header[22] | (header[23] << 8) | (header[24] << 16) | (header[25] << 24);
                    return w >= Cell && System.Math.Abs(h) >= Cell;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
