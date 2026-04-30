namespace Project.Gameplay.AI
{
    public struct AISensorData
    {
        public float HorizontalDistance;
        public float DepthDifference;
        public bool TargetVisible;
        public bool TargetStunned;
        public bool TargetAirborne;
        public float TimeSinceLastAttack;
    }
}
