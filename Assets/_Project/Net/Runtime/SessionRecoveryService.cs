using Project.Gameplay.Run;
using Project.Core.Telemetry;
using UnityEngine;

namespace Project.Net.Runtime
{
    public sealed class SessionRecoveryService : MonoBehaviour
    {
        private const string KeyPartialCurrency = "run.partial.currency";
        private const string KeyLastDisconnect = "run.last.disconnect";

        private NetSessionManager _session;
        private RunManager _run;
        private TelemetryService _telemetry;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            if (FindAnyObjectByType<SessionRecoveryService>() != null)
                return;

            var go = new GameObject("SessionRecoveryService");
            DontDestroyOnLoad(go);
            go.AddComponent<SessionRecoveryService>();
        }

        private void Awake()
        {
            _session = FindAnyObjectByType<NetSessionManager>();
            _run = FindAnyObjectByType<RunManager>();
            _telemetry = FindAnyObjectByType<TelemetryService>();

            if (_session != null)
                _session.OnConnectionChanged += OnConnectionChanged;
        }

        private void OnDestroy()
        {
            if (_session != null)
                _session.OnConnectionChanged -= OnConnectionChanged;
        }

        public void HandleHostQuit()
        {
            SavePartialProgress(25);
            PlayerPrefs.SetString(KeyLastDisconnect, "host_quit");
            PlayerPrefs.Save();
        }

        public void HandleDisconnect(string reason)
        {
            SavePartialProgress(10);
            PlayerPrefs.SetString(KeyLastDisconnect, reason);
            PlayerPrefs.Save();
            _telemetry?.TrackDisconnect(reason);
        }

        private void OnConnectionChanged(bool connected)
        {
            if (connected)
                return;

            HandleDisconnect("network_lost");
        }

        private static void SavePartialProgress(int currency)
        {
            var current = PlayerPrefs.GetInt(KeyPartialCurrency, 0);
            PlayerPrefs.SetInt(KeyPartialCurrency, current + Mathf.Max(0, currency));
        }

        private void OnGUI()
        {
            var reason = PlayerPrefs.GetString(KeyLastDisconnect, string.Empty);
            if (string.IsNullOrEmpty(reason))
                return;

            GUILayout.BeginArea(new Rect(12f, Screen.height - 110f, 520f, 90f), GUI.skin.box);
            GUILayout.Label($"Ultima desconexao: {reason}");
            GUILayout.Label($"Meta-currency parcial: {PlayerPrefs.GetInt(KeyPartialCurrency, 0)}");
            if (GUILayout.Button("Limpar aviso", GUILayout.Width(160f)))
                PlayerPrefs.DeleteKey(KeyLastDisconnect);
            GUILayout.EndArea();
        }
    }
}
