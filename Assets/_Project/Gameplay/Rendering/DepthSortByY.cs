using Project.Core.Tick;
using UnityEngine;
using UnityEngine.Rendering;

namespace Project.Gameplay.Rendering
{
    public sealed class DepthSortByY : MonoBehaviour, ITickable
    {
        [SerializeField] private int baseSortingOrder = 0;
        [SerializeField] private float orderPerUnit = 100f;

        private SortingGroup sortingGroup;
        private SpriteRenderer[] spriteRenderers;
        private int tiebreak;

        private void Awake()
        {
            sortingGroup = GetComponent<SortingGroup>();
            if (sortingGroup == null)
                spriteRenderers = GetComponentsInChildren<SpriteRenderer>(true);

            tiebreak = GetInstanceID() & 0xFF;
        }

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
            Apply();
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
        }

        public void Tick(in TickContext context)
        {
            Apply();
        }

        private void Apply()
        {
            var y = transform.position.y;
            var order = baseSortingOrder - Mathf.RoundToInt(y * orderPerUnit) + tiebreak;

            if (sortingGroup != null)
            {
                sortingGroup.sortingOrder = order;
                return;
            }

            if (spriteRenderers == null)
                return;

            for (var i = 0; i < spriteRenderers.Length; i++)
            {
                var sr = spriteRenderers[i];
                if (sr == null)
                    continue;

                sr.sortingOrder = order;
            }
        }
    }
}