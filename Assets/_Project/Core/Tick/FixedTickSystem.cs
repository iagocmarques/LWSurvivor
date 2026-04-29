using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace Project.Core.Tick
{
    public static class FixedTickSystem
    {
        public static bool IsInitialized { get; private set; }

        public static float FixedDelta { get; private set; } = 1f / 60f;
        public static long TickCount { get; private set; }

        public static double StartTime { get; private set; }
        public static double LastTime { get; private set; }
        public static double AccumulatorSeconds { get; private set; }
        public static double DriftSeconds { get; private set; }
        public static int LastTicksThisFrame { get; private set; }

        public static int MaxTicksPerFrame { get; private set; } = 5;
        public static double MaxAccumulatorSeconds { get; private set; } = 0.25;
        public static int RemainingGlobalHitStopTicks { get; private set; }

        private static readonly List<ITickable> Tickables = new List<ITickable>(256);
        private static readonly HashSet<ITickable> TickableSet = new HashSet<ITickable>();

        private static readonly List<ITickable> AddQueue = new List<ITickable>(64);
        private static readonly List<ITickable> RemoveQueue = new List<ITickable>(64);

        private static bool isTicking;

        public static event Action<TickContext> OnAfterTick;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetDomain()
        {
            IsInitialized = false;
            TickCount = 0;
            StartTime = 0;
            LastTime = 0;
            AccumulatorSeconds = 0;
            DriftSeconds = 0;
            LastTicksThisFrame = 0;
            RemainingGlobalHitStopTicks = 0;

            Tickables.Clear();
            TickableSet.Clear();
            AddQueue.Clear();
            RemoveQueue.Clear();
            isTicking = false;
            OnAfterTick = null;
        }

        public static void Configure(
            float fixedDelta,
            int maxTicksPerFrame,
            double maxAccumulatorSeconds)
        {
            FixedDelta = Mathf.Max(0.0001f, fixedDelta);
            MaxTicksPerFrame = Mathf.Clamp(maxTicksPerFrame, 1, 120);
            MaxAccumulatorSeconds = Math.Max(FixedDelta, maxAccumulatorSeconds);
        }

        public static void Initialize()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;

            var now = Time.realtimeSinceStartupAsDouble;
            StartTime = now;
            LastTime = now;
            AccumulatorSeconds = 0;

            var loop = PlayerLoop.GetCurrentPlayerLoop();
            InsertAfter(ref loop, typeof(UnityEngine.PlayerLoop.Update), new PlayerLoopSystem
            {
                type = typeof(FixedTickSystem),
                updateDelegate = PlayerLoopUpdate
            });
            PlayerLoop.SetPlayerLoop(loop);
        }

        public static void Register(ITickable tickable)
        {
            if (tickable == null)
                return;

            if (isTicking)
            {
                if (TickableSet.Contains(tickable))
                    return;

                AddQueue.Add(tickable);
                TickableSet.Add(tickable);
                return;
            }

            if (!TickableSet.Add(tickable))
                return;

            Tickables.Add(tickable);
        }

        public static void Unregister(ITickable tickable)
        {
            if (tickable == null)
                return;

            if (isTicking)
            {
                if (!TickableSet.Contains(tickable))
                    return;

                RemoveQueue.Add(tickable);
                TickableSet.Remove(tickable);
                return;
            }

            if (!TickableSet.Remove(tickable))
                return;

            Tickables.Remove(tickable);
        }

        public static void RequestGlobalHitStop(int ticks)
        {
            if (ticks <= 0)
                return;

            if (ticks > RemainingGlobalHitStopTicks)
                RemainingGlobalHitStopTicks = ticks;
        }

        private static void PlayerLoopUpdate()
        {
            if (!IsInitialized)
                return;

            var now = Time.realtimeSinceStartupAsDouble;
            var frameDelta = now - LastTime;
            LastTime = now;

            if (frameDelta < 0)
                frameDelta = 0;

            var maxFrameDelta = MaxAccumulatorSeconds;
            if (frameDelta > maxFrameDelta)
                frameDelta = maxFrameDelta;

            AccumulatorSeconds += frameDelta;
            if (AccumulatorSeconds > MaxAccumulatorSeconds)
                AccumulatorSeconds = MaxAccumulatorSeconds;

            var fixedDelta = (double)FixedDelta;
            var ticksThisFrame = 0;

            while (AccumulatorSeconds >= fixedDelta && ticksThisFrame < MaxTicksPerFrame)
            {
                TickOnce(now, ticksThisFrame + 1);
                AccumulatorSeconds -= fixedDelta;
                ticksThisFrame++;
            }

            if (ticksThisFrame >= MaxTicksPerFrame && AccumulatorSeconds >= fixedDelta)
            {
                AccumulatorSeconds = 0;
            }

            LastTicksThisFrame = ticksThisFrame;
        }

        private static void TickOnce(double now, int ticksThisFrame)
        {
            isTicking = true;

            ApplyQueues();

            TickCount++;

            DriftSeconds = (now - StartTime) - (TickCount * (double)FixedDelta);

            var context = new TickContext(
                TickCount,
                FixedDelta,
                now,
                DriftSeconds,
                AccumulatorSeconds,
                ticksThisFrame
            );

            if (RemainingGlobalHitStopTicks > 0)
            {
                RemainingGlobalHitStopTicks--;
            }
            else
            {
                var count = Tickables.Count;
                for (var i = 0; i < count; i++)
                {
                    var t = Tickables[i];
                    if (t == null)
                        continue;

                    t.Tick(in context);
                }
            }

            isTicking = false;

            ApplyQueues();

            var evt = OnAfterTick;
            if (evt != null)
                evt(context);
        }

        private static void ApplyQueues()
        {
            if (RemoveQueue.Count > 0)
            {
                for (var i = 0; i < RemoveQueue.Count; i++)
                {
                    var t = RemoveQueue[i];
                    Tickables.Remove(t);
                }
                RemoveQueue.Clear();
            }

            if (AddQueue.Count > 0)
            {
                for (var i = 0; i < AddQueue.Count; i++)
                {
                    var t = AddQueue[i];
                    Tickables.Add(t);
                }
                AddQueue.Clear();
            }
        }

        private static bool InsertAfter(ref PlayerLoopSystem root, Type parentType, PlayerLoopSystem toInsert)
        {
            if (root.subSystemList == null)
                return false;

            for (var i = 0; i < root.subSystemList.Length; i++)
            {
                ref var sys = ref root.subSystemList[i];
                if (sys.type == parentType)
                {
                    var old = sys.subSystemList ?? Array.Empty<PlayerLoopSystem>();
                    var next = new PlayerLoopSystem[old.Length + 1];
                    Array.Copy(old, next, old.Length);
                    next[old.Length] = toInsert;
                    sys.subSystemList = next;
                    return true;
                }

                if (InsertAfter(ref sys, parentType, toInsert))
                    return true;
            }

            return false;
        }
    }
}