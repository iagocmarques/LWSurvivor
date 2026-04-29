using Project.Gameplay.Combat;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.UI.Debug
{
    /// <summary>
    /// Debug overlay showing reactive combat state for the player.
    /// Only active in development builds.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class ReactiveCombatDebugOverlay : MonoBehaviour
    {
        private PlayerHsmController _hsm;
        private Health _health;

        private void Awake()
        {
            _hsm = GetComponent<PlayerHsmController>();
            _health = GetComponent<Health>();
        }

#if UNITY_EDITOR || DEVELOPMENT_BUILD
        private void OnGUI()
        {
            if (_hsm == null)
                return;

            var x = 10f;
            var y = 10f;
            var w = 280f;
            var h = 200f;

            // Background
            GUI.Box(new Rect(x, y, w, h), "");

            var style = new GUIStyle(GUI.skin.label)
            {
                fontSize = 13,
                richText = true
            };

            float lineY = y + 5f;
            float lineH = 18f;

            GUI.Label(new Rect(x + 5, lineY, w, lineH), "<b>Reactive Combat Debug</b>", style);
            lineY += lineH + 4f;

            // Macrostate
            string stateStr = _hsm.IsDead ? "Dead"
                : _hsm.IsHurtAir ? "HurtAir"
                : _hsm.IsLying ? "Lying"
                : _hsm.IsGetUp ? "GetUp"
                : _hsm.IsDefendBreak ? "DefendBreak"
                : _hsm.IsHurtGrounded ? "HurtGrounded"
                : _hsm.IsDefending ? "Defending"
                : _hsm.IsAttacking ? "Attack"
                : _hsm.IsMoving ? "Move"
                : "Idle";

            GUI.Label(new Rect(x + 5, lineY, w, lineH), $"State: <b>{stateStr}</b>", style);
            lineY += lineH;

            // Reactive frame
            if (_hsm.IsDefending || _hsm.IsDefendBreak || _hsm.IsHurtGrounded
                || _hsm.IsHurtAir || _hsm.IsLying || _hsm.IsGetUp || _hsm.IsDead)
            {
                GUI.Label(new Rect(x + 5, lineY, w, lineH),
                    $"Reactive: {_hsm.CurrentReactiveState} frame {_hsm.ReactiveFrameIndex}", style);
                lineY += lineH;
            }

            // HP
            if (_health != null)
            {
                GUI.Label(new Rect(x + 5, lineY, w, lineH),
                    $"HP: {_health.CurrentHealth}/{_health.MaxHealth}", style);
                lineY += lineH;
            }

            // Last hit
            var hit = _hsm.LastHitResult;
            if (hit.Damage > 0)
            {
                GUI.Label(new Rect(x + 5, lineY, w, lineH),
                    $"Hit: dmg={hit.Damage} kb={hit.Knockback:F1} flags={hit.Flags}", style);
                lineY += lineH;
            }

            // Status effect
            var se = _hsm.ActiveStatusEffect;
            if (se.IsActive)
            {
                GUI.Label(new Rect(x + 5, lineY, w, lineH),
                    $"Status: {se.Effect} ({se.RemainingTicks}t)", style);
                lineY += lineH;
            }
        }
#endif
    }
}
