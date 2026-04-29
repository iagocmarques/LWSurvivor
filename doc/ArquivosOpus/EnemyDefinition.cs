// Assets/Game/Data/EnemyDefinition.cs
//
// Lightweight enemy definition for swarm-style enemies (the "Survivors" layer).
// These are the 200-500 entities the perf section of levantamento_e_diretrizes.md
// targets. Keep this struct SMALL — many enemies live at once.
//
// For boss-tier or hand-authored enemies, use CharacterDefinition + a custom
// AI controller instead.

using UnityEngine;
using System.Collections.Generic;

namespace LF2Game.Data
{
    [CreateAssetMenu(menuName = "LF2Game/Enemy Definition", fileName = "NewEnemy")]
    public sealed class EnemyDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string id;
        public string displayName;

        [Header("Visual")]
        [Tooltip("Sliced sprite sheet. Idle/walk/death frame indices reference into this.")]
        public Sprite[] frames;
        [Tooltip("Frame indices for the idle loop (e.g. {0, 1, 2}).")]
        public int[] idleFrames = { 0 };
        [Tooltip("Frame indices for the walk loop.")]
        public int[] walkFrames = { 0, 1, 2, 3 };
        [Tooltip("Frames per second of the visual animation.")]
        public float animFps = 8f;

        [Header("Stats")]
        public float maxHp        = 10f;
        public float walkSpeed    = 2.5f;
        public float contactDamage = 5f;
        [Tooltip("How many XP orbs/coins drop on death.")]
        public int xpDrop = 1;

        [Header("Behavior")]
        [Tooltip("AI 'think' rate in Hz. Per the doc, swarm should think 10-20Hz, not 60Hz.")]
        [Range(2f, 30f)] public float thinkHz = 12f;
        [Tooltip("Steering aggression: 0 = pure pathfinding, 1 = pure direct chase.")]
        [Range(0f, 1f)] public float chaseDirectness = 0.85f;

        [Header("Drop table")]
        public List<DropEntry> drops = new();
    }

    [System.Serializable]
    public struct DropEntry
    {
        public string itemId;
        [Range(0f, 1f)] public float chance;
    }
}
