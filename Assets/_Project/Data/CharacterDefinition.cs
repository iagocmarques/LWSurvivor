using Project.Gameplay.Combat;
using Project.Gameplay.LF2;
using UnityEngine;

namespace Project.Data
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "_Project/Data/Character Definition", order = 0)]
    public sealed class CharacterDefinition : ScriptableObject
    {
        [Header("Identity")]
        public int lf2Id;
        public string displayName;
        
        [Header("Movement (from .dat movement params)")]
        public CharacterMovementConfig movement;
        
        [Header("Reactive Moves (defend, hurt, lying, getup, dead)")]
        public ReactiveMoveSet reactiveMoves;
        
        [Header("LF2 Data Reference")]
        [Tooltip("Raw .dat bytes for runtime parsing. Loaded from Lf2CharacterDatabase.")]
        public byte[] rawDatBytes;
        
        [Header("Frame Role IDs (auto-detected from frame data)")]
        public Lf2FrameRoleIds frameRoleIds;
        
        public void BuildFromLf2Data(int id, Lf2CharacterData charData, ReactiveMoveSet reactiveMoves, byte[] datBytes)
        {
            lf2Id = id;
            displayName = charData.Name ?? $"Character_{id}";
            movement = CharacterMovementConfig.FromLf2Movement(charData.Movement);
            this.reactiveMoves = reactiveMoves;
            rawDatBytes = datBytes;
            frameRoleIds = Lf2FrameRoleIds.BuildFromCharacterData(charData);
        }
    }
}
