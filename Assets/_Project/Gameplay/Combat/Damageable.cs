using Project.Core.Tick;
using Project.Gameplay.Audio;
using Project.Gameplay.Feedback;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    public sealed class Damageable : MonoBehaviour, ICombatHurtbox
    {
        private const float DefendDamageMultiplier = 0.3f;
        private const float DefendKnockbackMultiplier = 0.3f;
        private const float DefendFlashDuration = 0.06f;

        [SerializeField] private bool acceptKnockback = true;
        [SerializeField] private float knockbackScale = 1f;
        [SerializeField] private bool triggerGlobalHitStop = true;
        [SerializeField] private bool triggerScreenShake = true;

        private Health _health;
        private PlayerHsmController _hsm;
        private SpriteRenderer _sr;
        private float _defendFlashLeft;
        private static readonly Color DefendFlashColor = new Color(0.7f, 0.85f, 1f, 1f);

        private void Awake()
        {
            _health = GetComponent<Health>();
            _hsm = GetComponent<PlayerHsmController>();
            _sr = GetComponent<SpriteRenderer>();
        }

        public bool ReceiveHit(in CombatHitInfo hit)
        {
            if (_health == null || _health.IsDead)
                return false;

            bool isDefending = _hsm != null && _hsm.IsDefending;
            int effectiveDamage = hit.Damage;
            float effectiveKnockbackScale = knockbackScale;
            bool isGuardBreak = false;

            if (isDefending)
            {
                isGuardBreak = hit.Bdefend > 0
                    ? hit.Damage >= hit.Bdefend
                    : hit.Damage >= 15f;

                if (!isGuardBreak)
                {
                    effectiveDamage = Mathf.Max(1, Mathf.RoundToInt(hit.Damage * DefendDamageMultiplier));
                    effectiveKnockbackScale *= DefendKnockbackMultiplier;
                    _defendFlashLeft = DefendFlashDuration;
                }
            }

            if (!_health.ApplyDamage(effectiveDamage))
                return false;

            if (acceptKnockback)
            {
                var delta = hit.Knockback * effectiveKnockbackScale * 0.12f;
                transform.position += new Vector3(delta.x, delta.y, 0f);
            }

            if (triggerGlobalHitStop && hit.HitStopTicks > 0)
                FixedTickSystem.RequestGlobalHitStop(hit.HitStopTicks);

            if (triggerScreenShake && hit.ScreenShakeAmplitude > 0f)
                ScreenShake2D.Instance?.Shake(hit.ScreenShakeAmplitude, Mathf.Max(0.08f, hit.HitStopTicks / 60f));

            if (_hsm != null)
            {
                int attackerFacing = 1;
                if (hit.Attacker != null)
                {
                    float dx = hit.Attacker.transform.position.x - transform.position.x;
                    attackerFacing = dx >= 0f ? 1 : -1;
                }

                var flags = HitFlags.None;
                if (hit.Damage >= 10)
                    flags |= HitFlags.IsHeavy;
                if (hit.Fall >= 20)
                    flags |= HitFlags.HighFall;
                if (isGuardBreak)
                    flags |= HitFlags.BreaksGuard;

                var hitResult = HitResult.FromCombatHitInfo(
                    in hit,
                    attackerFacing,
                    fall: hit.Fall,
                    bdefend: hit.Bdefend,
                    effect: hit.Effect,
                    flags: flags);

                _hsm.ApplyReactiveHit(in hitResult);
            }

            if (effectiveDamage > 0)
                CombatReadabilityFx.SpawnDamagePopup(transform.position, effectiveDamage);

            var audio = Lf2AudioManager.Instance;
            if (audio != null)
            {
                if (isDefending && effectiveDamage < hit.Damage)
                    audio.PlaySfx(Lf2SoundId.DefendBlock, 0.7f);
                else if (isGuardBreak)
                    audio.PlaySfx(Lf2SoundId.DefendBreak, 1f);
            }

            return true;
        }

        private void Update()
        {
            if (_defendFlashLeft > 0f)
            {
                _defendFlashLeft -= Time.deltaTime;
                float t = Mathf.Clamp01(_defendFlashLeft / DefendFlashDuration);
                if (_sr != null)
                    _sr.color = Color.Lerp(DefendFlashColor, Color.white, t);
            }
        }
    }
}
