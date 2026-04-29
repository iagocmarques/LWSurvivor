using System.Collections.Generic;
using System.Text;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.Combat
{
    /// <summary>
    /// HUD simples (IMGUI) com últimos inputs bufferizados + ring de debug.
    /// </summary>
    public sealed class CombatInputDebugHud : MonoBehaviour
    {
        [SerializeField] private PlayerHsmController player;
        [SerializeField] private bool show = true;
        [SerializeField] private int ringLines = 12;

        private readonly List<CombatInputDebugEntry> _ringScratch = new List<CombatInputDebugEntry>(32);
        private readonly StringBuilder _sb = new StringBuilder(256);

        private void Reset()
        {
            player = FindAnyObjectByType<PlayerHsmController>();
        }

        private void OnGUI()
        {
            if (!show || player == null)
                return;

            const int pad = 8;
            GUILayout.BeginArea(new Rect(pad, pad, 520, 420), GUI.skin.box);
            GUILayout.Label("<b>Combat Input Buffer (debug)</b>", new GUIStyle(GUI.skin.label) { richText = true });

            player.GetCombatDebugHudText(_sb, _ringScratch, ringLines);
            GUILayout.TextArea(_sb.ToString(), GUILayout.ExpandHeight(true));
            GUILayout.EndArea();
        }
    }
}
