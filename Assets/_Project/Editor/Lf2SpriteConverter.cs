using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Project.EditorTools
{
    public static class Lf2SpriteConverter
    {
        private const string SourcePngDir = "Assets/Art/lf2_ref/characters";
        private const string ResourcesDir = "Assets/_Project/Resources/LF2";
        private const string AlphaSuffix = "_alpha.png";

        [MenuItem("_Project/LF2/Convert and Import Sprites")]
        public static void ConvertAndImport()
        {
            var copiedPng = 0;
            var convertedBmp = 0;
            var skipped = 0;
            var reimportPaths = new List<string>();

            // Ensure target directory exists
            if (!AssetDatabase.IsValidFolder(ResourcesDir))
            {
                Directory.CreateDirectory(Path.GetFullPath(ResourcesDir));
                AssetDatabase.Refresh();
            }

            // --- Step 1: Copy PNGs from Art/lf2_ref/characters with _alpha suffix ---
            if (AssetDatabase.IsValidFolder(SourcePngDir))
            {
                var sourcePngs = Directory.GetFiles(Path.GetFullPath(SourcePngDir), "*.png");
                foreach (var srcPath in sourcePngs)
                {
                    var srcAssetPath = srcPath.Replace('\\', '/');
                    if (!srcAssetPath.StartsWith("Assets/"))
                        srcAssetPath = "Assets/" + srcAssetPath.Substring(Path.GetFullPath("Assets").Replace('\\', '/').Length + 1);

                    var fileName = Path.GetFileNameWithoutExtension(srcAssetPath);
                    var destName = fileName + AlphaSuffix;
                    var destAssetPath = ResourcesDir + "/" + destName;

                    if (AssetExists(destAssetPath))
                    {
                        skipped++;
                        continue;
                    }

                    if (AssetDatabase.CopyAsset(srcAssetPath, destAssetPath))
                    {
                        copiedPng++;
                        reimportPaths.Add(destAssetPath);
                    }
                    else
                    {
                        Debug.LogWarning($"[Lf2SpriteConverter] Failed to copy {srcAssetPath} → {destAssetPath}");
                    }
                }
            }
            else
            {
                Debug.LogWarning($"[Lf2SpriteConverter] Source PNG directory not found: {SourcePngDir}");
            }

            // --- Step 2: Convert BMPs that don't have corresponding _alpha.png ---
            var bmpDir = Path.GetFullPath(ResourcesDir);
            if (Directory.Exists(bmpDir))
            {
                var bmpFiles = Directory.GetFiles(bmpDir, "*.bmp");
                foreach (var bmpPath in bmpFiles)
                {
                    var bmpFileName = Path.GetFileNameWithoutExtension(bmpPath);

                    // Skip mirror BMPs
                    if (bmpFileName.EndsWith("_mirror", StringComparison.OrdinalIgnoreCase))
                        continue;

                    var alphaPngPath = ResourcesDir + "/" + bmpFileName + AlphaSuffix;

                    // Skip if _alpha.png already exists (copied from source PNG or previously converted)
                    if (AssetExists(alphaPngPath))
                        continue;

                    var pngBytes = ConvertBmpToPngWithChromaKey(bmpPath);
                    if (pngBytes == null)
                    {
                        Debug.LogWarning($"[Lf2SpriteConverter] Failed to convert BMP: {bmpPath}");
                        continue;
                    }

                    var destAbsPath = Path.Combine(bmpDir, bmpFileName + AlphaSuffix);
                    File.WriteAllBytes(destAbsPath, pngBytes);
                    convertedBmp++;
                    reimportPaths.Add(alphaPngPath);
                }
            }

            AssetDatabase.Refresh();

            // --- Step 3: Reimport all _alpha.png files so LF2SpriteImporter processes them ---
            foreach (var path in reimportPaths)
            {
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            }

            Debug.Log($"[Lf2SpriteConverter] Done. Copied PNGs: {copiedPng}, Converted BMPs: {convertedBmp}, Skipped (already exist): {skipped}. Total reimported: {reimportPaths.Count}");
        }

        private static bool AssetExists(string assetPath)
        {
            return File.Exists(Path.GetFullPath(assetPath));
        }

private static byte[] ConvertBmpToPngWithChromaKey(string bmpFilePath)
        {
            try
            {
                var bmpData = File.ReadAllBytes(bmpFilePath);
                if (bmpData.Length < 54) return null;
                if (bmpData[0] != 'B' || bmpData[1] != 'M') return null;

                var bitsPerPixel = BitConverter.ToInt16(bmpData, 28);
                var compression = BitConverter.ToInt32(bmpData, 30);

                Texture2D texture;
                if (bitsPerPixel == 8 && compression == 1)
                    texture = ParseBmp8BitRle(bmpData);
                else if (bitsPerPixel == 24 && compression == 0)
                    texture = ParseBmp24Bit(bmpData);
                else
                    return null;

                if (texture == null) return null;
                ApplyChromaKey(texture);
                return texture.EncodeToPNG();
            }
            catch (Exception e)
            {
                Debug.LogError($"[Lf2SpriteConverter] BMP conversion error for {bmpFilePath}: {e.Message}");
                return null;
            }
        }

        private static Texture2D ParseBmp24Bit(byte[] data)
        {
            if (data.Length < 54)
                return null;

            // BMP header
            if (data[0] != 'B' || data[1] != 'M')
                return null;

            var dataOffset = BitConverter.ToInt32(data, 10);
            var headerSize = BitConverter.ToInt32(data, 14);
            var width = BitConverter.ToInt32(data, 18);
            var height = BitConverter.ToInt32(data, 22);
            var bitsPerPixel = BitConverter.ToInt16(data, 28);
            var compression = BitConverter.ToInt32(data, 30);

            if (bitsPerPixel != 24 || compression != 0)
            {
                Debug.LogWarning($"[Lf2SpriteConverter] Unsupported BMP format: {bitsPerPixel}bpp, compression={compression}");
                return null;
            }

            if (width <= 0 || height <= 0 || width > 10000 || height > 10000)
                return null;

            var absHeight = Math.Abs(height);
            var texture = new Texture2D(width, absHeight, TextureFormat.RGBA32, false);
            var rowSize = ((width * 3 + 3) / 4) * 4; // rows are padded to 4-byte boundary
            var bottomUp = height > 0;

            for (var row = 0; row < absHeight; row++)
            {
                var srcRow = bottomUp ? (absHeight - 1 - row) : row;
                var rowOffset = dataOffset + srcRow * rowSize;

                for (var col = 0; col < width; col++)
                {
                    var pixelOffset = rowOffset + col * 3;
                    if (pixelOffset + 2 >= data.Length)
                        continue;

                    var b = data[pixelOffset];
                    var g = data[pixelOffset + 1];
                    var r = data[pixelOffset + 2];

                    texture.SetPixel(col, row, new Color32(r, g, b, 255));
                }
            }

            texture.Apply();
            return texture;
        }

        private static void ApplyChromaKey(Texture2D texture)
        {
            var pixels = texture.GetPixels32();
            for (var i = 0; i < pixels.Length; i++)
            {
                if (pixels[i].r == 0 && pixels[i].g == 0 && pixels[i].b == 0)
                    pixels[i].a = 0;
                else
                    pixels[i].a = 255;
            }
            texture.SetPixels32(pixels);
            texture.Apply();
        }
    

private static Texture2D ParseBmp8BitRle(byte[] data)
        {
            var dataOffset = BitConverter.ToInt32(data, 10);
            var headerSize = BitConverter.ToInt32(data, 14);
            var width = BitConverter.ToInt32(data, 18);
            var height = BitConverter.ToInt32(data, 22);

            if (width <= 0 || height <= 0 || width > 10000 || height > 10000) return null;

            var absHeight = Math.Abs(height);
            var bottomUp = height > 0;

            // Read color palette (starts after BMP header + DIB header)
            var paletteOffset = 14 + headerSize;
            var palette = new Color32[256];
            for (var i = 0; i < 256; i++)
            {
                var p = paletteOffset + i * 4;
                if (p + 3 < data.Length)
                    palette[i] = new Color32(data[p + 2], data[p + 1], data[p], 255);
            }

            // Decode RLE8 pixel data into indexed buffer
            var indexed = new byte[width * absHeight];
            var pos = dataOffset;
            var x = 0;
            var y = 0;

            while (pos < data.Length - 1 && y < absHeight)
            {
                var first = data[pos];
                var second = data[pos + 1];
                pos += 2;

                if (first > 0)
                {
                    // Encoded mode: repeat second byte first times
                    for (var i = 0; i < first && x < width; i++)
                        indexed[y * width + x++] = second;
                }
                else
                {
                    // Escape
                    if (second == 0)
                    {
                        // End of line
                        x = 0;
                        y++;
                    }
                    else if (second == 1)
                    {
                        // End of bitmap
                        break;
                    }
                    else if (second == 2)
                    {
                        // Delta
                        if (pos + 1 < data.Length)
                        {
                            x += data[pos];
                            y += data[pos + 1];
                            pos += 2;
                        }
                    }
                    else
                    {
                        // Literal run of second bytes
                        for (var i = 0; i < second && x < width && pos < data.Length; i++)
                            indexed[y * width + x++] = data[pos++];
                        // Pad to word boundary
                        if (second % 2 != 0) pos++;
                    }
                }
            }

            // Build RGBA texture
            var texture = new Texture2D(width, absHeight, TextureFormat.RGBA32, false);
            for (var row = 0; row < absHeight; row++)
            {
                var srcRow = bottomUp ? (absHeight - 1 - row) : row;
                for (var col = 0; col < width; col++)
                {
                    var idx = indexed[srcRow * width + col];
                    texture.SetPixel(col, row, palette[idx]);
                }
            }

            texture.Apply();
            return texture;
        }
}
}
