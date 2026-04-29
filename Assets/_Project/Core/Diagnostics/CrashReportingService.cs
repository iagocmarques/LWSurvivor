using UnityEngine;

namespace Project.Core.Diagnostics
{
    public sealed class CrashReportingService : MonoBehaviour
    {
        [SerializeField] private bool enableCrashHook = true;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            if (FindAnyObjectByType<CrashReportingService>() != null)
                return;

            var go = new GameObject("CrashReportingService");
            DontDestroyOnLoad(go);
            go.AddComponent<CrashReportingService>();
        }

        private void OnEnable()
        {
            if (enableCrashHook)
                Application.logMessageReceived += OnLogMessageReceived;
        }

        private void OnDisable()
        {
            Application.logMessageReceived -= OnLogMessageReceived;
        }

        private void OnLogMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type != LogType.Exception && type != LogType.Error)
                return;

            // Placeholder para integração Sentry/serviço externo.
            Debug.Log($"[CrashReporting] captured type={type} msg={condition}");
        }
    }
}
