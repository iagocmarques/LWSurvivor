// Assets/Game/Runtime/Movement/CharacterController2_5D.cs
//
// 2.5D movement system per levantamento_e_diretrizes.md §4:
//   "Player/inimigos se movem em X/Y, onde Y é 'profundidade'.
//    Ordenação visual por Y e colisões/hitboxes com tolerância de profundidade
//    ('lane thickness')."
//
// Coordinates (logical, not Transform):
//   X      = horizontal screen      (LF2 calls "x")
//   Depth  = into/out of the screen (LF2 calls "y")
//   Height = vertical air position  (LF2 calls "z")
//
// World mapping (Unity Transform):
//   transform.position.x = X
//   transform.position.y = -Depth * DEPTH_FACTOR + Height
//   transform.position.z = 0
//
// Sorting: SpriteRenderer.sortingOrder = -RoundToInt(Depth * SORT_SCALE)
// — closer to camera = higher Y in our depth axis = lower depth value =
//   higher sortingOrder = drawn on top.

using LF2Game.Tick;
using UnityEngine;

namespace LF2Game.Movement
{
    [System.Serializable]
    public struct Position2_5D
    {
        public float X;
        public float Depth;
        public float Height;

        // How much of the depth axis bleeds into screen-Y. Tune for the look.
        public const float DEPTH_FACTOR = 0.5f;
        public const float SORT_SCALE   = 100f;

        public Vector3 ToWorld() => new(X, -Depth * DEPTH_FACTOR + Height, 0f);
        public int     ToSortingOrder() => -Mathf.RoundToInt(Depth * SORT_SCALE);

        public static Position2_5D Lerp(Position2_5D a, Position2_5D b, float t) => new()
        {
            X      = Mathf.Lerp(a.X,      b.X,      t),
            Depth  = Mathf.Lerp(a.Depth,  b.Depth,  t),
            Height = Mathf.Lerp(a.Height, b.Height, t),
        };
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class CharacterController2_5D : MonoBehaviour, ITickable
    {
        [Header("Logical Position")]
        public Position2_5D Position;

        [Header("Velocity (units/sec)")]
        public Vector3 Velocity; // x = X-vel, y = Depth-vel, z = Height-vel
        public bool   FacingLeft;

        [Header("Physics")]
        public float Gravity = -40f;

        [Header("Lane bounds (world Y on the floor)")]
        public float MinDepth = -3f;
        public float MaxDepth =  3f;

        public bool IsGrounded => Position.Height <= 0.0001f && Velocity.z <= 0.001f;

        Position2_5D _previous;
        SpriteRenderer _sr;

        void Awake() => _sr = GetComponent<SpriteRenderer>();

        void OnEnable()  { TickRunner.Instance?.Register(this); _previous = Position; }
        void OnDisable() { TickRunner.Instance?.Unregister(this); }

        public void Tick(int tick)
        {
            _previous = Position;

            Position.X      += Velocity.x * TickRunner.TICK_DT;
            Position.Depth  += Velocity.y * TickRunner.TICK_DT;
            Position.Height += Velocity.z * TickRunner.TICK_DT;

            // Clamp depth lane.
            if (Position.Depth < MinDepth) { Position.Depth = MinDepth; if (Velocity.y < 0) Velocity.y = 0; }
            if (Position.Depth > MaxDepth) { Position.Depth = MaxDepth; if (Velocity.y > 0) Velocity.y = 0; }

            // Gravity / ground.
            if (Position.Height > 0f)
                Velocity.z += Gravity * TickRunner.TICK_DT;
            else
            {
                Position.Height = 0f;
                if (Velocity.z < 0f) Velocity.z = 0f;
            }
        }

        void LateUpdate()
        {
            float a = TickRunner.Instance != null ? TickRunner.Instance.Alpha : 1f;
            var visual = Position2_5D.Lerp(_previous, Position, a);
            transform.position = visual.ToWorld();
            _sr.sortingOrder = visual.ToSortingOrder();
            _sr.flipX = FacingLeft;
        }

        public void SetGroundVelocity(float xVel, float depthVel)
        {
            Velocity.x = xVel;
            Velocity.y = depthVel;
        }

        public void Jump(float vel)
        {
            if (IsGrounded) Velocity.z = vel;
        }
    }
}
