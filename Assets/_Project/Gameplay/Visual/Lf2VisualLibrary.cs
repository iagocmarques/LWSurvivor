using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Visual
{
    public static class Lf2VisualLibrary
    {
        private const int Lf2FrameWidth = 80;
        private const int Lf2FrameHeight = 80;
        private const float CharacterPixelsPerUnit = 48f;

        private static readonly Dictionary<string, Sprite> Cache = new Dictionary<string, Sprite>();
        private static readonly Dictionary<string, List<RectInt>> SheetRectCache = new Dictionary<string, List<RectInt>>();
        private static readonly string[] BackgroundKeys =
        {
            "bg_pic1_alpha",
            "bg_template1_pic1_alpha",
            "bg_template2_pic1_alpha",
            "bg_template3_pic1_alpha"
        };

        public static Sprite GetPlayerSprite()
        {
            return LoadCharacterFrame("player_davis_0_alpha", preferredFrame: 2);
        }

        public static Sprite GetPlayerFrame(int frameIndex)
        {
            // LF2 Davis: pic 0-69 = sheet _0, pic 70-139 = sheet _1
            if (frameIndex >= 70)
                return GetCharacterFrame("player_davis_1_alpha", frameIndex - 70);
            return GetCharacterFrame("player_davis_0_alpha", frameIndex);
        }

        public static Sprite GetEnemySprite(string enemyId)
        {
            return LoadCharacterFrame(GetEnemySheetKey(enemyId), preferredFrame: 0);
        }

        public static Sprite GetEnemyFrame(string enemyId, int frameIndex)
        {
            // LF2 enemies: pic 0-69 = sheet _0, pic 70-139 = sheet _1
            if (frameIndex >= 70)
                return GetCharacterFrame(GetEnemySheetKey1(enemyId), frameIndex - 70);
            return GetCharacterFrame(GetEnemySheetKey(enemyId), frameIndex);
        }

        public static Sprite GetBackgroundSprite()
        {
            return Load("bg_pic1_alpha");
        }

        public static int BackgroundCount => BackgroundKeys.Length;

        public static Sprite GetBackgroundSpriteByIndex(int index)
        {
            if (BackgroundKeys.Length == 0)
                return null;

            var i = index % BackgroundKeys.Length;
            if (i < 0)
                i += BackgroundKeys.Length;
            return Load(BackgroundKeys[i]);
        }

        public static int GetPlayerFrameCount()
        {
            return GetCharacterFrameCount("player_davis_0_alpha")
                 + GetCharacterFrameCount("player_davis_1_alpha");
        }

        public static int GetEnemyFrameCount(string enemyId)
        {
            return GetCharacterFrameCount(GetEnemySheetKey(enemyId))
                 + GetCharacterFrameCount(GetEnemySheetKey1(enemyId));
        }

        public static Sprite GetCharacterFrame(string key, int frameIndex)
        {
            var cacheKey = key + "#frame_" + frameIndex;
            if (Cache.TryGetValue(cacheKey, out var sprite))
                return sprite;

            var texture = Resources.Load<Texture2D>("LF2/" + key);
            if (texture == null)
                return Load(key);

            var rects = GetNonEmptyFrameRects(key, texture);
            if (rects.Count == 0)
                return Load(key);

            var clamped = Mathf.Clamp(frameIndex, 0, rects.Count - 1);
            var rect = rects[clamped];
            sprite = Sprite.Create(
                texture,
                new Rect(rect.x, rect.y, rect.width, rect.height),
                new Vector2(0.5f, 0f),
                CharacterPixelsPerUnit,
                SpriteMeshType.FullRect);
            sprite.name = key + "_frame_" + clamped;
            Cache[cacheKey] = sprite;
            return sprite;
        }

        private static Sprite LoadCharacterFrame(string key, int preferredFrame)
        {
            return GetCharacterFrame(key, preferredFrame);
        }

        private static int GetCharacterFrameCount(string key)
        {
            var texture = Resources.Load<Texture2D>("LF2/" + key);
            if (texture == null)
                return 0;
            return GetNonEmptyFrameRects(key, texture).Count;
        }

        private static string GetEnemySheetKey(string enemyId)
        {
            if (!string.IsNullOrWhiteSpace(enemyId))
            {
                if (enemyId.Contains("scout"))
                    return "enemy_scout_hunter_0b_alpha";
                if (enemyId.Contains("bruiser"))
                    return "enemy_bruiser_knight_0_alpha";
            }

            return "enemy_grunt_bandit_0_alpha";
        }

        private static string GetEnemySheetKey1(string enemyId)
        {
            // _1 sheets: only bandit has one in Resources for now
            if (!string.IsNullOrWhiteSpace(enemyId) && enemyId.Contains("bandit"))
                return "enemy_grunt_bandit_1_alpha";
            // Fallback to _0 sheet if _1 doesn't exist
            return GetEnemySheetKey(enemyId);
        }

        private static List<RectInt> GetNonEmptyFrameRects(string key, Texture2D texture)
        {
            if (SheetRectCache.TryGetValue(key, out var cached))
                return cached;

            var results = new List<RectInt>();
            if (texture == null || texture.width <= 0 || texture.height <= 0)
            {
                SheetRectCache[key] = results;
                return results;
            }

            var columns = texture.width / Lf2FrameWidth;
            var rows = texture.height / Lf2FrameHeight;
            if (columns <= 0 || rows <= 0)
            {
                SheetRectCache[key] = results;
                return results;
            }

            // LF2 sheets are organized in a regular 79x79 grid.
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < columns; col++)
                {
                    var sourceTop = row * Lf2FrameHeight;
                    var sourceLeft = col * Lf2FrameWidth;
                    var unityY = texture.height - sourceTop - Lf2FrameHeight;
                    results.Add(new RectInt(sourceLeft, unityY, Lf2FrameWidth, Lf2FrameHeight));
                }
            }

            SheetRectCache[key] = results;
            return results;
        }

        private static Sprite Load(string key)
        {
            if (Cache.TryGetValue(key, out var s))
                return s;

            s = Resources.Load<Sprite>("LF2/" + key);
            Cache[key] = s;
            return s;
        }
    }
}
