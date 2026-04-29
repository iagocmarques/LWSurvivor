namespace Project.Core.Tick
{
    public readonly struct TickContext
    {
        public readonly long Tick;
        public readonly float FixedDelta;
        public readonly double UnscaledNow;
        public readonly double DriftSeconds;
        public readonly double AccumulatorSeconds;
        public readonly int TicksThisFrame;

        public TickContext(
            long tick,
            float fixedDelta,
            double unscaledNow,
            double driftSeconds,
            double accumulatorSeconds,
            int ticksThisFrame)
        {
            Tick = tick;
            FixedDelta = fixedDelta;
            UnscaledNow = unscaledNow;
            DriftSeconds = driftSeconds;
            AccumulatorSeconds = accumulatorSeconds;
            TicksThisFrame = ticksThisFrame;
        }
    }
}