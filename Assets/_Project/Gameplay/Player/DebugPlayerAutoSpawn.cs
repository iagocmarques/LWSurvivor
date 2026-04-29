using Project.Gameplay.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Gameplay.Player
{
    public static class DebugPlayerAutoSpawn
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void AfterSceneLoad()
        {
            if (SceneManager.GetActiveScene().name != "Game")
                return;

            var controller = Object.FindAnyObjectByType<Player25DController>();

            if (controller != null)
            {
                EnsureVisible(controller.gameObject);
                EnsureComponents(controller.gameObject);
                return;
            }

            var cam = Camera.main != null ? Camera.main : Object.FindAnyObjectByType<Camera>();
            var spawnPos = Vector3.zero;

            if (cam != null)
            {
                var p = cam.transform.position;
                spawnPos = new Vector3(p.x, p.y, 0f);
            }

            var player = new GameObject("Player");
            player.transform.position = spawnPos;

            var sr = player.AddComponent<SpriteRenderer>();
            sr.sprite = CreateDebugSprite();
            sr.color = new Color(1f, 0.95f, 0.35f, 1f);

            player.AddComponent<Player25DController>();
            player.AddComponent<DepthSortByY>();
        }

        private static void EnsureComponents(GameObject player)
        {
            if (player.GetComponent<DepthSortByY>() == null)
                player.AddComponent<DepthSortByY>();
        }

        private static void EnsureVisible(GameObject player)
        {
            var sr = player.GetComponent<SpriteRenderer>();
            if (sr == null)
                sr = player.AddComponent<SpriteRenderer>();

            if (sr.sprite == null)
                sr.sprite = CreateDebugSprite();

            if (sr.color.a <= 0.001f)
                sr.color = new Color(1f, 0.95f, 0.35f, 1f);
        }

        private static Sprite CreateDebugSprite()
        {
            const int size = 32;
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
            tex.filterMode = FilterMode.Point;
            tex.wrapMode = TextureWrapMode.Clamp;

            var pixels = new Color32[size * size];
            for (var i = 0; i < pixels.Length; i++)
                pixels[i] = new Color32(255, 255, 255, 255);

            tex.SetPixels32(pixels);
            tex.Apply(false, true);

            var rect = new Rect(0, 0, size, size);
            var pivot = new Vector2(0.5f, 0.5f);

            return Sprite.Create(tex, rect, pivot, 32f);
        }
    }
}