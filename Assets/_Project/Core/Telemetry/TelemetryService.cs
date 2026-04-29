using UnityEngine;

namespace Project.Core.Telemetry
{
    public sealed class TelemetryService : MonoBehaviour
    {
        [SerializeField] private bool enableLogs = true;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            if (FindAnyObjectByType<TelemetryService>() != null)
                return;

            var go = new GameObject("TelemetryService");
            DontDestroyOnLoad(go);
            go.AddComponent<TelemetryService>();
        }

        public void TrackRunStart(string mode)
        {
            if (enableLogs)
                Debug.Log($"[Telemetry] RunStart mode={mode}");
        }

        public void TrackRunEnd(string result, float durationSeconds)
        {
            if (enableLogs)
                Debug.Log($"[Telemetry] RunEnd result={result} duration={durationSeconds:0.0}");
        }

        public void TrackUpgradePick(string upgradeId, int level)
        {
            if (enableLogs)
                Debug.Log($"[Telemetry] UpgradePick id={upgradeId} level={level}");
        }

        public void TrackDisconnect(string reason)
        {
            if (enableLogs)
                Debug.Log($"[Telemetry] Disconnect reason={reason}");
        }

        public void TrackEnemyKilled(string enemyId, int totalKills)
        {
            if (enableLogs)
                Debug.Log($"[Telemetry] EnemyKilled id={enemyId} total={totalKills}");
        }
    }
}
