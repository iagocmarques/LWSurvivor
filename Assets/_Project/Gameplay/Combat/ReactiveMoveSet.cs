using System.Linq;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    public enum ReactiveStateId : byte
    {
        Defend = 0,
        DefendBreak = 1,
        DefendHit = 2,
        HurtGrounded = 3,
        HurtAir = 4,
        Lying = 5,
        GetUp = 6,
        Dead = 7
    }

    [System.Serializable]
    public struct ReactiveFrameDefinition
    {
        [Min(0)] public int picId;
        [Min(0)] public int durationTicks;
        public bool lockInput;
        public bool invulnerable;
        public Vector2 impulse;
    }

    [System.Serializable]
    public class ReactiveMoveDefinition
    {
        public ReactiveStateId stateId;
        public ReactiveFrameDefinition[] frames;
        public bool loop;
        public ReactiveStateId nextStateOnFinish;

        public int TotalDurationTicks => frames != null && frames.Length > 0
            ? frames.Sum(f => f.durationTicks)
            : 0;
    }

    [CreateAssetMenu(fileName = "ReactiveMoveSet_Default", menuName = "_Project/Combat/Reactive Move Set", order = 1)]
    public sealed class ReactiveMoveSet : ScriptableObject
    {
        [Header("Reactive Moves")]
        public ReactiveMoveDefinition defend;
        public ReactiveMoveDefinition defendBreak;
        public ReactiveMoveDefinition defendHit;
        public ReactiveMoveDefinition hurtGrounded;
        public ReactiveMoveDefinition hurtAir;
        public ReactiveMoveDefinition lying;
        public ReactiveMoveDefinition getUp;
        public ReactiveMoveDefinition dead;

        public ReactiveMoveDefinition GetMove(ReactiveStateId id)
        {
            return id switch
            {
                ReactiveStateId.Defend => defend,
                ReactiveStateId.DefendBreak => defendBreak,
                ReactiveStateId.DefendHit => defendHit,
                ReactiveStateId.HurtGrounded => hurtGrounded,
                ReactiveStateId.HurtAir => hurtAir,
                ReactiveStateId.Lying => lying,
                ReactiveStateId.GetUp => getUp,
                ReactiveStateId.Dead => dead,
                _ => null
            };
        }
    }
}
