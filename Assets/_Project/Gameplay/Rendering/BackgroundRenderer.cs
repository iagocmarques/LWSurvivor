using System.Collections.Generic;
using Project.Data;
using UnityEngine;

namespace Project.Gameplay.Rendering
{
    [DisallowMultipleComponent]
    public sealed class BackgroundRenderer : MonoBehaviour
    {
        [SerializeField] private BackgroundDefinition definition;
        [SerializeField] private Camera targetCamera;

        private readonly List<Transform> layerTransforms = new();
        private readonly List<SpriteRenderer> layerRenderers = new();
        private Vector3 lastCameraPos;
        private bool initialized;

        public BackgroundDefinition Definition => definition;

        public void Init(BackgroundDefinition bg)
        {
            ClearLayers();
            definition = bg;
            BuildLayers();
            ApplyArenaBounds();
            initialized = true;
        }

        private void Start()
        {
            if (initialized)
                return;

            if (definition != null)
                Init(definition);
        }

        private void LateUpdate()
        {
            if (!initialized || definition == null || targetCamera == null)
                return;

            Vector3 camPos = targetCamera.transform.position;
            Vector3 delta = camPos - lastCameraPos;

            for (int i = 0; i < layerTransforms.Count; i++)
            {
                if (layerTransforms[i] == null)
                    continue;

                BackgroundLayerDefinition layer = definition.layers[i];
                Vector3 pos = layerTransforms[i].position;

                pos.x += delta.x * layer.parallaxFactor;
                pos.y += delta.y * layer.parallaxFactor;

                if (layer.scrollSpeed != 0f)
                    pos.x += layer.scrollSpeed * Time.deltaTime;

                layerTransforms[i].position = pos;
            }

            lastCameraPos = camPos;
        }

        private void BuildLayers()
        {
            if (targetCamera == null)
                targetCamera = Camera.main;

            if (targetCamera != null)
                lastCameraPos = targetCamera.transform.position;

            if (definition.layers == null)
                return;

            for (int i = 0; i < definition.layers.Count; i++)
            {
                BackgroundLayerDefinition layer = definition.layers[i];
                GameObject go = new GameObject($"BG_Layer_{i}_{layer.sortOrder}");
                go.transform.SetParent(transform, false);

                Vector3 layerPos = go.transform.localPosition;
                layerPos.x = layer.offset.x;
                layerPos.y = layer.offset.y;
                layerPos.z = i * 0.1f;
                go.transform.localPosition = layerPos;

                var sr = go.AddComponent<SpriteRenderer>();
                sr.sprite = layer.sprite;
                sr.sortingOrder = layer.sortOrder;
                sr.drawMode = SpriteDrawMode.Tiled;

                float spriteW = layer.sprite != null ? layer.sprite.bounds.size.x : 1f;
                float spriteH = layer.sprite != null ? layer.sprite.bounds.size.y : 1f;
                sr.size = new Vector2(Mathf.Max(definition.width * 2f, spriteW), spriteH);

                layerTransforms.Add(go.transform);
                layerRenderers.Add(sr);
            }
        }

        private void ApplyArenaBounds()
        {
            var arena = GetComponentInParent<ArenaBounds>();
            if (arena == null)
                arena = FindFirstObjectByType<ArenaBounds>();

            if (arena == null || definition == null)
                return;

            float halfW = definition.HalfWidth;
            float minZ = definition.zBoundary.x;
            float maxZ = definition.zBoundary.y;
            arena.SetBounds(-halfW, halfW, minZ, maxZ);
        }

        private void ClearLayers()
        {
            for (int i = layerTransforms.Count - 1; i >= 0; i--)
            {
                if (layerTransforms[i] != null)
                    Destroy(layerTransforms[i].gameObject);
            }

            layerTransforms.Clear();
            layerRenderers.Clear();
            initialized = false;
        }

        private void OnDestroy()
        {
            ClearLayers();
        }
    }
}
