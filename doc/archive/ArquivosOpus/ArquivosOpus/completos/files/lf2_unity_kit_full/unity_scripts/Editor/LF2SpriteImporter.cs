// Assets/Editor/LF2SpriteImporter.cs
// AssetPostprocessor that auto-configures sprites converted from LF2 BMPs.
//
// Triggered automatically when:
//   - PNGs are added/modified under Assets/Art/lf2_ref/characters/
//   - PNGs are added/modified under Assets/Art/lf2_ref/backgrounds/
//
// Configures:
//   - Texture type: Sprite (2D and UI)
//   - Filter: Point (no filter)         <- essential for pixel art
//   - Compression: None                 <- preserves color exactly
//   - sRGB: enabled                     <- color textures
//   - Pixels Per Unit: 80               <- 1 LF2 cell = 1 Unity unit
//
// For 800x560 character sheets, applies grid slicing into 70 frames (10x7, 80x80
// cells) with bottom-center pivot. For other sizes (faces, icons, backgrounds),
// keeps as Single sprite.
//
// To re-import, right-click the file in Unity and choose "Reimport".
// To change defaults, edit the constants below or override per-folder via
// project preferences (TODO if needed).

using System.IO;
using UnityEngine;
using UnityEditor;

namespace LF2Ref.Editor
{
    public sealed class LF2SpriteImporter : AssetPostprocessor
    {
        // --- configuration ---
        private const string LF2_REF_ROOT = "Assets/Art/lf2_ref/";
        private const string CHAR_PATH    = "Assets/Art/lf2_ref/characters/";
        private const string BG_PATH      = "Assets/Art/lf2_ref/backgrounds/";

        private const int CELL_SIZE  = 80;
        private const int GRID_COLS  = 10;
        private const int GRID_ROWS  = 7;
        private const int PIXELS_PER_UNIT = 80;

        // Standard LF2 character sheet size.
        private const int SHEET_W = 800;
        private const int SHEET_H = 560;

        // --- entry point ---
        private void OnPreprocessTexture()
        {
            string p = assetPath.Replace('\\', '/');
            if (!p.StartsWith(LF2_REF_ROOT))
                return;

            var importer = (TextureImporter)assetImporter;

            // Common pixel-art settings.
            importer.textureType        = TextureImporterType.Sprite;
            importer.filterMode         = FilterMode.Point;
            importer.mipmapEnabled      = false;
            importer.sRGBTexture        = true;
            importer.alphaIsTransparency = true;
            importer.spritePixelsPerUnit = PIXELS_PER_UNIT;

            var defaultSettings = importer.GetDefaultPlatformTextureSettings();
            defaultSettings.textureCompression = TextureImporterCompression.Uncompressed;
            importer.SetPlatformTextureSettings(defaultSettings);

            // Grid-slice if this looks like a character sprite sheet.
            // We only check the path — the actual size is verified in postprocess
            // since we don't have texture dimensions yet at preprocess time.
            if (p.StartsWith(CHAR_PATH) && IsLikelySheetByName(p))
            {
                importer.spriteImportMode = SpriteImportMode.Multiple;
            }
            else
            {
                importer.spriteImportMode  = SpriteImportMode.Single;
                importer.spritePivot        = new Vector2(0.5f, 0f); // bottom-center
                importer.spriteBorder       = Vector4.zero;
            }
        }

        private void OnPostprocessTexture(Texture2D texture)
        {
            string p = assetPath.Replace('\\', '/');
            if (!p.StartsWith(CHAR_PATH))
                return;
            if (!IsLikelySheetByName(p))
                return;

            // Only slice if dimensions match the standard LF2 sheet.
            // Other sheets (e.g. ball spritesheets) have non-standard sizes and
            // should be sliced manually for now.
            int w = texture.width;
            int h = texture.height;
            if (w != SHEET_W || h != SHEET_H)
                return;

            var importer = (TextureImporter)assetImporter;
            // Build grid metadata.
            int frameCount = GRID_COLS * GRID_ROWS;
            var spriteRects = new SpriteMetaData[frameCount];
            string baseName = Path.GetFileNameWithoutExtension(assetPath);

            for (int row = 0; row < GRID_ROWS; row++)
            {
                for (int col = 0; col < GRID_COLS; col++)
                {
                    int frameIndex = row * GRID_COLS + col;
                    // Unity Y is bottom-up. LF2 frame 0 is top-left, so Y inverts.
                    int yPx = (GRID_ROWS - 1 - row) * CELL_SIZE;
                    int xPx = col * CELL_SIZE;

                    spriteRects[frameIndex] = new SpriteMetaData
                    {
                        name      = $"{baseName}_{frameIndex:D3}",
                        rect      = new Rect(xPx, yPx, CELL_SIZE, CELL_SIZE),
                        alignment = (int)SpriteAlignment.Custom,
                        pivot     = new Vector2(0.5f, 0f), // bottom-center inside cell
                    };
                }
            }

#pragma warning disable CS0618 // SpriteMetaData is "obsolete" but still the only API for batch slicing in 2022+
            importer.spritesheet = spriteRects;
#pragma warning restore CS0618
            importer.SaveAndReimport();
        }

        // --- helpers ---
        private static bool IsLikelySheetByName(string assetPathLower)
        {
            // Faces and icons should NOT be sliced.
            string fname = Path.GetFileNameWithoutExtension(assetPathLower).ToLowerInvariant();
            if (fname.EndsWith("_f") || fname.EndsWith("_s") || fname == "face")
                return false;
            // Mirror sprites should never reach here (excluded by Python script),
            // but defend anyway.
            if (fname.Contains("_mirror"))
                return false;
            return true;
        }
    }
}
