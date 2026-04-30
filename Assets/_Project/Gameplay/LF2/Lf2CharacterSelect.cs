using System.Collections.Generic;
using Project.Gameplay.Visual;
using UnityEngine;
using Project.Core.Input;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2CharacterSelect : MonoBehaviour
    {
        public struct CharacterSlot
        {
            public string name;
            public string portraitKey;
            public string datResourcePath;
            public Color placeholderColor;
        }

        private static readonly CharacterSlot[] AllCharacters =
        {
            new CharacterSlot { name = "Davis",     portraitKey = "davis_s",     datResourcePath = "LF2/davis",     placeholderColor = new Color(1f, 0.9f, 0.3f) },
            new CharacterSlot { name = "Deep",      portraitKey = "deep_s",      datResourcePath = "LF2/deep",      placeholderColor = new Color(0.3f, 0.5f, 1f) },
            new CharacterSlot { name = "John",      portraitKey = "john_s",      datResourcePath = "LF2/john",      placeholderColor = new Color(0.8f, 0.6f, 0.2f) },
            new CharacterSlot { name = "Henry",     portraitKey = "henry_s",     datResourcePath = "LF2/henry",     placeholderColor = new Color(0.4f, 0.8f, 0.4f) },
            new CharacterSlot { name = "Rudolf",    portraitKey = "rudolf_s",    datResourcePath = "LF2/rudolf",    placeholderColor = new Color(0.8f, 0.3f, 0.3f) },
            new CharacterSlot { name = "Louis",     portraitKey = "louis_s",     datResourcePath = "LF2/louis",     placeholderColor = new Color(0.6f, 0.6f, 0.8f) },
            new CharacterSlot { name = "Firen",     portraitKey = "firen_s",     datResourcePath = "LF2/firen",     placeholderColor = new Color(1f, 0.4f, 0.1f) },
            new CharacterSlot { name = "Freeze",    portraitKey = "freeze_s",    datResourcePath = "",              placeholderColor = new Color(0.3f, 0.7f, 1f) },
            new CharacterSlot { name = "Firzen",    portraitKey = "firzen_s",    datResourcePath = "",              placeholderColor = new Color(1f, 0.5f, 0.5f) },
            new CharacterSlot { name = "Dennis",    portraitKey = "dennis_s",    datResourcePath = "",              placeholderColor = new Color(0.5f, 0.9f, 0.5f) },
            new CharacterSlot { name = "Woody",     portraitKey = "woody_s",     datResourcePath = "",              placeholderColor = new Color(0.7f, 0.5f, 0.3f) },
            new CharacterSlot { name = "Justin",    portraitKey = "justin_s",    datResourcePath = "",              placeholderColor = new Color(0.9f, 0.7f, 0.4f) },
            new CharacterSlot { name = "Knight",    portraitKey = "knight_s",    datResourcePath = "",              placeholderColor = new Color(0.5f, 0.5f, 0.6f) },
            new CharacterSlot { name = "Mark",      portraitKey = "mark_s",      datResourcePath = "",              placeholderColor = new Color(0.4f, 0.4f, 0.9f) },
            new CharacterSlot { name = "Jan",       portraitKey = "jan_s",       datResourcePath = "",              placeholderColor = new Color(0.9f, 0.5f, 0.8f) },
            new CharacterSlot { name = "Jack",      portraitKey = "jack_s",      datResourcePath = "",              placeholderColor = new Color(0.6f, 0.3f, 0.3f) },
            new CharacterSlot { name = "Monk",      portraitKey = "monk_s",      datResourcePath = "",              placeholderColor = new Color(0.8f, 0.6f, 0.5f) },
            new CharacterSlot { name = "Sorcerer",  portraitKey = "sorcerer_s",  datResourcePath = "",              placeholderColor = new Color(0.5f, 0.2f, 0.7f) },
            new CharacterSlot { name = "Hunter",    portraitKey = "hunter_s",    datResourcePath = "",              placeholderColor = new Color(0.3f, 0.6f, 0.3f) },
            new CharacterSlot { name = "Julian",    portraitKey = "julian_s",    datResourcePath = "",              placeholderColor = new Color(0.7f, 0.2f, 0.2f) },
            new CharacterSlot { name = "LouisEX",   portraitKey = "louisEX_s",   datResourcePath = "",              placeholderColor = new Color(0.8f, 0.8f, 0.3f) },
            new CharacterSlot { name = "Bandit",    portraitKey = "bandit_s",    datResourcePath = "",              placeholderColor = new Color(0.6f, 0.4f, 0.4f) },
            new CharacterSlot { name = "Bat",       portraitKey = "bat_s",       datResourcePath = "",              placeholderColor = new Color(0.3f, 0.3f, 0.5f) },
            new CharacterSlot { name = "???",       portraitKey = "",            datResourcePath = "",              placeholderColor = new Color(0.3f, 0.3f, 0.3f) },
        };

        private const int Columns = 6;
        private const int Rows = 4;
        private const float CellSize = 56f;
        private const float CellPad = 4f;

        private int _p1Cursor;
        private int _p2Cursor;
        private bool _p1Confirmed;
        private bool _p2Confirmed;

        private readonly Dictionary<string, Sprite> _portraitCache = new Dictionary<string, Sprite>();
        private Texture2D _whiteTex;
        private GUIStyle _titleStyle;
        private GUIStyle _nameStyle;
        private GUIStyle _confirmStyle;
        private GUIStyle _controlsStyle;

        public bool BothConfirmed => _p1Confirmed && _p2Confirmed;
        public bool P1Confirmed => _p1Confirmed;
        public bool P2Confirmed => _p2Confirmed;
        public int P1Index => _p1Cursor;
        public int P2Index => _p2Cursor;
        public string P1CharacterName => AllCharacters[_p1Cursor].name;
        public string P2CharacterName => AllCharacters[_p2Cursor].name;
        public int CharacterCount => AllCharacters.Length;

        public CharacterSlot GetSlot(int index) => AllCharacters[Mathf.Clamp(index, 0, AllCharacters.Length - 1)];

        private void Awake()
        {
            _whiteTex = new Texture2D(1, 1);
            _whiteTex.SetPixel(0, 0, Color.white);
            _whiteTex.Apply();

            PreloadPortraits();
        }

        private void OnDestroy()
        {
            if (_whiteTex != null) Destroy(_whiteTex);
        }

        private void Update()
        {
            if (BothConfirmed) return;

            ReadP1Input();
            ReadP2Input();
        }

        private void ReadP1Input()
        {
            if (_p1Confirmed) return;

            if (Lf2Input.GetKeyDown(KeyCode.LeftArrow))
                MoveCursor(ref _p1Cursor, -1, 0);
            else if (Lf2Input.GetKeyDown(KeyCode.RightArrow))
                MoveCursor(ref _p1Cursor, 1, 0);
            else if (Lf2Input.GetKeyDown(KeyCode.UpArrow))
                MoveCursor(ref _p1Cursor, 0, -1);
            else if (Lf2Input.GetKeyDown(KeyCode.DownArrow))
                MoveCursor(ref _p1Cursor, 0, 1);

            if (Lf2Input.GetKeyDown(KeyCode.J))
                _p1Confirmed = true;
        }

        private void ReadP2Input()
        {
            if (_p2Confirmed) return;

            if (Lf2Input.GetKeyDown(KeyCode.I))
                MoveCursor(ref _p2Cursor, -1, 0);
            else if (Lf2Input.GetKeyDown(KeyCode.L))
                MoveCursor(ref _p2Cursor, 1, 0);
            else if (Lf2Input.GetKeyDown(KeyCode.J))
                MoveCursor(ref _p2Cursor, 0, -1);
            else if (Lf2Input.GetKeyDown(KeyCode.K))
                MoveCursor(ref _p2Cursor, 0, 1);

            if (Lf2Input.GetKeyDown(KeyCode.Semicolon))
                _p2Confirmed = true;
        }

        private void MoveCursor(ref int cursor, int dx, int dy)
        {
            int col = cursor % Columns;
            int row = cursor / Columns;
            col = (col + dx + Columns) % Columns;
            row = (row + dy + Rows) % Rows;
            cursor = row * Columns + col;
            if (cursor >= AllCharacters.Length)
                cursor = AllCharacters.Length - 1;
        }

        private void PreloadPortraits()
        {
            for (int i = 0; i < AllCharacters.Length; i++)
            {
                var key = AllCharacters[i].portraitKey;
                if (string.IsNullOrEmpty(key)) continue;
                if (_portraitCache.ContainsKey(key)) continue;

                var sprite = Resources.Load<Sprite>("LF2/" + key);
                if (sprite != null)
                    _portraitCache[key] = sprite;
            }
        }

        private void OnGUI()
        {
            InitStyles();

            float gridW = Columns * (CellSize + CellPad);
            float gridH = Rows * (CellSize + CellPad);
            float startX = (Screen.width - gridW) * 0.5f;
            float startY = 100f;

            GUI.Label(new Rect(startX, 20f, gridW, 40f), "CHARACTER SELECT", _titleStyle);

            string p1Status = _p1Confirmed ? $"{P1CharacterName} READY" : "P1: Arrows + J";
            string p2Status = _p2Confirmed ? $"{P2CharacterName} READY" : "P2: IJKL + ;";
            GUI.Label(new Rect(startX, 56f, gridW * 0.5f, 20f), p1Status, _nameStyle);
            GUI.Label(new Rect(startX + gridW * 0.5f, 56f, gridW * 0.5f, 20f), p2Status, _nameStyle);

            for (int i = 0; i < AllCharacters.Length; i++)
            {
                int col = i % Columns;
                int row = i / Columns;
                float x = startX + col * (CellSize + CellPad);
                float y = startY + row * (CellSize + CellPad);

                bool isP1 = i == _p1Cursor;
                bool isP2 = i == _p2Cursor;

                DrawCell(x, y, CellSize, i, isP1, isP2);

                if (isP1 || isP2)
                {
                    if (isP1)
                        DrawBorder(x - 1, y - 1, CellSize + 2, CellSize + 2, new Color(0.2f, 0.6f, 1f, 1f), 2);
                    if (isP2)
                        DrawBorder(x - 1, y - 1, CellSize + 2, CellSize + 2, new Color(1f, 0.3f, 0.3f, 1f), isP1 && isP2 ? 2 : 2);
                }
            }

            float nameY = startY + gridH + 10f;
            if (_p1Cursor >= 0 && _p1Cursor < AllCharacters.Length)
                GUI.Label(new Rect(startX, nameY, gridW * 0.5f, 24f), $"P1: {AllCharacters[_p1Cursor].name}", _nameStyle);
            if (_p2Cursor >= 0 && _p2Cursor < AllCharacters.Length)
                GUI.Label(new Rect(startX + gridW * 0.5f, nameY, gridW * 0.5f, 24f), $"P2: {AllCharacters[_p2Cursor].name}", _nameStyle);

            if (BothConfirmed)
            {
                GUI.Label(new Rect(startX, nameY + 30f, gridW, 30f), "FIGHT!", _confirmStyle);
            }
        }

        private void DrawCell(float x, float y, float size, int charIndex, bool isP1, bool isP2)
        {
            var slot = AllCharacters[charIndex];

            Sprite portrait = null;
            if (!string.IsNullOrEmpty(slot.portraitKey))
                _portraitCache.TryGetValue(slot.portraitKey, out portrait);

            if (portrait != null)
            {
                GUI.color = Color.white;
                var tex = portrait.texture;
                var r = portrait.textureRect;
                GUI.DrawTextureWithTexCoords(new Rect(x, y, size, size), tex,
                    new Rect(r.x / tex.width, r.y / tex.height, r.width / tex.width, r.height / tex.height));
            }
            else
            {
                GUI.color = slot.placeholderColor;
                GUI.DrawTexture(new Rect(x, y, size, size), _whiteTex);

                GUI.color = Color.white;
                var labelRect = new Rect(x, y + size * 0.35f, size, 20f);
                var prev = GUI.skin.label.alignment;
                GUI.skin.label.alignment = TextAnchor.MiddleCenter;
                GUI.Label(labelRect, slot.name);
                GUI.skin.label.alignment = prev;
            }

            GUI.color = Color.white;
        }

        private void DrawBorder(float x, float y, float w, float h, Color color, int thickness)
        {
            GUI.color = color;
            GUI.DrawTexture(new Rect(x, y, w, thickness), _whiteTex);
            GUI.DrawTexture(new Rect(x, y + h - thickness, w, thickness), _whiteTex);
            GUI.DrawTexture(new Rect(x, y, thickness, h), _whiteTex);
            GUI.DrawTexture(new Rect(x + w - thickness, y, thickness, h), _whiteTex);
            GUI.color = Color.white;
        }

        private void InitStyles()
        {
            if (_titleStyle != null) return;

            _titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 24,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            _nameStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 14,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };

            _confirmStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 28,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(1f, 0.9f, 0.2f) }
            };

            _controlsStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 11,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = new Color(0.7f, 0.7f, 0.7f) }
            };
        }

        public void ResetSelection()
        {
            _p1Cursor = 0;
            _p2Cursor = 1;
            _p1Confirmed = false;
            _p2Confirmed = false;
        }
    }
}
