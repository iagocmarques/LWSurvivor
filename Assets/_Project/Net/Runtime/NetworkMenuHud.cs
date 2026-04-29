using UnityEngine;
using Project.Core.Telemetry;
using UnityEngine.InputSystem;

namespace Project.Net.Runtime
{
    public sealed class NetworkMenuHud : MonoBehaviour
    {
        private NetSessionManager _session;
        private SessionRecoveryService _recovery;
        private TelemetryService _telemetry;
        private string _joinAddress = "loopback";
        private bool _show = true;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            if (FindAnyObjectByType<NetworkMenuHud>() != null)
                return;

            var go = new GameObject("NetworkMenuHud");
            DontDestroyOnLoad(go);
            go.AddComponent<NetworkMenuHud>();
        }

        private void Awake()
        {
            _session = FindAnyObjectByType<NetSessionManager>();
            if (_session == null)
            {
                var go = new GameObject("NetSessionManager");
                DontDestroyOnLoad(go);
                _session = go.AddComponent<NetSessionManager>();
            }

            _recovery = FindAnyObjectByType<SessionRecoveryService>();
            _telemetry = FindAnyObjectByType<TelemetryService>();
        }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.f2Key.wasPressedThisFrame)
                _show = !_show;
        }

        private void OnGUI()
        {
            if (!_show || _session == null)
                return;

            GUILayout.BeginArea(new Rect(Screen.width - 320f, 12f, 300f, 240f), GUI.skin.box);
            GUILayout.Label("Net Session (F2 para ocultar)");
            GUILayout.Label($"Role: {_session.Role}");
            GUILayout.Label($"Connected: {_session.IsConnected}");
            GUILayout.Label($"Ping: {_session.SimulatedPingMs} ms");
            GUILayout.Label($"Status: {_session.LastStatusMessage}");
            GUILayout.Space(6f);

            if (GUILayout.Button("Host"))
            {
                _session.StartHost();
                _telemetry?.TrackRunStart("host_mode");
            }

            GUILayout.BeginHorizontal();
            _joinAddress = GUILayout.TextField(_joinAddress);
            if (GUILayout.Button("Join", GUILayout.Width(80)))
            {
                _session.StartClient(_joinAddress);
                _telemetry?.TrackRunStart("client_mode");
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Reconnect"))
                _session.ReconnectLast();

            if (GUILayout.Button("Offline"))
            {
                _session.GoOffline();
                _recovery?.HandleDisconnect("manual_offline");
                _telemetry?.TrackDisconnect("manual_offline");
            }

            GUILayout.EndArea();
        }
    }
}
