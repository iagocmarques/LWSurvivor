using Project.Core.Tick;
using UnityEngine;

namespace Project.UI.Debug
{
    public sealed class TickDebugHud : MonoBehaviour, ITickable
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            EnsureExists();
        }

        private TextMesh textMesh;
        private MeshRenderer meshRenderer;

        private long lastTick;
        private double lastDrift;
        private double lastAcc;
        private int lastTicksThisFrame;
        private float fixedDelta;

        public static TickDebugHud EnsureExists()
        {
            var existing = FindExisting();
            if (existing != null)
                return existing;

            var go = new GameObject("DebugHUD_Tick");
            Object.DontDestroyOnLoad(go);
            return go.AddComponent<TickDebugHud>();
        }

        private static TickDebugHud FindExisting()
        {
            var all = Resources.FindObjectsOfTypeAll<TickDebugHud>();
            return (all != null && all.Length > 0) ? all[0] : null;
        }

        private void Awake()
        {
            Object.DontDestroyOnLoad(gameObject);

            var textGo = new GameObject("Tick_Text");
            textGo.transform.SetParent(transform, false);

            textMesh = textGo.AddComponent<TextMesh>();
            textMesh.text = "Tick: ...";
            textMesh.fontSize = 44;
            textMesh.characterSize = 0.055f;
            textMesh.anchor = TextAnchor.UpperLeft;
            textMesh.alignment = TextAlignment.Left;
            textMesh.color = new Color(0.85f, 1f, 0.9f, 0.95f);

            meshRenderer = textGo.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
                meshRenderer.sortingOrder = short.MaxValue;

            fixedDelta = FixedTickSystem.FixedDelta;
        }

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
        }

        public void Tick(in TickContext context)
        {
            lastTick = context.Tick;
            lastDrift = context.DriftSeconds;
            lastAcc = context.AccumulatorSeconds;
            lastTicksThisFrame = context.TicksThisFrame;
            fixedDelta = context.FixedDelta;

            if (textMesh != null)
            {
                var driftMs = lastDrift * 1000.0;
                var accMs = lastAcc * 1000.0;

                textMesh.text =
                    $"Tick: {lastTick}\n" +
                    $"dt: {fixedDelta:0.0000}s  ticks/frame: {lastTicksThisFrame}\n" +
                    $"drift: {driftMs:+0.0;-0.0;0.0} ms  acc: {accMs:0.0} ms";
            }
        }

        private void LateUpdate()
        {
            var cam = Camera.main;
            if (cam == null)
                cam = Object.FindAnyObjectByType<Camera>();

            if (cam == null || !cam.orthographic)
                return;

            var size = cam.orthographicSize;
            var halfW = size * cam.aspect;

            var pos = cam.transform.position;
            pos += cam.transform.right * (-halfW + 0.25f);
            pos += cam.transform.up * (size - 1.05f);
            pos.z = 0f;

            if (textMesh != null)
                textMesh.transform.position = pos;
        }
    }
}