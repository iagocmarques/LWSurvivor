using UnityEngine;

namespace Project.Gameplay.Combat
{
    // ------------------------------------------------------------------
    // StatusEffect enum — moved from HitResult.cs to its own file.
    // ------------------------------------------------------------------

    /// <summary>
    /// Efeito de status aplicado por um golpe (itr.effect).
    /// </summary>
    public enum StatusEffect : byte
    {
        None = 0,
        Blood = 1,
        Burn = 2,
        Freeze = 3
    }

    // ------------------------------------------------------------------
    // StatusEffectInstance — active effect state on a character.
    // ------------------------------------------------------------------

    /// <summary>
    /// Represents a single active status effect on a character.
    /// All timers are in ticks (60 Hz). No Time.deltaTime usage.
    /// </summary>
    public struct StatusEffectInstance
    {
        public StatusEffect Effect;
        public int RemainingTicks;
        public int DamagePerTick;
        public int TickCounter;
        public int DamageIntervalTicks;

        /// <summary>True while the effect has remaining duration.</summary>
        public readonly bool IsActive => RemainingTicks > 0 && Effect != StatusEffect.None;
    }

    // ------------------------------------------------------------------
    // StatusEffectProcessor — static tick-based processor.
    // ------------------------------------------------------------------

    /// <summary>
    /// Static processor for applying and ticking status effects.
    /// All durations are in ticks at 60 Hz.
    /// </summary>
    public static class StatusEffectProcessor
    {
        /// <summary>
        /// Create a new StatusEffectInstance from tuning parameters.
        /// </summary>
        /// <param name="effect">Type of effect to apply.</param>
        /// <param name="durationTicks">Duration in ticks (60 Hz).</param>
        /// <param name="damagePerTick">Damage per application (burn only).</param>
        /// <param name="damageIntervalTicks">Ticks between damage applications (burn only).</param>
        /// <returns>A configured StatusEffectInstance ready to tick.</returns>
        public static StatusEffectInstance Apply(
            StatusEffect effect,
            int durationTicks,
            int damagePerTick = 0,
            int damageIntervalTicks = 10)
        {
            return new StatusEffectInstance
            {
                Effect = effect,
                RemainingTicks = durationTicks,
                DamagePerTick = damagePerTick,
                TickCounter = 0,
                DamageIntervalTicks = Mathf.Max(1, damageIntervalTicks)
            };
        }

        /// <summary>
        /// Convenience overload: create an instance from a StatusEffectTuning asset.
        /// </summary>
        public static StatusEffectInstance ApplyFromTuning(StatusEffect effect, StatusEffectTuning tuning)
        {
            if (tuning == null)
                return Apply(effect, 60); // 1-second fallback

            return effect switch
            {
                StatusEffect.Burn => Apply(effect, tuning.burnDurationTicks,
                                           tuning.burnDamagePerTick, tuning.burnDamageIntervalTicks),
                StatusEffect.Freeze => Apply(effect, tuning.freezeDurationTicks),
                StatusEffect.Blood => Apply(effect, tuning.bloodDurationTicks),
                _ => default
            };
        }

        /// <summary>
        /// Tick an active effect instance. Call once per FixedUpdate (60 Hz).
        /// Handles visual tinting on the SpriteRenderer and invokes the
        /// damage callback for burn effects.
        /// </summary>
        /// <param name="instance">The effect instance (passed by ref, mutated).</param>
        /// <param name="sr">Target SpriteRenderer for visual tinting.</param>
        /// <param name="originalTint">The sprite's color before the effect was applied.</param>
        /// <param name="onDamage">Callback invoked with damage amount for burn ticks. May be null.</param>
        /// <returns>True while the effect is still active; false when it has expired.</returns>
        public static bool Tick(
            ref StatusEffectInstance instance,
            SpriteRenderer sr,
            Color originalTint,
            System.Action<int> onDamage = null)
        {
            if (!instance.IsActive)
            {
                ClearVisual(sr, originalTint);
                return false;
            }

            // --- Visual tint ---
            ApplyVisualTint(instance.Effect, sr);

            // --- Burn damage ---
            if (instance.Effect == StatusEffect.Burn && instance.DamagePerTick > 0)
            {
                instance.TickCounter++;
                if (instance.TickCounter >= instance.DamageIntervalTicks)
                {
                    instance.TickCounter = 0;
                    onDamage?.Invoke(instance.DamagePerTick);
                }
            }

            // --- Decrement ---
            instance.RemainingTicks--;

            // If we just expired, clear the visual.
            if (instance.RemainingTicks <= 0)
            {
                ClearVisual(sr, originalTint);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Force-clear the effect and reset the sprite tint.
        /// </summary>
        public static void ClearVisual(SpriteRenderer sr, Color originalTint)
        {
            if (sr != null)
                sr.color = originalTint;
        }

        // ------------------------------------------------------------------
        // Internal helpers
        // ------------------------------------------------------------------

        private static void ApplyVisualTint(StatusEffect effect, SpriteRenderer sr)
        {
            if (sr == null)
                return;

            // Get the tint color from a small lookup.
            // We don't have access to tuning here (static class, no SO reference),
            // so we use reasonable hardcoded tints for the visual pass.
            // Consumers that need tuning-accurate colors should pass them separately.
            Color tint = effect switch
            {
                StatusEffect.Burn => new Color(1f, 0.6f, 0.2f, 1f),   // orange
                StatusEffect.Freeze => new Color(0.4f, 0.8f, 1f, 1f), // cyan
                StatusEffect.Blood => new Color(0.8f, 0.1f, 0.1f, 1f),// red
                _ => sr.color
            };

            sr.color = tint;
        }
    }

    // ------------------------------------------------------------------
    // StatusEffectTuning — ScriptableObject for designer-configurable values.
    // ------------------------------------------------------------------

    /// <summary>
    /// ScriptableObject holding tunable values for all status effects.
    /// Create via Assets > Create > Project > Combat > Status Effect Tuning.
    /// </summary>
    [CreateAssetMenu(
        fileName = "StatusEffectTuning",
        menuName = "Project/Combat/Status Effect Tuning",
        order = 0)]
    public sealed class StatusEffectTuning : ScriptableObject
    {
        [Header("Burn")]
        [Tooltip("Duration in ticks at 60 Hz. 120 = 2 seconds.")]
        public int burnDurationTicks = 120;

        [Tooltip("Damage applied per burn tick event.")]
        public int burnDamagePerTick = 1;

        [Tooltip("Ticks between each damage application. 10 = 6 dmg/sec at 60 Hz.")]
        public int burnDamageIntervalTicks = 10;

        public Color burnTint = new Color(1f, 0.6f, 0.2f, 1f);

        [Header("Freeze")]
        [Tooltip("Duration in ticks at 60 Hz. 90 = 1.5 seconds.")]
        public int freezeDurationTicks = 90;

        [Tooltip("Movement speed multiplier while frozen.")]
        public float freezeSpeedMultiplier = 0.3f;

        public Color freezeTint = new Color(0.4f, 0.8f, 1f, 1f);

        [Header("Blood")]
        [Tooltip("Duration in ticks at 60 Hz. 30 = 0.5 seconds.")]
        public int bloodDurationTicks = 30;

        public Color bloodTint = new Color(0.8f, 0.1f, 0.1f, 1f);
    }
}
