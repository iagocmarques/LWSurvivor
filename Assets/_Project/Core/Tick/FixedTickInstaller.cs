using UnityEngine;

namespace Project.Core.Tick
{
    public sealed class FixedTickInstaller : MonoBehaviour
    {
        [SerializeField] private int tickRate = 60;
        [SerializeField] private int maxTicksPerFrame = 5;
        [SerializeField] private double maxAccumulatorSeconds = 0.25;

        private void Awake()
        {
            var dt = 1f / Mathf.Max(1, tickRate);

            Time.fixedDeltaTime = dt;

            FixedTickSystem.Configure(dt, maxTicksPerFrame, maxAccumulatorSeconds);
            FixedTickSystem.Initialize();
        }
    }
}