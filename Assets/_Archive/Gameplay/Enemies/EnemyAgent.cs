#if false
using Project.Core.Tick;
using Project.Gameplay.Combat;
using Project.Gameplay.LF2;
using Project.Gameplay.Visual;
using LF2Importer;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay.Enemies
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Damageable))]
    public sealed class EnemyAgent : MonoBehaviour, ITickable
    {
        [SerializeField] private EnemyDefinition definition;
        [SerializeField] private TextAsset banditDat;

        private Transform _target;
        private Health _health;
        private float _nextThinkAt;
        private EnemySpawnerDirector _ownerSpawner;
        private float _touchCooldown;
        public int NetId { get; private set; }
        private SpriteRenderer _sprite;
        private Color _baseTint = Color.white;
        private float _flashLeft;
        private Lf2EnemySpriteAnimator _lf2Animator;

        private Lf2StateMachine _lf2Sm;
        private LF2.Lf2EnemyAI _lf2Ai;
        private Lf2ItrProcessor _itrProcessor;
        private bool _lf2Initialized;

        private static readonly Dictionary<string, Lf2CharacterData> _characterDataCache = new Dictionary<string, Lf2CharacterData>();
        private static bool _triedLoadSo;

        public float XpDrop => definition != null ? definition.xpDrop : 1f;
        public string DefinitionId => definition != null ? definition.id : "enemy.unknown";
        public Lf2StateMachine Lf2Sm => _lf2Sm;

        public void Initialize(int netId, EnemyDefinition def, Transform target, EnemySpawnerDirector ownerSpawner)
        {
            NetId = netId;
            definition = def;
            _target = target;
            _ownerSpawner = ownerSpawner;
            _nextThinkAt = 0f;
            _touchCooldown = 0f;

            EnsureHealth();
            _health.ResetToFull();
            ApplyVisualFromDefinition();
            InitializeLf2();
        }

        private void Awake()
        {
            EnsureHealth();
            _sprite = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            FixedTickSystem.Register(this);
            EnsureHealth();
            _health.OnDied += OnDied;
            _health.OnDamaged += OnDamaged;
        }

        private void OnDisable()
        {
            FixedTickSystem.Unregister(this);
            if (_health != null)
            {
                _health.OnDied -= OnDied;
                _health.OnDamaged -= OnDamaged;
            }
        }

        public void Tick(in TickContext context)
        {
            if (definition == null || _target == null || _health == null || _health.IsDead)
                return;

            if (_touchCooldown > 0f)
                _touchCooldown -= context.FixedDelta;

            if (_lf2Initialized && _lf2Sm != null && _lf2Ai != null)
            {
                _lf2Ai.Tick((float)context.UnscaledNow, context.FixedDelta, _health);
                _lf2Sm.Tick();

                var pos = transform.position;
                _lf2Sm.SetPosition(ref pos);
                transform.position = pos;

                if (_lf2Animator != null)
                    _lf2Animator.SetFacing(_lf2Sm.FacingRight);
            }
            else
            {
                var now = (float)context.UnscaledNow;
                if (now >= _nextThinkAt)
                {
                    var toTarget = (Vector2)(_target.position - transform.position);
                    var steerDir = toTarget.sqrMagnitude > 0.0001f ? toTarget.normalized : Vector2.zero;
                    _nextThinkAt = now + Mathf.Max(0.05f, definition.thinkInterval);

                    var delta = (Vector3)(steerDir * (definition.moveSpeed * context.FixedDelta));
                    transform.position += delta;

                    if (steerDir.sqrMagnitude > 0.0001f && _lf2Animator != null)
                        _lf2Animator.SetFacing(steerDir.x >= 0f);
                }
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (definition == null || _touchCooldown > 0f)
                return;

            if (_lf2Initialized)
                return;

            if (!other.TryGetComponent<ICombatHurtbox>(out var hurt))
                return;

            var toVictim = (Vector2)(other.transform.position - transform.position);
            if (toVictim.sqrMagnitude < 0.0001f)
                toVictim = Vector2.right;

            var hit = new CombatHitInfo(
                gameObject,
                Mathf.RoundToInt(definition.touchDamage),
                toVictim.normalized * 2f,
                CombatAttackId.None,
                2,
                0.08f,
                false);

            if (hurt.ReceiveHit(in hit))
                _touchCooldown = 0.4f;
        }

        private void OnDied(Health _)
        {
            CombatReadabilityFx.SpawnKillPopup(transform.position, definition != null ? definition.displayName : "enemy");
            _ownerSpawner?.NotifyEnemyDied(this);
        }

        private void OnDamaged(Health _, int amount)
        {
            _flashLeft = definition != null ? definition.hitFlashSeconds : 0.06f;
            CombatReadabilityFx.SpawnDamagePopup(transform.position, amount);

            if (_lf2Initialized && _lf2Ai != null)
            {
                var attackerPos = _target != null ? _target.position : transform.position;
                _lf2Ai.OnDamaged(amount, attackerPos);
            }
        }

        private void EnsureHealth()
        {
            _health = GetComponent<Health>();
            if (_health == null)
                return;

            _health.ConfigureMaxHealth(definition != null ? definition.maxHealth : 20, true);
        }

        private void ApplyVisualFromDefinition()
        {
            if (definition == null)
                return;

            if (_sprite == null)
                _sprite = GetComponent<SpriteRenderer>();

            if (_sprite != null)
            {
                var sp = Lf2VisualLibrary.GetEnemySprite(definition.id);
                if (sp != null)
                    _sprite.sprite = sp;

                _baseTint = definition.tint;
                _sprite.color = _baseTint;
            }

            if (_lf2Animator == null)
                _lf2Animator = GetComponent<Lf2EnemySpriteAnimator>();
            if (_lf2Animator == null)
                _lf2Animator = gameObject.AddComponent<Lf2EnemySpriteAnimator>();
            _lf2Animator.Configure(definition.id);

            transform.localScale = Vector3.one * Mathf.Max(0.25f, definition.scale);
        }

        private void InitializeLf2()
        {
            var characterData = LoadCharacterData();
            if (characterData == null || characterData.Frames == null || characterData.Frames.Count == 0)
            {
                Debug.LogWarning($"[EnemyAgent] No LF2 character data for {definition?.id}. Using fallback movement.");
                return;
            }

            _lf2Sm = new Lf2StateMachine();
            var groundY = transform.position.y;
            _lf2Sm.Initialize(characterData, groundY);

            var hurtMask = LayerMask.GetMask("Hurtbox", "Player");
            _itrProcessor = new Lf2ItrProcessor();
            _itrProcessor.Initialize(_lf2Sm, transform, hurtMask);
            _lf2Sm.SetItrProcessor(_itrProcessor);
            _lf2Sm.SetAlwaysProcessItr(true);

            var personality = definition != null ? definition.personality : Lf2EnemyPersonality.Aggressive;
            _lf2Ai = new LF2.Lf2EnemyAI();
            _lf2Ai.Initialize(_lf2Sm, transform, _target, personality);

            _lf2Initialized = true;
        }

        private Lf2CharacterData LoadCharacterData()
        {
            var datName = definition != null && !string.IsNullOrEmpty(definition.characterDatName)
                ? definition.characterDatName
                : "bandit";

            if (_characterDataCache.TryGetValue(datName, out var cached))
                return cached;

            if (!_triedLoadSo)
            {
                _triedLoadSo = true;
                var allImported = Resources.FindObjectsOfTypeAll<ImportedLf2Character>();
                if (allImported != null)
                {
                    for (int i = 0; i < allImported.Length; i++)
                    {
                        if (allImported[i] != null && allImported[i].sourceDatRelativePath.Contains(datName))
                        {
                            var soData = Lf2RuntimeDatLoader.LoadFromImportedCharacter(allImported[i]);
                            if (soData != null)
                            {
                                _characterDataCache[datName] = soData;
                                Debug.Log($"[EnemyAgent] Loaded {datName} from ImportedLf2Character SO: {soData.Frames.Count} frames");
                                return soData;
                            }
                        }
                    }
                }
            }

            if (datName == "bandit" && banditDat != null)
            {
                var taData = Lf2RuntimeDatLoader.LoadFromTextAsset(banditDat);
                if (taData != null)
                {
                    _characterDataCache[datName] = taData;
                    Debug.Log($"[EnemyAgent] Loaded {datName} .dat: {taData.Frames.Count} frames, name='{taData.Name}'");
                    return taData;
                }
            }

            var textAsset = Resources.Load<TextAsset>("LF2/" + datName);
            if (textAsset != null)
            {
                var resData = Lf2RuntimeDatLoader.LoadFromTextAsset(textAsset);
                if (resData != null)
                {
                    _characterDataCache[datName] = resData;
                    Debug.Log($"[EnemyAgent] Loaded {datName} from Resources: {resData.Frames.Count} frames");
                    return resData;
                }
            }

            var fallback = BuildFallbackCharacterData(datName);
            _characterDataCache[datName] = fallback;
            return fallback;
        }

        private static Lf2CharacterData BuildFallbackCharacterData(string datName)
        {
            float walkSpeed = 3.0f;
            float runSpeed = 5.5f;
            string charName = datName ?? "Bandit";

            if (datName == "mark" || datName == "knight")
            {
                walkSpeed = 2.0f;
                runSpeed = 3.5f;
            }
            else if (datName == "jack" || datName == "justin")
            {
                walkSpeed = 4.0f;
                runSpeed = 7.0f;
            }
            else if (datName == "bat" || datName == "louisEX" || datName == "firzen" || datName == "julian")
            {
                walkSpeed = 3.5f;
                runSpeed = 6.0f;
            }

            var character = new Lf2CharacterData
            {
                Name = charName,
                Movement = new System.Collections.Generic.Dictionary<string, float>(System.StringComparer.OrdinalIgnoreCase)
                {
                    { "walking_speed", walkSpeed },
                    { "running_speed", runSpeed },
                },
                Frames = new System.Collections.Generic.Dictionary<int, Lf2FrameData>(),
                BmpEntries = new System.Collections.Generic.List<Lf2BmpEntry>()
            };

            character.Frames[0] = MakeFrame(0, "standing", 0, Lf2State.Standing, 4, 999);
            character.Frames[1] = MakeFrame(1, "walking", 3, Lf2State.Walking, 3, 0);
            character.Frames[2] = MakeFrame(2, "running", 3, Lf2State.Running, 2, 0);

            character.Frames[60] = MakeAttackFrame(60, "punch1", 15, 4, 61, 5, 3.5f, 0f);
            character.Frames[61] = MakeAttackFrame(61, "punch2", 18, 4, 62, 5, 4f, 0f);
            character.Frames[62] = MakeAttackFrame(62, "punch3", 21, 5, 999, 8, 5f, 0f);

            character.Frames[100] = MakeFrame(100, "defend", 56, Lf2State.Defending, 0, 999);
            character.Frames[210] = MakeFrame(210, "jump", 70, Lf2State.Jumping, 3, 999);
            character.Frames[230] = MakeFrame(230, "getup", 34, Lf2State.Lying, 5, 0);

            character.RoleIds = new Lf2FrameRoleIds();

            Debug.Log($"[EnemyAgent] Using fallback frame data for '{charName}' (9 frames).");
            return character;
        }

        private static Lf2FrameData MakeFrame(int id, string name, int pic, Lf2State state, int wait, int next)
        {
            return new Lf2FrameData
            {
                Id = id,
                Name = name,
                Pic = pic,
                State = state,
                Wait = wait,
                Next = next,
                Dvx = 0f,
                Dvy = 0f,
                Itrs = System.Array.Empty<Lf2ItrData>(),
                Bdy = System.Array.Empty<Lf2BdyData>(),
                Opoints = System.Array.Empty<Lf2OpointData>(),
                ExtraProps = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.OrdinalIgnoreCase)
            };
        }

        private static Lf2FrameData MakeAttackFrame(int id, string name, int pic, int wait, int next,
            int injury, float dvx, float dvy)
        {
            var frame = MakeFrame(id, name, pic, Lf2State.Attacking, wait, next);
            frame.Dvx = dvx;
            frame.Dvy = dvy;
            frame.Itrs = new[]
            {
                new Lf2ItrData(
                    Lf2ItrKind.Hit,
                    new Rect(20f, -10f, 40f, 30f),
                    dvx, dvy,
                    70, 0, 0,
                    injury,
                    Lf2EffectType.None,
                    0)
            };
            return frame;
        }

        private void Update()
        {
            if (_sprite == null)
                return;

            if (_flashLeft > 0f)
            {
                _flashLeft -= Time.deltaTime;
                float t = Mathf.Clamp01(_flashLeft / Mathf.Max(0.01f, definition != null ? definition.hitFlashSeconds : 0.10f));
                _sprite.color = Color.Lerp(_baseTint, Color.white, Mathf.Lerp(0.85f, 0f, 1f - t));
            }
            else
            {
                _sprite.color = _baseTint;
            }
        }
    }
}
#endif
