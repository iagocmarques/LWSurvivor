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
            return !name.StartsWith("bg_");
        }
    }
}
