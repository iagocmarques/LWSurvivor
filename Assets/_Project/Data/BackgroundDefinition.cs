using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Data
{
    [Serializable]
    public struct BackgroundLayerDefinition
    {
        public Sprite sprite;
        [Tooltip("0 = static, 1 = moves fully with camera")]
        public float parallaxFactor;
        public float scrollSpeed;
        public Vector2 offset;
        public int sortOrder;
    }

    [CreateAssetMenu(fileName = "NewBackground", menuName = "_Project/Data/Background Definition", order = 3)]
    public sealed class BackgroundDefinition : ScriptableObject
    {
        [Header("Identity")]
        public int lf2BackgroundId;
        public string displayName;

        [Header("Arena")]
        public int width;
        [Tooltip("min / max depth for gameplay area")]
        public Vector2Int zBoundary;

        [Header("Shadow")]
        public Sprite shadowSprite;
        public Vector2Int shadowSize;

        [Header("Layers (back-to-front)")]
        public List<BackgroundLayerDefinition> layers = new();

        public float HalfWidth => width * 0.5f;
    }
}
