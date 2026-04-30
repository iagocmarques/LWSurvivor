#if false
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Gameplay.Combat
{
    /// <summary>
    /// Dev-only tool for testing reactive combat.
    /// Spawns hitboxes on keypress near the player.
    /// Numpad 1 = weak hit, Numpad 2 = heavy hit, Numpad 3 = launcher.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class TestAttacker : MonoBehaviour
    {
        [SerializeField] private LayerMask hurtboxMask = ~0;
        [SerializeField] private float spawnOffsetX = 1.5f;

        private void Update()
        {
            var kb = Keyboard.current;
            if (kb == null) return;

            if (kb.numpad1Key.wasPressedThisFrame)
                SpawnHit(damage: 5, knockback: new Vector2(3f, 0f), hitStopTicks: 2, screenShake: 0.08f, bdefend: 12, label: "Weak");

            if (kb.numpad2Key.wasPressedThisFrame)
                SpawnHit(damage: 15, knockback: new Vector2(6f, 0f), hitStopTicks: 4, screenShake: 0.20f, bdefend: 12, label: "Heavy");

            if (kb.numpad3Key.wasPressedThisFrame)
                SpawnHit(damage: 12, knockback: new Vector2(3f, 8f), hitStopTicks: 4, screenShake: 0.15f, bdefend: 0, label: "Launcher");
        }

        private void SpawnHit(int damage, Vector2 knockback, int hitStopTicks, float screenShake, int bdefend, string label)
        {
            var hitbox = CombatHitboxPool.Rent();
            var facing = transform.position.x >= PlayerPosition.x ? 1f : -1f;
            var center = PlayerPosition + new Vector3(spawnOffsetX * facing, 0f, 0f);

            hitbox.Arm(
                gameObject,
                hurtboxMask,
                damage,
                knockback * facing,
                CombatAttackId.None,
                hitStopTicks,
                screenShake,
                isGrab: false,
                bdefend: bdefend);

            hitbox.SetWorldPose(center, new Vector2(0.8f, 0.5f), 0f);
            hitbox.TickOverlap();

            CombatHitboxPool.Return(hitbox);

            Debug.Log($"[TestAttacker] {label} hit: dmg={damage} kb={knockback}");
        }

        private static Vector3 PlayerPosition
        {
            get
            {
                var player = GameObject.Find("Player");
                return player != null ? player.transform.position : Vector3.zero;
            }
        }
    }
}
#endif
