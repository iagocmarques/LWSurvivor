using Project.Data;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public sealed class DamageReactionRouter
    {
        private readonly CharacterMovementConfig _config;
        private readonly ReactiveMoveSet _moves;
        private readonly Health _health;

        public DamageReactionRouter(CharacterMovementConfig config, ReactiveMoveSet moves, Health health)
        {
            _config = config;
            _moves = moves;
            _health = health;
        }

        public ReactiveStateId Evaluate(in HitResult hit, bool isDefending, bool isAirborne)
        {
            ReactiveStateId result;

            if (_health != null && _health.IsDead)
            {
                result = ReactiveStateId.Dead;
            }
            else if (isDefending && (hit.Damage >= (hit.Bdefend > 0 ? hit.Bdefend : Mathf.RoundToInt(_config.guardBreakThreshold))
                || (hit.Flags & HitFlags.BreaksGuard) != 0))
            {
                result = ReactiveStateId.DefendBreak;
            }
            else if (isAirborne
                || hit.Knockback.y >= _config.airLaunchThreshold
                || (hit.Flags & HitFlags.HighFall) != 0)
            {
                result = ReactiveStateId.HurtAir;
            }
            else if (!isDefending)
            {
                result = ReactiveStateId.HurtGrounded;
            }
            else
            {
                result = ReactiveStateId.DefendHit;
            }

#if UNITY_EDITOR
            Debug.Log($"[ReactionRouter] hit={hit.Damage} fall={hit.Fall} flags={hit.Flags} -> {result}");
#endif

            return result;
        }

        public ReactiveMoveDefinition GetMove(ReactiveStateId id)
        {
            return _moves != null ? _moves.GetMove(id) : null;
        }
    }
}
