using UnityEngine;

namespace Project.Gameplay.Combat
{
    public static class CombatReadabilityFx
    {
        public static void SpawnDamagePopup(Vector3 worldPos, int damage)
        {
            SpawnPopup(worldPos + new Vector3(0f, 0.35f, 0f), "-" + Mathf.Max(0, damage), new Color(1f, 0.9f, 0.25f, 1f), 0.45f);
        }

        public static void SpawnKillPopup(Vector3 worldPos, string label)
        {
            var text = string.IsNullOrWhiteSpace(label) ? "KO!" : "KO " + label;
            SpawnPopup(worldPos + new Vector3(0f, 0.65f, 0f), text, new Color(1f, 0.45f, 0.45f, 1f), 0.8f);
        }

        private static void SpawnPopup(Vector3 pos, string text, Color color, float ttl)
        {
            var go = new GameObject("CombatPopup");
            var tm = go.AddComponent<TextMesh>();
            tm.text = text;
            tm.fontSize = 48;
            tm.characterSize = 0.035f;
            tm.color = color;
            tm.anchor = TextAnchor.MiddleCenter;
            go.transform.position = pos;
            Object.Destroy(go, Mathf.Max(0.1f, ttl));
        }
    }
}
