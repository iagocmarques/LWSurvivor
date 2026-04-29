using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace LF2Importer.EditorTools
{
    public static class Lf2DatDecryptor
    {
        public const string Key37 = "odBearBecauseHeIsVeryGoodSiuHungIsAGo";
        public const string Key32 = "odBearBecauseHeIsVeryGoodSiuMan!";

        public static bool LooksPlaintext(byte[] headBytes)
        {
            if (headBytes == null || headBytes.Length == 0)
                return false;
            var n = Mathf.Min(headBytes.Length, 4096);
            var s = Encoding.ASCII.GetString(headBytes, 0, n);
            return s.IndexOf("<bmp_begin>", StringComparison.OrdinalIgnoreCase) >= 0
                   || s.IndexOf("<frame>", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        public static string ReadDatAsText(string path)
        {
            var raw = File.ReadAllBytes(path);
            if (raw.Length == 0)
                return string.Empty;

            if (LooksPlaintext(raw))
                return NormalizeNewlines(Encoding.GetEncoding(28591).GetString(raw));

            var best = "";
            var bestScore = -1;

            void TryVariant(string label, Func<byte[], string> fn)
            {
                string text;
                try
                {
                    text = fn(raw);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[LF2Importer] Decrypt variant {label} failed: {ex.Message}");
                    return;
                }

                var score = ScoreDecoded(text);
                if (score > bestScore)
                {
                    bestScore = score;
                    best = text;
                }
            }

            TryVariant("kit32_trim123", DecryptKitStyle);
            TryVariant("trae37_head123", DecryptTraeHeadOnlyKey37);
            TryVariant("head123_key32", DecryptHeadOnlyKey32);

            if (bestScore < 0)
                best = Encoding.GetEncoding(28591).GetString(raw);

            return NormalizeNewlines(best);
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
                throw new InvalidDataException("file too small for kit-style trim");
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
                throw new InvalidDataException("file too small");
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
                throw new InvalidDataException("file too small");
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

        public static void DebugPrintFirstLines(string path, int lineCount = 40)
        {
            var text = ReadDatAsText(path);
            var lines = text.Split('\n');
            Debug.Log($"[LF2Importer] First {lineCount} lines of {path}:");
            for (var i = 0; i < lines.Length && i < lineCount; i++)
                Debug.Log($"{i + 1:D3}: {lines[i]}");
        }
    }
}
