// Assets/Game/Runtime/Bootstrap/BootstrapInstaller.cs
//
// Drop this on an empty GameObject in your boot scene. It sets up the
// singletons (TickRunner, HitboxResolver) and registers the active Registry.
//
// Order of execution (manual ordering by registration sequence):
//   1. TickRunner exists and ticks at 60Hz.
//   2. PlayerInputReader.Tick      - feeds buffer
//   3. PlayerFSM.Tick              - reads input, submits hitboxes/hurtboxes
//   4. SwarmManager.Tick           - submits hitboxes/hurtboxes
//   5. HitboxResolver.Tick         - resolves all submissions, applies damage
//
// HitboxResolver MUST tick last to see all submissions. We enforce this by
// instantiating it last and relying on the Register order.

using LF2Game.Combat;
using LF2Game.Data;
using LF2Game.Tick;
using UnityEngine;

namespace LF2Game.Bootstrap
{
    [DefaultExecutionOrder(-1000)]
    public sealed class BootstrapInstaller : MonoBehaviour
    {
        [Header("Required references")]
        public Registry GameRegistry;

        [Header("Optional camera shake")]
        public Camera GameCamera;
        public bool   AddCameraShake = true;

        void Awake()
        {
            // Tick runner - must exist before anything else registers.
            if (TickRunner.Instance == null)
            {
                var go = new GameObject("[TickRunner]");
                go.transform.SetParent(transform, false);
                go.AddComponent<TickRunner>();
            }

            // Hitbox resolver - registered AFTER players/swarms register
            // (since gameplay objects register in their OnEnable, which runs
            // after this Awake when scene loads top-down).
            if (HitboxResolver.Instance == null)
            {
                var go = new GameObject("[HitboxResolver]");
                go.transform.SetParent(transform, false);
                go.AddComponent<HitboxResolver>();
            }

            // Activate registry
            if (GameRegistry != null) GameRegistry.Activate();

            // Camera shake
            if (AddCameraShake && GameCamera != null && GameCamera.GetComponent<CameraShake>() == null)
                GameCamera.gameObject.AddComponent<CameraShake>();
        }
    }
}
