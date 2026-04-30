using System;
using System.Collections.Generic;
using Project.Core.Input;
using Project.Gameplay.LF2;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Project.UI.Screens
{
    public sealed class CharacterSelectScreen : MonoBehaviour
    {
        private const int GridColumns = 6;

        [Header("Grid")]
        [SerializeField] private Transform characterGrid;
        [SerializeField] private GameObject characterButtonPrefab;

        [Header("Player 1")]
        [SerializeField] private Image p1Preview;
        [SerializeField] private TextMeshProUGUI p1Name;
        [SerializeField] private Image p1CursorOverlay;

        [Header("Player 2")]
        [SerializeField] private Image p2Preview;
        [SerializeField] private TextMeshProUGUI p2Name;
        [SerializeField] private Image p2CursorOverlay;

        [Header("Actions")]
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;

        private readonly List<int> _characterIds = new();
        private readonly List<CharacterButton> _buttons = new();
        private readonly Dictionary<string, Sprite> _portraitCache = new();

        private int _p1Index = -1;
        private int _p2Index = -1;
        private bool _p1Confirmed;
        private bool _p2Confirmed;

        public event Action<int, int> OnConfirmed;
        public event Action OnCancelled;

        public bool BothConfirmed => _p1Confirmed && _p2Confirmed;

        private void Awake()
        {
            if (confirmButton != null)
            {
                confirmButton.interactable = false;
                confirmButton.onClick.AddListener(Confirm);
            }

            if (cancelButton != null)
                cancelButton.onClick.AddListener(Cancel);
        }

        private void Update()
        {
            if (BothConfirmed) return;

            HandleP1Input();
            HandleP2Input();
        }

        public void Initialize(Lf2CharacterDatabase database)
        {
            ClearGrid();

            var ids = database.GetAllIds();
            for (int i = 0; i < ids.Count; i++)
            {
                int id = ids[i];
                _characterIds.Add(id);
                CreateButton(id, database);
            }

            ResetSelection();
        }

        private void CreateButton(int characterId, Lf2CharacterDatabase database)
        {
            var go = Instantiate(characterButtonPrefab, characterGrid);
            var entry = FindEntry(database, characterId);
            string charName = entry.HasValue ? entry.Value.characterName : $"ID {characterId}";

            var btn = new CharacterButton(go, characterId, charName);

            var label = go.GetComponentInChildren<TextMeshProUGUI>();
            if (label != null)
                label.text = charName;

            var button = go.GetComponent<Button>();
            if (button != null)
                button.onClick.AddListener(() => OnGridButtonClicked(characterId));

            _buttons.Add(btn);
        }

        public void ResetSelection()
        {
            _p1Index = _characterIds.Count > 0 ? 0 : -1;
            _p2Index = _characterIds.Count > 1 ? 1 : -1;
            _p1Confirmed = false;
            _p2Confirmed = false;

            UpdatePreviews();
            UpdateButtonHighlights();
            UpdateConfirmButton();
        }

        private void OnGridButtonClicked(int characterId)
        {
            int index = _characterIds.IndexOf(characterId);
            if (index < 0) return;

            if (!_p1Confirmed)
            {
                _p1Index = index;
                _p1Confirmed = true;
            }
            else if (!_p2Confirmed)
            {
                _p2Index = index;
                _p2Confirmed = true;
            }

            UpdatePreviews();
            UpdateButtonHighlights();
            UpdateConfirmButton();
        }

        private void HandleP1Input()
        {
            if (_p1Confirmed || _p1Index < 0) return;

            int prev = _p1Index;

            if (Lf2Input.GetKeyDown(KeyCode.LeftArrow))
                MoveSelection(ref _p1Index, -1, 0);
            else if (Lf2Input.GetKeyDown(KeyCode.RightArrow))
                MoveSelection(ref _p1Index, 1, 0);
            else if (Lf2Input.GetKeyDown(KeyCode.UpArrow))
                MoveSelection(ref _p1Index, 0, -1);
            else if (Lf2Input.GetKeyDown(KeyCode.DownArrow))
                MoveSelection(ref _p1Index, 0, 1);

            if (_p1Index != prev)
            {
                UpdatePreviews();
                UpdateButtonHighlights();
            }

            if (Lf2Input.GetKeyDown(KeyCode.Return) || Lf2Input.GetKeyDown(KeyCode.J))
            {
                _p1Confirmed = true;
                UpdateButtonHighlights();
                UpdateConfirmButton();
            }
        }

        private void HandleP2Input()
        {
            if (_p2Confirmed || _p2Index < 0) return;

            int prev = _p2Index;

            if (Lf2Input.GetKeyDown(KeyCode.A))
                MoveSelection(ref _p2Index, -1, 0);
            else if (Lf2Input.GetKeyDown(KeyCode.D))
                MoveSelection(ref _p2Index, 1, 0);
            else if (Lf2Input.GetKeyDown(KeyCode.W))
                MoveSelection(ref _p2Index, 0, -1);
            else if (Lf2Input.GetKeyDown(KeyCode.S))
                MoveSelection(ref _p2Index, 0, 1);

            if (_p2Index != prev)
            {
                UpdatePreviews();
                UpdateButtonHighlights();
            }

            if (Lf2Input.GetKeyDown(KeyCode.Space))
            {
                _p2Confirmed = true;
                UpdateButtonHighlights();
                UpdateConfirmButton();
            }
        }

        private void MoveSelection(ref int index, int dx, int dy)
        {
            if (_characterIds.Count == 0) return;

            int col = index % GridColumns;
            int row = index / GridColumns;

            col = (col + dx + GridColumns) % GridColumns;
            row += dy;

            int maxRow = (_characterIds.Count - 1) / GridColumns;
            if (row < 0) row = maxRow;
            else if (row > maxRow) row = 0;

            int newIndex = row * GridColumns + col;
            if (newIndex >= _characterIds.Count)
                newIndex = row * GridColumns + (_characterIds.Count - 1) % GridColumns;

            index = Mathf.Clamp(newIndex, 0, _characterIds.Count - 1);
        }

        private void UpdatePreviews()
        {
            UpdatePlayerPreview(_p1Index, p1Preview, p1Name, "P1");
            UpdatePlayerPreview(_p2Index, p2Preview, p2Name, "P2");
        }

        private void UpdatePlayerPreview(int index, Image preview, TextMeshProUGUI nameLabel, string playerPrefix)
        {
            if (index < 0 || index >= _characterIds.Count) return;

            int id = _characterIds[index];
            string charName = GetNameForId(id);

            if (nameLabel != null)
                nameLabel.text = $"{playerPrefix}: {charName}";

            if (preview != null)
            {
                var sprite = LoadPortrait(charName);
                if (sprite != null)
                {
                    preview.sprite = sprite;
                    preview.color = Color.white;
                }
                else
                {
                    preview.sprite = null;
                    preview.color = GetPlaceholderColor(id);
                }
            }
        }

        private void UpdateButtonHighlights()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                var btn = _buttons[i];
                bool isP1 = _p1Index >= 0 && _characterIds[_p1Index] == btn.CharacterId;
                bool isP2 = _p2Index >= 0 && _characterIds[_p2Index] == btn.CharacterId;
                btn.SetHighlight(isP1, isP2);
            }
        }

        private void UpdateConfirmButton()
        {
            if (confirmButton != null)
                confirmButton.interactable = _p1Confirmed && _p2Confirmed;
        }

        public void Confirm()
        {
            if (_p1Index < 0 || _p2Index < 0) return;
            if (!_p1Confirmed || !_p2Confirmed) return;

            OnConfirmed?.Invoke(_characterIds[_p1Index], _characterIds[_p2Index]);
        }

        public void Cancel()
        {
            if (_p2Confirmed)
            {
                _p2Confirmed = false;
                UpdateButtonHighlights();
                UpdateConfirmButton();
                return;
            }

            if (_p1Confirmed)
            {
                _p1Confirmed = false;
                UpdateButtonHighlights();
                UpdateConfirmButton();
                return;
            }

            OnCancelled?.Invoke();
        }

        private void ClearGrid()
        {
            _characterIds.Clear();
            _buttons.Clear();

            if (characterGrid == null) return;

            for (int i = characterGrid.childCount - 1; i >= 0; i--)
                Destroy(characterGrid.GetChild(i).gameObject);
        }

        private Sprite LoadPortrait(string characterName)
        {
            if (string.IsNullOrEmpty(characterName)) return null;

            string key = characterName.ToLowerInvariant();
            if (_portraitCache.TryGetValue(key, out var cached))
                return cached;

            string path = $"LF2/{key}_s";
            var sprite = Resources.Load<Sprite>(path);
            _portraitCache[key] = sprite;
            return sprite;
        }

        private static Color GetPlaceholderColor(int id)
        {
            float hue = (id * 0.137f) % 1f;
            return Color.HSVToRGB(hue, 0.5f, 0.8f);
        }

        private string GetNameForId(int id)
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (_buttons[i].CharacterId == id)
                    return _buttons[i].CharacterName;
            }
            return $"ID {id}";
        }

        private static Lf2CharacterDatabase.CharacterEntry? FindEntry(Lf2CharacterDatabase database, int id)
        {
            for (int i = 0; i < database.characters.Count; i++)
            {
                if (database.characters[i].id == id)
                    return database.characters[i];
            }
            return null;
        }

        private sealed class CharacterButton
        {
            public readonly GameObject Root;
            public readonly int CharacterId;
            public readonly string CharacterName;

            private readonly Image _p1Indicator;
            private readonly Image _p2Indicator;

            public CharacterButton(GameObject root, int characterId, string characterName)
            {
                Root = root;
                CharacterId = characterId;
                CharacterName = characterName;
                _p1Indicator = root.transform.Find("P1Indicator")?.GetComponent<Image>();
                _p2Indicator = root.transform.Find("P2Indicator")?.GetComponent<Image>();
            }

            public void SetHighlight(bool isP1, bool isP2)
            {
                if (_p1Indicator != null)
                    _p1Indicator.enabled = isP1;
                if (_p2Indicator != null)
                    _p2Indicator.enabled = isP2;
            }
        }
    }
}
