using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.UI.Debug
{
    public sealed class PlayerHealthHud : MonoBehaviour
    {
        private Health _health;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            AutoInstallForRuntime();
        }

        public static void AutoInstallForRuntime()
        {
            if (FindAnyObjectByType<PlayerHealthHud>() != null)
                return;

            var go = new GameObject("PlayerHealthHud");
            DontDestroyOnLoad(go);
            go.AddComponent<PlayerHealthHud>();
        }

        private void ResolveHealth()
        {
            if (_health != null)
                return;

            var p = GameObject.Find("Player");
            if (p != null)
                _health = p.GetComponent<Health>();
        }

        private void OnGUI()
        {
            ResolveHealth();
            if (_health == null)
                return;

            GUILayout.BeginArea(new Rect(12f, 12f, 240f, 90f), GUI.skin.box);
            GUILayout.Label("Player");
            GUILayout.Label($"HP: {_health.CurrentHealth}/{_health.MaxHealth}");
            GUILayout.EndArea();
        }
    }
}
