#nullable enable
using UnityEngine;

namespace Project.Gameplay.Visual
{
    /// <summary>
    /// Ensures <see cref="SpriteRenderer"/> uses a material compatible with the active render pipeline.
    /// Serialized null materials or Built-in sprite materials often show as magenta under URP.
    /// </summary>
    public static class Sprite2DMaterialUtility
    {
        private const string UrpSpriteUnlitShaderName = "Universal Render Pipeline/2D/Sprite-Unlit-Default";
        private static Material? _cachedUrpSpriteUnlit;

        public static void EnsureCompatibleMaterial(SpriteRenderer? renderer)
        {
            if (renderer == null)
                return;

            var mat = renderer.sharedMaterial;
            if (mat != null && mat.shader != null && mat.shader.isSupported)
                return;

            if (_cachedUrpSpriteUnlit == null)
            {
                var shader = Shader.Find(UrpSpriteUnlitShaderName);
                if (shader == null)
                {
                    Debug.LogError(
                        $"[Sprite2DMaterialUtility] Shader '{UrpSpriteUnlitShaderName}' not found. Add it to Graphics Settings → Always Included Shaders if stripping removes it.");
                    return;
                }

                _cachedUrpSpriteUnlit = new Material(shader) { name = "Cached URP Sprite-Unlit (runtime)" };
            }

            renderer.sharedMaterial = _cachedUrpSpriteUnlit;
        }
    }
}
