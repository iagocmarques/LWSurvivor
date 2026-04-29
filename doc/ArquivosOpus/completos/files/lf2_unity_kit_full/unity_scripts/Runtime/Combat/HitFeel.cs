// Assets/Game/Runtime/Combat/HitFeel.cs
//
// Global hitstop + screen shake. Per levantamento_e_diretrizes.md §4:
//   "Hit stop / screen shake / knockback — Tudo deve ser parametrizável por
//    golpe (dados na hitbox), não hardcoded."
//
// Hitstop is implemented by gating tick advancement: HitFeel.IsHitstopActive
// is checked by every ITickable that runs gameplay logic. The TickRunner
// itself does NOT pause — it keeps counting — but combat code skips its tick
// body. This keeps timing predictable and makes resync trivial.
//
// Screen shake is a position offset applied to the camera in LateUpdate.

using LF2Game.Tick;
using UnityEngine;

namespace LF2Game.Combat
{
    public static class HitFeel
    {
        // ----- Hitstop -----
        static int _hitstopUntilTick;
        public static bool IsHitstopActive
            => TickRunner.Instance != null && TickRunner.Instance.CurrentTick < _hitstopUntilTick;

        public static void RequestHitstop(int durationTicks)
        {
            if (TickRunner.Instance == null) return;
            int target = TickRunner.Instance.CurrentTick + Mathf.Max(0, durationTicks);
            if (target > _hitstopUntilTick) _hitstopUntilTick = target;
        }

        // ----- Screen shake -----
        // Trauma-based shake (Squirrel Eiserloh style): shake intensity = trauma^2.
        static float _trauma;
        public static float TraumaDecayPerSecond = 1.5f;

        public static void RequestShake(float traumaAdd, int durationTicks = 0)
        {
            // durationTicks is informational only; actual decay is by TraumaDecay.
            _trauma = Mathf.Min(1f, _trauma + Mathf.Max(0f, traumaAdd));
        }

        public static float SampleShakeMagnitude()
        {
            return _trauma * _trauma; // squared for snappier feel
        }

        public static void DecayShake(float dt)
        {
            _trauma = Mathf.Max(0f, _trauma - TraumaDecayPerSecond * dt);
        }
    }

    /// <summary>Attach to your camera. Reads HitFeel and offsets the camera.</summary>
    public sealed class CameraShake : MonoBehaviour
    {
        public float MaxOffset      = 0.4f;  // world units
        public float MaxAngleDeg    = 4f;
        public int   NoiseSeed      = 1234;

        Vector3 _basePos;
        Quaternion _baseRot;
        bool _captured;

        void LateUpdate()
        {
            if (!_captured) { _basePos = transform.localPosition; _baseRot = transform.localRotation; _captured = true; }
            HitFeel.DecayShake(Time.deltaTime);

            float mag = HitFeel.SampleShakeMagnitude();
            if (mag <= 0.0001f)
            {
                transform.localPosition = _basePos;
                transform.localRotation = _baseRot;
                return;
            }

            float t = Time.unscaledTime;
            float ox = (Mathf.PerlinNoise(NoiseSeed + 0f, t * 30f) - 0.5f) * 2f * MaxOffset * mag;
            float oy = (Mathf.PerlinNoise(NoiseSeed + 1f, t * 30f) - 0.5f) * 2f * MaxOffset * mag;
            float oa = (Mathf.PerlinNoise(NoiseSeed + 2f, t * 30f) - 0.5f) * 2f * MaxAngleDeg * mag;

            transform.localPosition = _basePos + new Vector3(ox, oy, 0f);
            transform.localRotation = _baseRot * Quaternion.Euler(0f, 0f, oa);
        }
    }
}
