// Assets/Game/Data/CharacterDefinition.cs
//
// Top-level ScriptableObject describing a playable character or enemy.
// Mirrors the structure proposed in levantamento_e_diretrizes.md §5.
//
// One CharacterDefinition asset per character. References:
//   - The sliced sprite sheet (sprites array, indexed by frame number)
//   - The face/icon sprites (UI)
//   - The full move list (which moves the character can use)
//   - Base stats
//
// Authoring rule: do NOT copy LF2's hitbox values, frame counts, or impulse
// magnitudes verbatim. Use the LF2 sprites as visual reference only and pick
// numbers that fit OUR combat feel.

using System.Collections.Generic;
using UnityEngine;

namespace LF2Game.Data
{
    [CreateAssetMenu(menuName = "LF2Game/Character Definition", fileName = "NewCharacter")]
    public sealed class CharacterDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string id;                    // stable ID used by Registry. e.g. "bandit"
        public string displayName;
        public Sprite portrait;              // 120x120 face
        public Sprite icon;                  // 40x45 selection icon

        [Header("Visual")]
        [Tooltip("Sliced character sheet. Frame N of any move points into this array.")]
        public Sprite[] frames;              // populated by drag/drop after import

        [Header("Base Stats")]
        public float maxHp           = 100f;
        public float walkSpeed       = 3.5f;     // units/sec — NOT copied from LF2
        public float runSpeed        = 6.0f;
        public float jumpVelocity    = 10.0f;
        [Tooltip("How heavy this character is for knockback resistance. 1 = average.")]
        public float poise           = 1.0f;

        [Header("Moveset")]
        public List<MoveDefinition> moves = new();

        [Header("Notes")]
        [TextArea(3, 8)]
        public string designNotes;
    }
}
