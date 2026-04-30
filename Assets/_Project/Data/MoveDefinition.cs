using System.Collections.Generic;
using UnityEngine;

namespace Project.Data
{
    public enum MoveKind
    {
        Active,
        Reactive
    }

    [CreateAssetMenu(fileName = "NewMove", menuName = "_Project/Data/Move Definition", order = 1)]
    public sealed class MoveDefinition : ScriptableObject
    {
        [Header("Identity")]
        public string moveName;
        public MoveKind moveKind;

        [Header("LF2 Frame Chain")]
        [Tooltip("First frame ID in the sequence. Follow 'next' field until 999 or loop.")]
        public int startFrameId;

        [Header("Input Command (for Active moves)")]
        [Tooltip("LF2 input command that triggers this move. Empty for reactive moves.")]
        public Lf2InputCommand inputCommand;

        [Header("State (for Reactive moves)")]
        [Tooltip("LF2 state ID that triggers this move (e.g., 7=Defend, 11=Injured).")]
        public int reactiveStateId;

        [Header("Metadata")]
        [Tooltip("MP cost for specials. 0 for normal moves.")]
        public int mpCost;
        [Tooltip("Whether this move can be cancelled into other moves.")]
        public bool isCancellable;
        [Tooltip("Frame range where cancellation is allowed (inclusive). -1 = not cancellable.")]
        public int cancelStartFrame = -1;
        public int cancelEndFrame = -1;
    }

    public enum Lf2InputCommand
    {
        None = 0,
        Attack,
        Jump,
        Defend,
        DefendForwardAttack,
        DefendForwardJump,
        DefendUpAttack,
        DefendUpJump,
        DefendDownAttack,
        DefendDownJump,
        JumpAttack,
        RunAttack,
        RunJump,
    }
}
