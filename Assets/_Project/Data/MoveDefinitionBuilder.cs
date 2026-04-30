using System.Collections.Generic;
using Project.Gameplay.LF2;
using UnityEngine;

namespace Project.Data
{
    public static class MoveDefinitionBuilder
    {
        public static List<MoveDefinition> BuildActiveMoves(Lf2CharacterData charData, Lf2FrameRoleIds roleIds)
        {
            var moves = new List<MoveDefinition>();
            if (charData == null || roleIds == null) return moves;

            AddMove(moves, "Jab", Lf2InputCommand.Attack, roleIds.AttackNeutral, charData);
            AddMove(moves, "ForwardAttack", Lf2InputCommand.Attack, roleIds.AttackForward, charData);
            AddMove(moves, "BackAttack", Lf2InputCommand.Attack, roleIds.AttackBack, charData);
            AddMove(moves, "EnergyBlast", Lf2InputCommand.DefendForwardAttack, roleIds.EnergyBlast, charData, roleIds.EnergyBlastMpCost);
            AddMove(moves, "Shrafe", Lf2InputCommand.DefendForwardJump, roleIds.Shrafe, charData, roleIds.ShrafeMpCost);
            AddMove(moves, "DragonPunch", Lf2InputCommand.DefendUpAttack, roleIds.DragonPunch, charData, roleIds.DragonPunchMpCost);
            AddMove(moves, "LeapAttack", Lf2InputCommand.DefendUpJump, roleIds.LeapAttack, charData, roleIds.LeapAttackMpCost);
            AddMove(moves, "DownAttack", Lf2InputCommand.DefendDownAttack, roleIds.DownAttack, charData, roleIds.DownAttackMpCost);
            AddMove(moves, "DownJump", Lf2InputCommand.DefendDownJump, roleIds.DownJump, charData, roleIds.DownJumpMpCost);

            return moves;
        }

        public static List<MoveDefinition> BuildReactiveMoves(Lf2CharacterData charData, Lf2FrameRoleIds roleIds)
        {
            var moves = new List<MoveDefinition>();
            if (charData == null || roleIds == null) return moves;

            AddReactiveMove(moves, "Defend", (int)Lf2State.Defending, roleIds.Defend);
            AddReactiveMove(moves, "HurtGrounded", (int)Lf2State.Injured, FindFirstFrameByState(charData, Lf2State.Injured));
            AddReactiveMove(moves, "HurtAir", (int)Lf2State.FallingAlt, FindFirstFrameByState(charData, Lf2State.FallingAlt));
            AddReactiveMove(moves, "Lying", (int)Lf2State.Lying, roleIds.Lying);
            AddReactiveMove(moves, "BrokenDefend", (int)Lf2State.BrokenDefend, FindFirstFrameByState(charData, Lf2State.BrokenDefend));

            return moves;
        }

        private static void AddMove(List<MoveDefinition> moves, string name, Lf2InputCommand cmd, int startFrame, Lf2CharacterData charData, int mpCost = 0)
        {
            if (startFrame < 0) return;

            var move = ScriptableObject.CreateInstance<MoveDefinition>();
            move.moveName = name;
            move.moveKind = MoveKind.Active;
            move.inputCommand = cmd;
            move.startFrameId = startFrame;
            move.mpCost = mpCost;

            DetectCancelWindow(move, charData, startFrame);

            moves.Add(move);
        }

        private static void AddReactiveMove(List<MoveDefinition> moves, string name, int stateId, int startFrame)
        {
            if (startFrame < 0) return;

            var move = ScriptableObject.CreateInstance<MoveDefinition>();
            move.moveName = name;
            move.moveKind = MoveKind.Reactive;
            move.reactiveStateId = stateId;
            move.startFrameId = startFrame;

            moves.Add(move);
        }

        private static int FindFirstFrameByState(Lf2CharacterData charData, Lf2State state)
        {
            if (charData?.Frames == null) return -1;

            foreach (var kv in charData.Frames)
            {
                if (kv.Value.State == state)
                    return kv.Key;
            }

            return -1;
        }

        private static void DetectCancelWindow(MoveDefinition move, Lf2CharacterData charData, int startFrame)
        {
            if (charData == null || !charData.Frames.ContainsKey(startFrame)) return;

            int frameId = startFrame;
            int frameIndex = 0;
            int attackStart = -1;
            int attackEnd = -1;
            var visited = new HashSet<int>();

            while (frameId >= 0 && frameId != 999 && !visited.Contains(frameId))
            {
                visited.Add(frameId);

                if (charData.Frames.TryGetValue(frameId, out var frame))
                {
                    if (frame.State == Lf2State.Attacking)
                    {
                        if (attackStart < 0) attackStart = frameIndex;
                        attackEnd = frameIndex;
                    }

                    frameId = frame.Next;
                }
                else break;

                frameIndex++;
            }

            if (attackStart >= 0)
            {
                move.isCancellable = true;
                move.cancelStartFrame = attackStart;
                move.cancelEndFrame = attackEnd;
            }
        }
    }
}
