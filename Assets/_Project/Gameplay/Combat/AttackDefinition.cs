using UnityEngine;

namespace Project.Gameplay.Combat
{
    [System.Serializable]
    public struct HitboxFrameDefinition
    {
        [Min(0)] public int startTick;
        [Min(0)] public int endTick;
        public Vector2 localOffset;
        public Vector2 halfExtents;
        [Min(0)] public int damage;
        public Vector2 knockback;
        [Min(0)] public int hitStopTicks;
        [Min(0f)] public float screenShakeAmplitude;
        public bool isGrab;
    }

    [CreateAssetMenu(fileName = "Attack_Jab", menuName = "_Project/Combat/Attack Definition", order = 0)]
    public sealed class AttackDefinition : ScriptableObject
    {
        [Header("Identity")]
        public CombatAttackId attackId = CombatAttackId.Jab;

        [Header("Timing (ticks @ 60Hz)")]
        [Min(1)] public int durationTicks = 18;
        [Min(0)] public int hitboxStartTick = 3;
        [Min(0)] public int hitboxEndTick = 5;

        [Header("Hitbox (local space, X espelha com facing)")]
        public Vector2 hitboxLocalOffset = new Vector2(0.65f, 0.05f);
        public Vector2 hitboxHalfExtents = new Vector2(0.55f, 0.32f);

        [Header("Hit")]
        [Min(0)] public int damage = 5;
        public Vector2 knockback = new Vector2(5f, 0f);
        [Min(0)] public int hitStopTicks = 2;
        [Min(0f)] public float screenShakeAmplitude = 0.12f;

        [Header("Per-frame hitboxes (v0)")]
        public bool usePerFrameHitboxes = true;
        public HitboxFrameDefinition[] hitboxFrames;

        [Header("Cancel window (ticks @ 60Hz, inclusive)")]
        [Min(0)] public int cancelWindowStartTick = 10;
        [Min(0)] public int cancelWindowEndTick = 14;
        public CombatAttackId[] allowedCancels = { CombatAttackId.Launcher };

        private void OnValidate()
        {
            if (hitboxEndTick < hitboxStartTick)
                hitboxEndTick = hitboxStartTick;

            if (cancelWindowEndTick < cancelWindowStartTick)
                cancelWindowEndTick = cancelWindowStartTick;

            if (durationTicks < 1)
                durationTicks = 1;

            if (hitboxFrames == null || hitboxFrames.Length == 0)
            {
                hitboxFrames = new[]
                {
                    new HitboxFrameDefinition
                    {
                        startTick = hitboxStartTick,
                        endTick = hitboxEndTick,
                        localOffset = hitboxLocalOffset,
                        halfExtents = hitboxHalfExtents,
                        damage = damage,
                        knockback = knockback,
                        hitStopTicks = hitStopTicks,
                        screenShakeAmplitude = screenShakeAmplitude,
                        isGrab = false
                    }
                };
            }
            else
            {
                for (var i = 0; i < hitboxFrames.Length; i++)
                {
                    if (hitboxFrames[i].endTick < hitboxFrames[i].startTick)
                        hitboxFrames[i].endTick = hitboxFrames[i].startTick;
                }
            }
        }

        public bool TryGetActiveHitboxFrame(int attackTick, out HitboxFrameDefinition frame)
        {
            if (usePerFrameHitboxes && hitboxFrames != null)
            {
                for (var i = 0; i < hitboxFrames.Length; i++)
                {
                    var f = hitboxFrames[i];
                    if (attackTick < f.startTick || attackTick > f.endTick)
                        continue;

                    frame = f;
                    return true;
                }
            }

            if (attackTick >= hitboxStartTick && attackTick <= hitboxEndTick)
            {
                frame = new HitboxFrameDefinition
                {
                    startTick = hitboxStartTick,
                    endTick = hitboxEndTick,
                    localOffset = hitboxLocalOffset,
                    halfExtents = hitboxHalfExtents,
                    damage = damage,
                    knockback = knockback,
                    hitStopTicks = hitStopTicks,
                    screenShakeAmplitude = screenShakeAmplitude,
                    isGrab = false
                };
                return true;
            }

            frame = default;
            return false;
        }
    }
}
