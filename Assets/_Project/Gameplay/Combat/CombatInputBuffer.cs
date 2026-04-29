using System;
using System.Collections.Generic;
using Project.Core.Tick;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Registro de um input “bufferizável” com tick de origem.
    /// </summary>
    public readonly struct BufferedCombatAttack
    {
        public readonly CombatAttackId AttackId;
        public readonly long PressedTick;

        public BufferedCombatAttack(CombatAttackId attackId, long pressedTick)
        {
            AttackId = attackId;
            PressedTick = pressedTick;
        }
    }

    /// <summary>
    /// Entrada de debug (ring) independente da expiração do buffer.
    /// </summary>
    public readonly struct CombatInputDebugEntry
    {
        public readonly long Tick;
        public readonly CombatAttackId AttackId;
        public readonly string Label;

        public CombatInputDebugEntry(long tick, CombatAttackId attackId, string label)
        {
            Tick = tick;
            AttackId = attackId;
            Label = label;
        }
    }

    /// <summary>
    /// Buffer FIFO de até <see cref="BufferWindowTicks"/> ticks de idade.
    /// </summary>
    public sealed class CombatInputBuffer
    {
        public const int BufferWindowTicks = 8;

        private readonly List<BufferedCombatAttack> _fifo = new List<BufferedCombatAttack>(16);
        private readonly List<CombatInputDebugEntry> _debugLog = new List<CombatInputDebugEntry>(32);
        private const int DebugLogCap = 32;

        public IReadOnlyList<BufferedCombatAttack> BufferedInputs => _fifo;

        public void Clear()
        {
            _fifo.Clear();
        }

        public void Push(in TickContext ctx, CombatAttackId attackId, string debugLabel)
        {
            if (attackId == CombatAttackId.None)
                return;

            _fifo.Add(new BufferedCombatAttack(attackId, ctx.Tick));

            _debugLog.Add(new CombatInputDebugEntry(ctx.Tick, attackId, debugLabel));
            while (_debugLog.Count > DebugLogCap)
                _debugLog.RemoveAt(0);
        }

        /// <summary>
        /// Remove entradas fora da janela de 8 ticks (chamar 1x por tick fixo).
        /// </summary>
        public void AgeOutExpired(long currentTick)
        {
            for (var i = 0; i < _fifo.Count;)
            {
                var e = _fifo[i];
                if (currentTick - e.PressedTick >= BufferWindowTicks)
                    _fifo.RemoveAt(i);
                else
                    i++;
            }
        }

        /// <summary>
        /// Últimas N entradas de debug (ordem cronológica).
        /// </summary>
        public void GetRecentDebugEntries(int max, List<CombatInputDebugEntry> into)
        {
            into.Clear();
            if (max <= 0)
                return;

            var take = Mathf.Min(max, _debugLog.Count);
            if (take <= 0)
                return;

            var start = _debugLog.Count - take;
            for (var i = start; i < _debugLog.Count; i++)
                into.Add(_debugLog[i]);
        }

        /// <summary>
        /// Consome a entrada bufferizada mais antiga que satisfaz o predicado (FIFO).
        /// </summary>
        public bool TryConsumeOldest(long currentTick, Predicate<CombatAttackId> filter, out BufferedCombatAttack consumed)
        {
            consumed = default;
            for (var i = 0; i < _fifo.Count; i++)
            {
                var e = _fifo[i];
                if (currentTick - e.PressedTick >= BufferWindowTicks)
                    continue;

                if (!filter(e.AttackId))
                    continue;

                consumed = e;
                _fifo.RemoveAt(i);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Apenas olha a mais antiga válida sem consumir.
        /// </summary>
        public bool TryPeekOldest(long currentTick, Predicate<CombatAttackId> filter, out BufferedCombatAttack found)
        {
            found = default;
            for (var i = 0; i < _fifo.Count; i++)
            {
                var e = _fifo[i];
                if (currentTick - e.PressedTick >= BufferWindowTicks)
                    continue;

                if (!filter(e.AttackId))
                    continue;

                found = e;
                return true;
            }

            return false;
        }
    }
}
