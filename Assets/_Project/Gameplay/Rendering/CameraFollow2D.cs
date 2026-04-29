using UnityEngine;

namespace Project.Gameplay.Rendering
{
    [RequireComponent(typeof(Camera))]
    public sealed class CameraFollow2D : MonoBehaviour
    {
        [Header("Follow")]
        [SerializeField] private Transform target;
        [SerializeField] private float smoothSpeed = 8f;
        [SerializeField] private Vector2 offset;

        [Header("Arena Bounds")]
        [SerializeField] private float minX = -15f;
        [SerializeField] private float maxX = 15f;
        [SerializeField] private float minY = -10f;
        [SerializeField] private float maxY = 10f;

        private Camera cam;
        private bool hasSearchedForTarget;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            if (cam == null)
                cam = Camera.main;
        }

        private void LateUpdate()
        {
            if (cam == null)
                return;

            if (target == null)
            {
                if (hasSearchedForTarget)
                    return;

                var player = GameObject.Find("Player");
                if (player != null)
                    target = player.transform;

                hasSearchedForTarget = true;

                if (target == null)
                    return;
            }

            Vector3 desiredPosition = target.position + (Vector3)offset;
            desiredPosition.z = transform.position.z;

            Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * cam.aspect;

            smoothed.x = Mathf.Clamp(smoothed.x, minX + halfWidth, maxX - halfWidth);
            smoothed.y = Mathf.Clamp(smoothed.y, minY + halfHeight, maxY - halfHeight);

            transform.position = smoothed;
        }
    }
}
