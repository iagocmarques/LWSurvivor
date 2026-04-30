using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    [DisallowMultipleComponent]
    public sealed class Lf2ItemSpawner : MonoBehaviour
    {
        [SerializeField] private Sprite milkSprite;
        [SerializeField] private Sprite beerSprite;
        [SerializeField] private Sprite chickenSprite;
        [SerializeField] private Sprite dumplingSprite;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float spawnInterval = 15f;
        [SerializeField] private int maxActiveItems = 5;
        [SerializeField] private float itemGravity = 3f;
        [SerializeField] private float groundY;

        private float _spawnTimer;
        private int _activeCount;
        private readonly HashSet<Lf2Item> _trackedItems = new();

        private Sprite GetSprite(Lf2ItemType type)
        {
            return type switch
            {
                Lf2ItemType.Milk => milkSprite,
                Lf2ItemType.Beer => beerSprite,
                Lf2ItemType.Chicken => chickenSprite,
                Lf2ItemType.Dumpling => dumplingSprite,
                _ => milkSprite,
            };
        }

        private void Update()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0f)
            {
                _spawnTimer = spawnInterval;
                TrySpawn();
            }
        }

        public Lf2Item SpawnItem(Lf2ItemType type, Vector3 position)
        {
            var go = new GameObject($"Item_{type}");
            go.transform.position = position;
            go.transform.SetParent(transform);

            go.layer = LayerMask.NameToLayer("Items");

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sortingOrder = 5;
            sr.sprite = GetSprite(type);

            var col = go.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = new Vector2(0.4f, 0.4f);

            var rb = go.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
            rb.bodyType = RigidbodyType2D.Kinematic;

            var item = go.AddComponent<Lf2Item>();
            item.Setup(type);

            TrackItem(item);
            return item;
        }

        public Lf2Item SpawnWeaponItem(Lf2WeaponType weaponType, Vector3 position)
        {
            var go = new GameObject($"Item_Weapon_{weaponType}");
            go.transform.position = position;
            go.transform.SetParent(transform);

            go.layer = LayerMask.NameToLayer("Items");

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sortingOrder = 5;

            var col = go.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = new Vector2(0.4f, 0.4f);

            var rb = go.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
            rb.bodyType = RigidbodyType2D.Kinematic;

            var item = go.AddComponent<Lf2Item>();
            item.SetupWeapon(weaponType);

            TrackItem(item);
            return item;
        }

        public void SpawnAtPoint(Lf2ItemType type, Vector3 position, Transform parent = null)
        {
            var item = SpawnItem(type, position);
            if (parent != null)
                item.transform.SetParent(parent);
        }

        private void TrySpawn()
        {
            if (_activeCount >= maxActiveItems) return;
            if (spawnPoints == null || spawnPoints.Length == 0) return;

            var point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            float roll = Random.value;

            Lf2ItemType type;
            if (roll < 0.35f)
                type = Lf2ItemType.Milk;
            else if (roll < 0.65f)
                type = Lf2ItemType.Beer;
            else if (roll < 0.85f)
                type = Lf2ItemType.Chicken;
            else
                type = Lf2ItemType.Dumpling;

            SpawnItem(type, point.position);
        }

        private void TrackItem(Lf2Item item)
        {
            if (item == null) return;
            _trackedItems.Add(item);
            _activeCount++;
        }

        public void OnItemDestroyed(Lf2Item item)
        {
            if (item == null) return;
            if (_trackedItems.Remove(item))
                _activeCount = Mathf.Max(0, _activeCount - 1);
        }

        private void LateUpdate()
        {
            if (_trackedItems.Count == 0) return;

            _trackedItems.RemoveWhere(item =>
            {
                if (item == null)
                {
                    _activeCount = Mathf.Max(0, _activeCount - 1);
                    return true;
                }

                var pos = item.transform.position;
                if (pos.y > groundY)
                {
                    pos.y -= itemGravity * Time.deltaTime;
                    if (pos.y < groundY) pos.y = groundY;
                    item.transform.position = pos;
                }

                return false;
            });
        }
    }
}
