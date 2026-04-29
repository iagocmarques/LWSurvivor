// Assets/Game/Runtime/Bootstrap/PixelPerfectCameraSetup.cs
//
// One-shot helper that configures a Camera for crisp pixel-art rendering with
// the LF2 sprite scale.
//
// Per the assets guide §10:
//   "Pixels Per Unit = 80 (1 célula LF2 = 1 unit Unity)
//    Câmera ortográfica com size = 4.375 dá 1280×720 nativo"
//
// REQUIRED PACKAGES (add via Package Manager):
//   - com.unity.render-pipelines.universal  (URP)
//   - com.unity.2d.pixel-perfect             (Pixel Perfect Camera)
//
// HOW TO USE:
//   1. Add this component to your Main Camera.
//   2. Click "Apply Recommended Settings" in the inspector (or it auto-applies in Awake).
//
// If you don't have the Pixel Perfect package installed, the script falls back
// to a manual orthographic configuration that gives the same result for static cameras.

using UnityEngine;

#if PIXEL_PERFECT_2D
using UnityEngine.U2D;
#endif

namespace LF2Game.Bootstrap
{
    [RequireComponent(typeof(Camera))]
    public sealed class PixelPerfectCameraSetup : MonoBehaviour
    {
        [Header("Reference resolution")]
        public int RefResolutionX = 1280;
        public int RefResolutionY = 720;

        [Header("Sprite scale")]
        public int PixelsPerUnit = 80;

        [Header("Apply on Awake?")]
        public bool ApplyOnAwake = true;

        void Awake()
        {
            if (ApplyOnAwake) Apply();
        }

        public void Apply()
        {
            var cam = GetComponent<Camera>();
            cam.orthographic = true;
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = new Color(0.05f, 0.05f, 0.08f, 1f);

#if PIXEL_PERFECT_2D
            // Use Pixel Perfect Camera if available — best results.
            var ppc = GetComponent<PixelPerfectCamera>();
            if (ppc == null) ppc = gameObject.AddComponent<PixelPerfectCamera>();
            ppc.refResolutionX = RefResolutionX;
            ppc.refResolutionY = RefResolutionY;
            ppc.assetsPPU      = PixelsPerUnit;
            ppc.upscaleRT      = false;       // crisp text overlay
            ppc.pixelSnapping  = true;
            ppc.cropFrameX     = false;
            ppc.cropFrameY     = false;
            ppc.stretchFill    = false;
#else
            // Fallback: manual ortho size.
            // size = (RefResolutionY / 2) / PixelsPerUnit
            cam.orthographicSize = (RefResolutionY * 0.5f) / PixelsPerUnit;
#endif
        }

        // Editor preview helper.
        void OnValidate()
        {
            if (Application.isPlaying) return;
            var cam = GetComponent<Camera>();
            if (cam != null) cam.orthographicSize = (RefResolutionY * 0.5f) / Mathf.Max(1, PixelsPerUnit);
        }
    }
}

// NOTE: To enable the Pixel Perfect Camera path, add `PIXEL_PERFECT_2D` to your
// project's Scripting Define Symbols (Project Settings > Player > Scripting Define
// Symbols) AFTER you've installed the `com.unity.2d.pixel-perfect` package.
