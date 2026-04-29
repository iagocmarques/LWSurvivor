using UnityEngine;

namespace Project.UI.Debug
{
    public sealed class FpsHud : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AutoInstall()
        {
            EnsureExists();
        }

        private TextMesh textMesh;
        private MeshRenderer meshRenderer;

        private float accum;
        private int frames;

        private void Awake()
        {
            Object.DontDestroyOnLoad(gameObject);

            var textGo = new GameObject("FPS_Text");
            textGo.transform.SetParent(transform, false);

            textMesh = textGo.AddComponent<TextMesh>();
            textMesh.text = "FPS: ...";
            textMesh.fontSize = 48;
            textMesh.characterSize = 0.06f;
            textMesh.anchor = TextAnchor.UpperLeft;
            textMesh.alignment = TextAlignment.Left;
            textMesh.color = new Color(1f, 1f, 1f, 0.95f);

            meshRenderer = textGo.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.sortingOrder = short.MaxValue;
            }
        }

        public static FpsHud EnsureExists()
        {
            if (Exists<FpsHud>())
                return FindExisting();

            var go = new GameObject("DebugHUD_FPS");
            Object.DontDestroyOnLoad(go);
            return go.AddComponent<FpsHud>();
        }

        private static bool Exists<T>() where T : Object
        {
            var all = Resources.FindObjectsOfTypeAll<T>();
            return all != null && all.Length > 0;
        }

        private static FpsHud FindExisting()
        {
            var all = Resources.FindObjectsOfTypeAll<FpsHud>();
            return (all != null && all.Length > 0) ? all[0] : null;
        }

        private void LateUpdate()
        {
            var cam = Camera.main;
            if (cam == null)
                cam = Object.FindAnyObjectByType<Camera>();

            if (cam != null && cam.orthographic)
            {
                var size = cam.orthographicSize;
                var halfW = size * cam.aspect;

                var pos = cam.transform.position;
                pos += cam.transform.right * (-halfW + 0.25f);
                pos += cam.transform.up * (size - 0.25f);
                pos.z = 0f;

                if (textMesh != null)
                    textMesh.transform.position = pos;
            }
        }

        private void Update()
        {
            accum += Time.unscaledDeltaTime;
            frames++;

            if (accum < 0.25f)
                return;

            var fps = frames / accum;
            var ms = 1000f / Mathf.Max(1f, fps);

            if (textMesh != null)
                textMesh.text = $"FPS: {fps:0}  ({ms:0.0} ms)";

            accum = 0f;
            frames = 0;
        }
    }
}