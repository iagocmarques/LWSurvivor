using UnityEngine;

namespace Project.Gameplay.Rendering
{
    /// <summary>
    /// Clamps the attached transform to rectangular arena boundaries every LateUpdate.
    /// Also exposes a static helper for other systems (e.g. enemy spawners) to clamp on demand.
    /// </summary>
    public sealed class ArenaBounds : MonoBehaviour
    {
        [SerializeField] private float minX = -15f;
        [SerializeField] private float maxX = 15f;
        [SerializeField] private float minY = -10f;
        [SerializeField] private float maxY = 10f;

        public float MinX => minX;
        public float MaxX => maxX;
        public float MinY => minY;
        public float MaxY => maxY;

        /// <summary>
        /// Updates the arena boundaries at runtime.
        /// </summary>
        public void SetBounds(float newMinX, float newMaxX, float newMinY, float newMaxY)
        {
            minX = newMinX;
            maxX = newMaxX;
            minY = newMinY;
            maxY = newMaxY;
        }

        /// <summary>
        /// Returns a position clamped inside the arena rectangle.
        /// Uses the instance bounds so each ArenaBounds can have different values.
        /// </summary>
        public Vector3 ClampPosition(Vector3 pos)
        {
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            return pos;
        }

        private void LateUpdate()
        {
            transform.position = ClampPosition(transform.position);
        }
    }
}
