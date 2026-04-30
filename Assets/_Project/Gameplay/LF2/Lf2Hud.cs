using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2Hud : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Mana mana;
        [SerializeField] private string characterName = "Davis";
        [SerializeField] private int playerIndex;

        private Texture2D _whiteTex;
        private float _displayHpRatio = 1f;
        private float _displayMpRatio = 1f;

        private static readonly Color HpColor = new Color(0.9f, 0.1f, 0.1f);
        private static readonly Color HpBgColor = new Color(0.3f, 0.05f, 0.05f);
        private static readonly Color MpColor = new Color(0.2f, 0.4f, 0.9f);
        private static readonly Color MpBgColor = new Color(0.05f, 0.1f, 0.3f);
        private static readonly Color TextColor = Color.white;

        private const float BarWidth = 200f;
        private const float BarHeight = 12f;
        private const float BarSpacing = 2f;
        private const float AnimSpeed = 5f;

        private GUIStyle _nameStyle;

        public void Bind(Health h, Mana m, string name, int index)
        {
            health = h;
            mana = m;
            characterName = name;
            playerIndex = index;
            if (health != null)
                _displayHpRatio = health.CurrentHealth / (float)health.MaxHealth;
            if (mana != null)
                _displayMpRatio = mana.CurrentMana / (float)mana.MaxMana;
        }

        private void Awake()
        {
            _whiteTex = new Texture2D(1, 1);
            _whiteTex.SetPixel(0, 0, Color.white);
            _whiteTex.Apply();
        }

        private void Update()
        {
            if (health != null)
            {
                float target = health.CurrentHealth / (float)health.MaxHealth;
                _displayHpRatio = Mathf.MoveTowards(_displayHpRatio, target, AnimSpeed * Time.deltaTime);
            }

            if (mana != null)
            {
                float target = mana.CurrentMana / (float)mana.MaxMana;
                _displayMpRatio = Mathf.MoveTowards(_displayMpRatio, target, AnimSpeed * Time.deltaTime);
            }
        }

        private void OnGUI()
        {
            if (health == null) return;

            if (_nameStyle == null)
            {
                _nameStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 14,
                    fontStyle = FontStyle.Bold,
                    normal = { textColor = TextColor }
                };
            }

            float x = playerIndex == 0 ? 10f : Screen.width - BarWidth - 10f;
            float y = 10f;

            GUI.Label(new Rect(x, y, BarWidth, 20f), characterName, _nameStyle);
            y += 20f;

            DrawBar(x, y, BarWidth, BarHeight, _displayHpRatio, HpColor, HpBgColor);
            y += BarHeight + BarSpacing;

            if (mana != null)
                DrawBar(x, y, BarWidth, BarHeight, _displayMpRatio, MpColor, MpBgColor);
        }

        private void DrawBar(float x, float y, float w, float h, float ratio, Color fill, Color bg)
        {
            GUI.color = bg;
            GUI.DrawTexture(new Rect(x, y, w, h), _whiteTex);

            GUI.color = fill;
            GUI.DrawTexture(new Rect(x, y, w * Mathf.Clamp01(ratio), h), _whiteTex);

            GUI.color = Color.white;
        }

        private void OnDestroy()
        {
            if (_whiteTex != null)
                Destroy(_whiteTex);
        }
    }
}
