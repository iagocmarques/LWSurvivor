using Project.Gameplay.Enemies;
using UnityEngine;

namespace Project.Net.Runtime
{
    public sealed class EnemySnapshotReplicator : MonoBehaviour
    {
        [SerializeField] private float clientLerp = 0.35f;

        private EnemySpawnerDirector _spawner;

        private void ResolveSpawner()
        {
            if (_spawner == null)
                _spawner = FindAnyObjectByType<EnemySpawnerDirector>();
        }

        public HordeSnapshot Capture(long tick)
        {
            ResolveSpawner();
            if (_spawner == null)
                return new HordeSnapshot { Tick = (int)tick, Enemies = new EnemySnapshot[0] };

            var src = _spawner.AliveById;
            var arr = new EnemySnapshot[src.Count];
            var i = 0;
            foreach (var kv in src)
            {
                var e = kv.Value;
                if (e == null)
                    continue;
                arr[i++] = new EnemySnapshot
                {
                    Id = kv.Key,
                    Position = e.transform.position,
                    Hp = 1,
                    Flags = 0
                };
            }

            if (i != arr.Length)
                System.Array.Resize(ref arr, i);

            return new HordeSnapshot
            {
                Tick = (int)tick,
                Enemies = arr
            };
        }

        public void ApplyRemoteSnapshot(HordeSnapshot snapshot)
        {
            ResolveSpawner();
            if (_spawner == null || snapshot.Enemies == null)
                return;

            var alive = _spawner.AliveById;
            for (var i = 0; i < snapshot.Enemies.Length; i++)
            {
                var s = snapshot.Enemies[i];
                if (!alive.TryGetValue(s.Id, out var e) || e == null)
                    continue;

                e.transform.position = Vector3.Lerp(e.transform.position, s.Position, clientLerp);
            }
        }
    }
}
