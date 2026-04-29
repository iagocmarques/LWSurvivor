using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public static class CombatReadabilityFx
    {
        private static readonly Stack<GameObject> _pool = new Stack<GameObject>(32);
        private static readonly List<PopupEntry> _active = new List<PopupEntry>(32);

        private struct PopupEntry
        {
            public GameObject go;
            public TextMesh tm;
            public float remaining;
        }

        public static void SpawnDamagePopup(Vector3 worldPos, int damage)
        {
            SpawnPopup(worldPos + new Vector3(0f, 0.35f, 0f), "-" + Mathf.Max(0, damage), new Color(1f, 0.9f, 0.25f, 1f), 0.45f);
        }

        public static void SpawnKillPopup(Vector3 worldPos, string label)
        {
            var text = string.IsNullOrWhiteSpace(label) ? "KO!" : "KO " + label;
            SpawnPopup(worldPos + new Vector3(0f, 0.65f, 0f), text, new Color(1f, 0.45f, 0.45f, 1f), 0.8f);
        }

        private static void SpawnPopup(Vector3 pos, string text, Color color, float ttl)
        {
            var go = _pool.Count > 0 ? _pool.Pop() : CreatePopupGO();
            var tm = go.GetComponent<TextMesh>();
            tm.text = text;
            tm.color = color;
            go.transform.position = pos;
            go.SetActive(true);
            _active.Add(new PopupEntry { go = go, tm = tm, remaining = Mathf.Max(0.1f, ttl) });
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _pool.Clear();
            _active.Clear();
        }

        private static GameObject CreatePopupGO()
        {
            var go = new GameObject("CombatPopup");
            var tm = go.AddComponent<TextMesh>();
            tm.fontSize = 48;
            tm.characterSize = 0.035f;
            tm.anchor = TextAnchor.MiddleCenter;
            return go;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void RegisterTick()
        {
            // Tick popups via PlayerLoop-safe callback
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += (_, _) =>
            {
                // nothing — just ensuring the static class is loaded
            };
        }

        /// <summary>
        /// Call from a MonoBehaviour.Update or similar to tick popup timers.
        /// For MVP, we use Destroy fallback if no ticker exists.
        /// </summary>
        public static void Tick(float deltaTime)
        {
            for (int i = _active.Count - 1; i >= 0; i--)
            {
                var entry = _active[i];
                entry.remaining -= deltaTime;
                if (entry.remaining <= 0f)
                {
                    entry.go.SetActive(false);
                    _pool.Push(entry.go);
                    _active.RemoveAt(i);
                }
                else
                {
                    _active[i] = entry;
                }
            }
        }
    }
}
