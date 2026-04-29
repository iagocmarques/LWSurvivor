// Assets/Game/Data/MoveDefinition.cs
//
// A single move (attack, dash, special, etc) composed of N frames.
// Each frame plays for `durationTicks` at the fixed 60Hz simulation tick.
//
// Maps to LF2 .dat concepts (see Annex A of lf2_assets_guide.md), but EVERY
// VALUE IS OURS — pick numbers that fit our combat feel, not LF2's.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace LF2Game.Data
{
    [CreateAssetMenu(menuName = "LF2Game/Move Definition", fileName = "NewMove")]
    public sealed class MoveDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string id;                       // e.g. "bandit_punch1"
        public string displayName;

        [Header("Entry conditions")]
        [Tooltip("From which states this move can begin.")]
        public List<CharacterState> validEntryStates = new() { CharacterState.Idle };
        [Tooltip("Required input for this move (None = AI-only or transition).")]
        public InputAction inputTrigger = InputAction.None;

        [Header("Frames")]
        public List<FrameDefinition> frames = new();

        [Header("Cancels")]
        [Tooltip("Move that interrupts this if Attack is buffered.")]
        public MoveDefinition cancelOnAttack;
        [Tooltip("Move that interrupts this if Defense is pressed.")]
        public MoveDefinition cancelOnDefense;
        [Tooltip("Move that interrupts this if Jump is buffered.")]
        public MoveDefinition cancelOnJump;
    }

    /// <summary>One simulation tick of a move. 60Hz.</summary>
    [Serializable]
    public struct FrameDefinition
    {
        [Tooltip("Index into CharacterDefinition.frames. The visual.")]
        public int spriteIndex;
        [Tooltip("How many ticks this frame is shown. 60Hz = 1/60s per tick.")]
        public int durationTicks;
        [Tooltip("Velocity impulse applied at frame entry. (x = horizontal, y = vertical, z = depth)")]
        public Vector3 impulse;
        [Tooltip("Hurtboxes (you take damage here). Multiple allowed for limbs.")]
        public HitboxDefinition[] hurtboxes;
        [Tooltip("Hitboxes (you deal damage here). Empty during recovery frames.")]
        public HitboxDefinition[] hitboxes;
        [Tooltip("Sound effect to play at frame entry. Optional.")]
        public AudioClip sound;
        [Tooltip("Stops cancels until this many ticks pass after frame entry.")]
        public int cancelLockoutTicks;
    }

    /// <summary>A single rectangular collision volume in cell-local coords.</summary>
    [Serializable]
    public struct HitboxDefinition
    {
        public HitboxKind kind;
        [Tooltip("Local rect inside the 80x80 cell. Origin = bottom-center (the pivot).")]
        public Rect rect;
        [Header("If hitbox (attack):")]
        public float damage;
        [Tooltip("Knockback velocity applied to victim.")]
        public Vector2 knockback;
        [Tooltip("Frames the victim is in hitstun.")]
        public int hitstunTicks;
        [Tooltip("Frames the WORLD freezes on hit (juice).")]
        public int hitstopTicks;
        [Tooltip("Whether this hit launches the victim into 'falling' state.")]
        public bool causesKnockdown;
    }

    public enum HitboxKind
    {
        Body,      // hurtbox - normal
        BodyHeavy, // hurtbox - armored (resists low-damage attacks)
        Strike,    // hitbox - melee attack
        Grab,      // hitbox - grab attempt
        Projectile // hitbox - on a spawned projectile entity
    }

    public enum CharacterState
    {
        Idle,
        Walk,
        Run,
        Dash,
        Jump,
        Falling,
        Attack,
        Hitstun,
        Knockdown,
        GettingUp,
        Dead
    }

    public enum InputAction
    {
        None,
        Attack,
        Defense,
        Jump,
        SpecialA,    // e.g. forward+attack
        SpecialB,    // e.g. defense+attack
        SpecialC,    // e.g. jump+attack
    }
}
