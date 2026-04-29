using UnityEngine;

namespace Project.Gameplay.Combat
{
    using Project.Gameplay.Player;

    /// <summary>
    /// Pure-logic router that decides which reactive state to enter
    /// when the player receives a hit. No MonoBehaviour — injected
    /// with tuning data, the reactive move set, and the health component.
    /// </summary>
    public sealed class DamageReactionRouter
    {
        private readonly PlayerTuning _tuning;
        private readonly ReactiveMoveSet _moves;
        private readonly Health _health;

        public DamageReactionRouter(PlayerTuning tuning, ReactiveMoveSet moves, Health health)
        {
            _tuning = tuning;
            _moves = moves;
            _health = health;
        }

        /// <summary>
        /// Evaluate which reactive state to enter based on the hit and current state.
        /// </summary>
        /// <param name="hit">The hit data.</param>
        /// <param name="isDefending">Whether the character is currently in Defend state.</param>
        /// <param name="isAirborne">Whether the character is currently airborne.</param>
        /// <returns>The reactive state to enter.</returns>
        public ReactiveStateId Evaluate(in HitResult hit, bool isDefending, bool isAirborne)
        {
            // Priority order (from spec):
            // 1. Dead (hp <= 0)
            // 2. DefendBreak (defending + guard break condition)
            // 3. DefendHit (defending + hit doesn't break guard)
            // 4. HurtAir (airborne OR vertical knockback above threshold)
            // 5. HurtGrounded (default)

            ReactiveStateId result;

            if (_health != null && _health.IsDead)
            {
                result = ReactiveStateId.Dead;
            }
            else if (isDefending)
            {
                bool breaksGuard = hit.Damage >= _tuning.guardBreakThreshold
                    || (hit.Flags & HitFlags.BreaksGuard) != 0;
                result = breaksGuard ? ReactiveStateId.DefendBreak : ReactiveStateId.DefendHit;
            }
            else if (isAirborne || hit.Knockback.y >= _tuning.airLaunchThreshold)
            {
                result = ReactiveStateId.HurtAir;
            }
            else
            {
                result = ReactiveStateId.HurtGrounded;
            }

#if UNITY_EDITOR
            Debug.Log($"[ReactionRouter] hit={hit.Damage} flags={hit.Flags} -> {result}");
#endif

            return result;
        }

        /// <summary>
        /// Get the ReactiveMoveDefinition for a given state.
        /// Returns null if the move set is not assigned.
        /// </summary>
        public ReactiveMoveDefinition GetMove(ReactiveStateId id)
        {
            return _moves != null ? _moves.GetMove(id) : null;
        }
    }
}
