using UnityEngine;

namespace Project.Net.Runtime
{
    public sealed class EnemySnapshotReplicator : MonoBehaviour
    {
        #pragma warning disable CS0414
        [SerializeField] private float clientLerp = 0.35f;

        public HordeSnapshot Capture(long tick)
        {
            return new HordeSnapshot { Tick = (int)tick, Enemies = System.Array.Empty<EnemySnapshot>() };
        }

        public void ApplyRemoteSnapshot(HordeSnapshot snapshot)
        {
        }
    }
}
