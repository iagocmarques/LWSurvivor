using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Visual
{
    public static class Lf2VisualLibrary
    {
        private static readonly Dictionary<string, Sprite> Cache = new Dictionary<string, Sprite>();
        private static readonly Dictionary<string, Sprite[]> SheetCache = new Dictionary<string, Sprite[]>();
        private static readonly string[] BackgroundKeys =
        {
            "bg_pic1_alpha",
            "bg_template1_pic1_alpha",
            "bg_template2_pic1_alpha",
            "bg_template3_pic1_alpha"
        };

        public static Sprite GetPlayerSprite()
        {
            return GetCharacterFrame("player_davis_0_alpha", 2);
        }

        public static Sprite GetPlayerFrame(int frameIndex)
        {
            if (frameIndex >= 70)
                return GetCharacterFrame("player_davis_1_alpha", frameIndex - 70);
            return GetCharacterFrame("player_davis_0_alpha", frameIndex);
        }

        public static Sprite GetEnemySprite(string enemyId)
        {
            return GetCharacterFrame(GetEnemySheetKey(enemyId), 0);
        }

        public static Sprite GetEnemyFrame(string enemyId, int frameIndex)
        {
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
            return GetSheetSprites("player_davis_0_alpha").Length
                 + GetSheetSprites("player_davis_1_alpha").Length;
        }

        public static int GetEnemyFrameCount(string enemyId)
        {
            return GetSheetSprites(GetEnemySheetKey(enemyId)).Length
                 + GetSheetSprites(GetEnemySheetKey1(enemyId)).Length;
        }

        /// <summary>
        /// Get a specific frame from a sprite sheet using Unity's pre-configured sprites.
        /// Uses Resources.LoadAll to load all sub-sprites from the sheet, which respects
        /// the .meta sprite definitions without needing Read/Write enabled on the texture.
        /// </summary>
        public static Sprite GetCharacterFrame(string key, int frameIndex)
        {
            var cacheKey = key + "#frame_" + frameIndex;
            if (Cache.TryGetValue(cacheKey, out var cached))
                return cached;

            var sprites = GetSheetSprites(key);
            if (sprites.Length == 0)
                return Load(key);

            var clamped = Mathf.Clamp(frameIndex, 0, sprites.Length - 1);
            var sprite = sprites[clamped];
            Cache[cacheKey] = sprite;
            return sprite;
        }

        private static Sprite[] GetSheetSprites(string key)
        {
            if (SheetCache.TryGetValue(key, out var cached))
                return cached;

            // LoadAll returns sprites configured in .meta (now set to FullRect).
            var sprites = Resources.LoadAll<Sprite>("LF2/" + key);
            if (sprites != null && sprites.Length > 0)
            {
                SheetCache[key] = sprites;
                return sprites;
            }

            // Fallback: create from texture directly if LoadAll fails
            var tex = Resources.Load<Texture2D>("LF2/" + key);
            if (tex != null)
            {
                Debug.LogWarning($"[Lf2VisualLibrary] LoadAll returned 0 sprites for '{key}', using grid fallback");
                sprites = CreateSpritesFromGrid(tex, 80, 80);
                SheetCache[key] = sprites;
                return sprites;
            }

            Debug.LogError($"[Lf2VisualLibrary] Failed to load texture '{key}'");
            var empty = System.Array.Empty<Sprite>();
            SheetCache[key] = empty;
            return empty;
        }

        private static Sprite[] CreateSpritesFromGrid(Texture2D tex, int cellW, int cellH)
        {
            // Sprite sheet is 10 columns x N rows, 80x80 per cell
            var cols = tex.width / cellW;
            var rows = tex.height / cellH;
            var total = cols * rows;
            var result = new Sprite[total];
            var idx = 0;
            // Bottom-up row order (Unity texture coords)
            for (var r = rows - 1; r >= 0; r--)
            {
                for (var c = 0; c < cols; c++)
                {
                    var rect = new Rect(c * cellW, r * cellH, cellW, cellH);
                    result[idx++] = Sprite.Create(tex, rect, new Vector2(0.5f, 0f), 80f, 0u, SpriteMeshType.FullRect);
                }
            }
            return result;
        }

        private static int GetCharacterFrameCount(string key)
        {
            return GetSheetSprites(key).Length;
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
