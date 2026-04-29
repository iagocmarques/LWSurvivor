// Assets/Game/Runtime/Tick/TickRunner.cs
//
// Drives the fixed 60Hz simulation. This is the central clock everything in
// gameplay synchronizes to.
//
// Per levantamento_e_diretrizes.md §3.1:
//   "Tick fixo 60Hz para simulação (base do netcode e consistência do combate)"
//
// Why a custom tick loop instead of FixedUpdate?
//   - Decoupled from Unity physics (we don't use Rigidbody for combat).
//   - Deterministic ordering of subscribers (List<ITickable>, indexable).
//   - Single point to insert host-authority gate when we add netcode.
//   - Render-side interpolation via Alpha (0..1) for smooth visuals on >60Hz monitors.
//
// Usage:
//   - Drop one TickRunner GameObject in your boot scene (BootstrapInstaller does this).
//   - Implement ITickable on anything that needs to tick.
//   - Register/Unregister in OnEnable/OnDisable.
//
// Determinism notes (for future netcode):
//   - Tick subscribers should NOT call Random.Range without a seeded RNG.
//   - Tick subscribers should NOT read Time.deltaTime — use TickRunner.TICK_DT.

using System;
using System.Collections.Generic;
using UnityEngine;

namespace LF2Game.Tick
{
    public sealed class TickRunner : MonoBehaviour
    {
        public const float TICK_RATE        = 60f;
        public const float TICK_DT          = 1f / TICK_RATE;
        public const int   MAX_CATCHUP_TICKS = 5;   // cap to avoid spiral-of-death

        public static TickRunner Instance { get; private set; }

        /// <summary>Number of ticks elapsed since boot. Monotonic, never decreases.</summary>
        public int CurrentTick { get; private set; }

        /// <summary>Interpolation factor for render: 0 = previous tick, 1 = current tick.</summary>
        public float Alpha { get; private set; }

        /// <summary>Simulation time in seconds (CurrentTick / 60).</summary>
        public float SimTime => CurrentTick * TICK_DT;

        /// <summary>Fired after every tick, post-Tick of all ITickables.</summary>
        public event Action<int> OnTickAdvance;

        // Two lists so we can iterate stably even if registrations happen during Tick.
        readonly List<ITickable> _tickables       = new();
        readonly List<ITickable> _pendingAdds     = new();
        readonly List<ITickable> _pendingRemoves  = new();
        bool _iterating;

        float _accumulator;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        void Update()
        {
            _accumulator += Time.deltaTime;
            int caught = 0;
            while (_accumulator >= TICK_DT && caught < MAX_CATCHUP_TICKS)
            {
                Step();
                _accumulator -= TICK_DT;
                caught++;
            }
            // If catchup capped, drop the surplus to avoid permanent slowdown.
            if (caught >= MAX_CATCHUP_TICKS) _accumulator = 0f;
            Alpha = Mathf.Clamp01(_accumulator / TICK_DT);
        }

        void Step()
        {
            CurrentTick++;
            _iterating = true;
            for (int i = 0; i < _tickables.Count; i++)
            {
                try { _tickables[i].Tick(CurrentTick); }
                catch (Exception e) { Debug.LogException(e); }
            }
            _iterating = false;
            FlushPending();
            OnTickAdvance?.Invoke(CurrentTick);
        }

        void FlushPending()
        {
            for (int i = 0; i < _pendingRemoves.Count; i++) _tickables.Remove(_pendingRemoves[i]);
            _pendingRemoves.Clear();
            for (int i = 0; i < _pendingAdds.Count; i++)
                if (!_tickables.Contains(_pendingAdds[i])) _tickables.Add(_pendingAdds[i]);
            _pendingAdds.Clear();
        }

        public void Register(ITickable t)
        {
            if (t == null) return;
            if (_iterating) _pendingAdds.Add(t);
            else if (!_tickables.Contains(t)) _tickables.Add(t);
        }

        public void Unregister(ITickable t)
        {
            if (t == null) return;
            if (_iterating) _pendingRemoves.Add(t);
            else _tickables.Remove(t);
        }
    }

    public interface ITickable
    {
        /// <summary>Called once per simulation tick (60Hz).</summary>
        void Tick(int tick);
    }
}
