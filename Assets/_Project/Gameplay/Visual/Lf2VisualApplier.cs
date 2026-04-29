using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Gameplay.Visual
{
    public sealed class Lf2VisualApplier : MonoBehaviour
    {
        private SpriteRenderer _backgroundRenderer;
        private int _backgroundIndex;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            if (FindAnyObjectByType<Lf2VisualApplier>() != null)
                return;

            var go = new GameObject("Lf2VisualApplier");
            DontDestroyOnLoad(go);
            go.AddComponent<Lf2VisualApplier>().Apply();
        }

        private void Apply()
        {
            ApplyPlayer();
            EnsureBackground();
        }

        private static void ApplyPlayer()
        {
            var player = GameObject.Find("Player");
            if (player == null)
                return;

            var sr = player.GetComponent<SpriteRenderer>();
            if (sr == null)
                sr = player.AddComponent<SpriteRenderer>();

            var sp = Lf2VisualLibrary.GetPlayerSprite();
            if (sp != null)
                sr.sprite = sp;

            if (player.GetComponent<Lf2PlayerSpriteAnimator>() == null)
                player.AddComponent<Lf2PlayerSpriteAnimator>();
        }

        private void EnsureBackground()
        {
            var existing = GameObject.Find("LF2_Background");
            if (existing != null)
            {
                _backgroundRenderer = existing.GetComponent<SpriteRenderer>();
                return;
            }

            var sp = Lf2VisualLibrary.GetBackgroundSprite();
            if (sp == null)
                return;

            var go = new GameObject("LF2_Background");
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sp;
            sr.sortingOrder = -1000;
            go.transform.position = new Vector3(0f, 0f, 20f);
            go.transform.localScale = Vector3.one * 3.5f;
            _backgroundRenderer = sr;
            _backgroundIndex = 0;
        }

        private void Update()
        {
            var kb = Keyboard.current;
            if (kb == null)
                return;

            if (!kb.f8Key.wasPressedThisFrame)
                return;

            if (_backgroundRenderer == null)
                EnsureBackground();

            if (_backgroundRenderer == null || Lf2VisualLibrary.BackgroundCount <= 0)
                return;

            _backgroundIndex = (_backgroundIndex + 1) % Lf2VisualLibrary.BackgroundCount;
            var next = Lf2VisualLibrary.GetBackgroundSpriteByIndex(_backgroundIndex);
            if (next != null)
                _backgroundRenderer.sprite = next;
        }
    }
}
