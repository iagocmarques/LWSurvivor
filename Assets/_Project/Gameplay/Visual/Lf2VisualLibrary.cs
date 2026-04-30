using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Visual
{
    public static class Lf2VisualLibrary
    {
        private static readonly Dictionary<long, Sprite> FrameCache = new Dictionary<long, Sprite>(256);
        private static readonly Dictionary<string, Sprite> Cache = new Dictionary<string, Sprite>();
        private static readonly Dictionary<string, Sprite[]> SheetCache = new Dictionary<string, Sprite[]>();
        private static readonly string[] BackgroundKeys =
        {
            "bg_pic1_alpha",
            "bg_template1_pic1_alpha",
            "bg_template2_pic1_alpha",
            "bg_template3_pic1_alpha"
        };

        private static string _playerSheet0 = "davis_0_alpha";
        private static string _playerSheet1 = "davis_1_alpha";
        private static string _playerSheet2 = "davis_2_alpha";
        private static string _playerName = "davis";

        public static void SetPlayerCharacter(string characterName)
        {
            _playerName = characterName ?? "davis";
            _playerSheet0 = _playerName + "_0_alpha";
            _playerSheet1 = _playerName + "_1_alpha";
            _playerSheet2 = _playerName + "_2_alpha";

            for (long i = 0; i < 210; i++)
                FrameCache.Remove(i);

            foreach (var k in new[] { _playerSheet0, _playerSheet1, _playerSheet2 })
                SheetCache.Remove(k);
        }

        public static string PlayerCharacterName => _playerName;

        public static Sprite GetPlayerSprite()
        {
            return GetCharacterFrame(_playerSheet0, 2);
        }

        public static Sprite GetPlayerFrame(int frameIndex)
        {
            long key = frameIndex;
            if (FrameCache.TryGetValue(key, out var cached))
                return cached;

            Sprite sprite;
            if (frameIndex >= 140)
                sprite = GetCharacterFrameInternal(_playerSheet2, frameIndex - 140);
            else if (frameIndex >= 70)
                sprite = GetCharacterFrameInternal(_playerSheet1, frameIndex - 70);
            else
                sprite = GetCharacterFrameInternal(_playerSheet0, frameIndex);

            FrameCache[key] = sprite;
            return sprite;
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
            return GetSheetSprites(_playerSheet0).Length
                 + GetSheetSprites(_playerSheet1).Length
                 + GetSheetSprites(_playerSheet2).Length;
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
            long cacheKey = ((long)key.GetHashCode() << 16) | (long)(frameIndex & 0xFFFF);
            if (FrameCache.TryGetValue(cacheKey, out var cached))
                return cached;

            var sprite = GetCharacterFrameInternal(key, frameIndex);
            FrameCache[cacheKey] = sprite;
            return sprite;
        }

        private static Sprite GetCharacterFrameInternal(string key, int frameIndex)
        {
            var sprites = GetSheetSprites(key);
            if (sprites.Length == 0)
                return Load(key);

            var clamped = Mathf.Clamp(frameIndex, 0, sprites.Length - 1);
            return sprites[clamped];
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
                    return "hunter_0b_alpha";
                if (enemyId.Contains("bruiser"))
                    return "knight_0b_alpha";
                if (enemyId.Contains("mark"))
                    return "mark_0_alpha";
                if (enemyId.Contains("jack"))
                    return "jack_0_alpha";
                if (enemyId.Contains("sorcerer"))
                    return "sorcerer_0_alpha";
                if (enemyId.Contains("monk"))
                    return "monk_0_alpha";
                if (enemyId.Contains("jan"))
                    return "jan_0_alpha";
                if (enemyId.Contains("knight"))
                    return "knight_0_alpha";
                if (enemyId.Contains("bat"))
                    return "bat_0_alpha";
                if (enemyId.Contains("justin"))
                    return "justin_0_alpha";
                if (enemyId.Contains("louisEX"))
                    return "louisEX_0_alpha";
                if (enemyId.Contains("firzen"))
                    return "firzen_0_alpha";
                if (enemyId.Contains("julian"))
                    return "julian_0_alpha";
                if (enemyId.Contains("hunter"))
                    return "hunter_0b_alpha";
            }

            return "bandit_0_alpha";
        }

private static string GetEnemySheetKey1(string enemyId)
        {
            if (!string.IsNullOrWhiteSpace(enemyId))
            {
                if (enemyId.Contains("bandit"))
                    return "bandit_1_alpha";
                if (enemyId.Contains("mark"))
                    return "mark_1_alpha";
                if (enemyId.Contains("jack"))
                    return "jack_1_alpha";
                if (enemyId.Contains("sorcerer"))
                    return "sorcerer_1_alpha";
                if (enemyId.Contains("knight"))
                    return "knight_1_alpha";
                if (enemyId.Contains("bat"))
                    return "bat_1_alpha";
                if (enemyId.Contains("justin"))
                    return "justin_1_alpha";
                if (enemyId.Contains("louisEX"))
                    return "louisEX_1_alpha";
                if (enemyId.Contains("firzen"))
                    return "firzen_1_alpha";
                if (enemyId.Contains("julian"))
                    return "julian_1_alpha";
            }

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
