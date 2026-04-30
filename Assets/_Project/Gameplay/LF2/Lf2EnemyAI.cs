using Project.Gameplay.Combat;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public enum Lf2EnemyPersonality
    {
        Aggressive,
        Defensive,
        Ranged,
        Brute,
        Fast,
        Boss,
        MagicSupport,
    }

    public enum Lf2EnemyBehaviorState
    {
        Idle,
        Chase,
        Attack,
        Defend,
        Flee,
        Cooldown,
    }

    public sealed class Lf2EnemyAI
    {
        private const float PixelToUnit = Lf2StateMachine.PixelToUnit;

        private Lf2StateMachine _sm;
        private Transform _owner;
        private Transform _target;
        private Lf2EnemyPersonality _personality;

        private Lf2EnemyBehaviorState _behaviorState;
        private float _nextThinkAt;
        private float _thinkInterval = 0.06f;
        private float _attackCooldownLeft;
        private float _defendCooldownLeft;
        private float _idleTimer;
        private float _fleeTimer;
        private float _jumpCooldownLeft;

        private float _meleeRange = 1.2f;
        private float _chaseRange = 15f;
        private float _fleeHealthPercent = 0.2f;
        private float _defendChance = 0.3f;
        private float _jumpChancePerSecond = 0.1f;
        private float _attackCooldown = 0.5f;
        private float _defendDuration = 0.6f;
        private float _fleeDuration = 1.0f;

        private bool _facingRight = true;
        private int _comboStep;
        private int[] _comboChain = { 60, 61, 62 };
        private bool _canHealAllies;
        private float _healCooldownLeft;
#pragma warning disable 0414
        private float _healCooldown = 8f;
        private float _healRange = 6f;
#pragma warning restore 0414

        public Lf2EnemyBehaviorState BehaviorState => _behaviorState;
        public bool FacingRight => _facingRight;
        public float ThinkInterval { get => _thinkInterval; set => _thinkInterval = Mathf.Clamp(value, 0.05f, 0.1f); }

        public void Initialize(Lf2StateMachine sm, Transform owner, Transform target, Lf2EnemyPersonality personality)
        {
            _sm = sm;
            _owner = owner;
            _target = target;
            _personality = personality;
            _behaviorState = Lf2EnemyBehaviorState.Idle;
            _nextThinkAt = 0f;
            _attackCooldownLeft = 0f;
            _defendCooldownLeft = 0f;
            _idleTimer = 0f;
            _fleeTimer = 0f;
            _jumpCooldownLeft = 0f;
            _comboStep = 0;
            _healCooldownLeft = 0f;

            ApplyPersonalityTuning();
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public bool Tick(float currentTime, float fixedDelta, Health health)
        {
            if (_sm == null || _owner == null || _target == null)
                return false;

            if (_attackCooldownLeft > 0f)
                _attackCooldownLeft -= fixedDelta;
            if (_defendCooldownLeft > 0f)
                _defendCooldownLeft -= fixedDelta;
            if (_jumpCooldownLeft > 0f)
                _jumpCooldownLeft -= fixedDelta;
            if (_healCooldownLeft > 0f)
                _healCooldownLeft -= fixedDelta;

            if (currentTime < _nextThinkAt)
                return false;

            _nextThinkAt = currentTime + _thinkInterval;

            var toTarget = (Vector2)(_target.position - _owner.position);
            var distSq = toTarget.sqrMagnitude;
            var dist = Mathf.Sqrt(distSq);

            if (distSq > 0.0001f)
            {
                _facingRight = toTarget.x >= 0f;
                _sm.SetFacingRight(_facingRight);
            }

            var currentState = _sm.CurrentState;

            if (currentState == Lf2State.Attacking)
                return false;

            if (currentState == Lf2State.Injured
                || currentState == Lf2State.Falling || currentState == Lf2State.FallingAlt
                || currentState == Lf2State.Lying || currentState == Lf2State.BrokenDefend
                || currentState == Lf2State.Caught)
                return false;

            bool lowHealth = health != null && (float)health.CurrentHealth / Mathf.Max(1, health.MaxHealth) < _fleeHealthPercent;

            var newState = EvaluateBehavior(dist, currentState, lowHealth);

            if (newState != _behaviorState)
            {
                _behaviorState = newState;
                _idleTimer = 0f;
                _fleeTimer = 0f;
            }

            ExecuteBehavior(newState, dist, toTarget, fixedDelta);
            return true;
        }

        public void OnDamaged(int damage, Vector2 attackerPos)
        {
            if (_sm == null) return;

            var currentState = _sm.CurrentState;
            if (currentState == Lf2State.Attacking)
                return;

            if (_defendCooldownLeft <= 0f && Random.value < _defendChance)
            {
                _sm.SetFrame(100);
                _behaviorState = Lf2EnemyBehaviorState.Defend;
                _defendCooldownLeft = _defendDuration + 0.3f;
            }
        }

        private Lf2EnemyBehaviorState EvaluateBehavior(float dist, Lf2State lf2State, bool lowHealth)
        {
            if (_canHealAllies && _healCooldownLeft <= 0f && lowHealth)
                return Lf2EnemyBehaviorState.Idle;

            if (lowHealth && _personality != Lf2EnemyPersonality.Aggressive
                && _personality != Lf2EnemyPersonality.Brute
                && _personality != Lf2EnemyPersonality.Boss)
                return Lf2EnemyBehaviorState.Flee;

            if (dist <= _meleeRange)
            {
                if (_attackCooldownLeft <= 0f)
                    return Lf2EnemyBehaviorState.Attack;

                return Lf2EnemyBehaviorState.Cooldown;
            }

            if (dist <= _chaseRange)
                return Lf2EnemyBehaviorState.Chase;

            return Lf2EnemyBehaviorState.Idle;
        }

        private void ExecuteBehavior(Lf2EnemyBehaviorState state, float dist, Vector2 toTarget, float fixedDelta)
        {
            switch (state)
            {
                case Lf2EnemyBehaviorState.Idle:
                    ExecuteIdle(fixedDelta);
                    break;
                case Lf2EnemyBehaviorState.Chase:
                    ExecuteChase(toTarget, dist, fixedDelta);
                    break;
                case Lf2EnemyBehaviorState.Attack:
                    ExecuteAttack();
                    break;
                case Lf2EnemyBehaviorState.Defend:
                    ExecuteDefend(fixedDelta);
                    break;
                case Lf2EnemyBehaviorState.Flee:
                    ExecuteFlee(toTarget, fixedDelta);
                    break;
                case Lf2EnemyBehaviorState.Cooldown:
                    ExecuteCooldown(toTarget, fixedDelta);
                    break;
            }
        }

        private void ExecuteIdle(float fixedDelta)
        {
            _idleTimer += fixedDelta;

            if (_idleTimer > 2f)
            {
                _behaviorState = Lf2EnemyBehaviorState.Chase;
                return;
            }

            if (_sm.CurrentState != Lf2State.Standing)
                _sm.SetFrame(0);
        }

        private void ExecuteChase(Vector2 toTarget, float dist, float fixedDelta)
        {
            var currentState = _sm.CurrentState;

            if (currentState != Lf2State.Walking && currentState != Lf2State.Running)
                _sm.SetFrame(1);

            if (dist > 0.1f)
            {
                var dir = toTarget.normalized;
                var moveSpeed = _sm.Character?.Movement != null && _sm.Character.Movement.TryGetValue("walking_speed", out var ws)
                    ? ws * PixelToUnit
                    : 2.5f * PixelToUnit;
                var pos = _owner.position;
                pos.x += dir.x * moveSpeed * fixedDelta * 60f;
                _owner.position = pos;
            }

            if (_jumpCooldownLeft <= 0f && Random.value < _jumpChancePerSecond * fixedDelta)
            {
                _sm.SetFrame(210);
                _jumpCooldownLeft = 2f;
            }
        }

        private void ExecuteAttack()
        {
            int attackFrame;
            if (_comboStep < _comboChain.Length && _sm.HasFrame(_comboChain[_comboStep]))
            {
                attackFrame = _comboChain[_comboStep];
                _comboStep++;
            }
            else
            {
                attackFrame = _comboChain[0];
                _comboStep = 1;
            }

            _sm.SetFrame(attackFrame);
            _attackCooldownLeft = _attackCooldown;
            _behaviorState = Lf2EnemyBehaviorState.Cooldown;
        }

        private void ExecuteDefend(float fixedDelta)
        {
            if (_sm.CurrentState != Lf2State.Defending)
                _sm.SetFrame(100);

            _defendCooldownLeft -= fixedDelta;
            if (_defendCooldownLeft <= 0f)
            {
                _behaviorState = Lf2EnemyBehaviorState.Chase;
                _sm.SetFrame(0);
            }
        }

        private void ExecuteFlee(Vector2 toTarget, float fixedDelta)
        {
            _fleeTimer += fixedDelta;

            if (_fleeTimer >= _fleeDuration)
            {
                _behaviorState = Lf2EnemyBehaviorState.Chase;
                return;
            }

            if (_sm.CurrentState != Lf2State.Walking)
                _sm.SetFrame(1);

            if (toTarget.sqrMagnitude > 0.0001f)
            {
                var dir = -toTarget.normalized;
                var moveSpeed = _sm.Character?.Movement != null && _sm.Character.Movement.TryGetValue("walking_speed", out var ws)
                    ? ws * PixelToUnit
                    : 2.5f * PixelToUnit;
                var pos = _owner.position;
                pos.x += dir.x * moveSpeed * fixedDelta * 60f;
                _owner.position = pos;
            }
        }

        private void ExecuteCooldown(Vector2 toTarget, float fixedDelta)
        {
            if (_attackCooldownLeft <= 0f)
            {
                _comboStep = 0;
                _behaviorState = Lf2EnemyBehaviorState.Chase;
                return;
            }

            if (_sm.CurrentState != Lf2State.Attacking && _sm.CurrentState != Lf2State.Standing)
                _sm.SetFrame(0);
        }

        private void ApplyPersonalityTuning()
        {
            switch (_personality)
            {
                case Lf2EnemyPersonality.Aggressive:
                    _defendChance = 0.15f;
                    _attackCooldown = 0.35f;
                    _fleeHealthPercent = 0.1f;
                    _jumpChancePerSecond = 0.05f;
                    _comboChain = new[] { 60, 61, 62 };
                    break;

                case Lf2EnemyPersonality.Defensive:
                    _defendChance = 0.5f;
                    _attackCooldown = 0.7f;
                    _fleeHealthPercent = 0.3f;
                    _jumpChancePerSecond = 0.08f;
                    _defendDuration = 0.9f;
                    _comboChain = new[] { 60, 61 };
                    break;

                case Lf2EnemyPersonality.Ranged:
                    _meleeRange = 0.8f;
                    _defendChance = 0.2f;
                    _attackCooldown = 0.6f;
                    _fleeHealthPercent = 0.25f;
                    _jumpChancePerSecond = 0.15f;
                    _comboChain = new[] { 60 };
                    break;

                case Lf2EnemyPersonality.Brute:
                    _meleeRange = 1.5f;
                    _defendChance = 0.1f;
                    _attackCooldown = 0.8f;
                    _fleeHealthPercent = 0.05f;
                    _jumpChancePerSecond = 0.02f;
                    _thinkInterval = 0.08f;
                    _comboChain = new[] { 60, 62 };
                    break;

                case Lf2EnemyPersonality.Fast:
                    _meleeRange = 1.0f;
                    _defendChance = 0.25f;
                    _attackCooldown = 0.25f;
                    _fleeHealthPercent = 0.15f;
                    _jumpChancePerSecond = 0.2f;
                    _thinkInterval = 0.05f;
                    _comboChain = new[] { 60, 61, 62, 60 };
                    break;

                case Lf2EnemyPersonality.Boss:
                    _meleeRange = 1.8f;
                    _defendChance = 0.3f;
                    _attackCooldown = 0.4f;
                    _fleeHealthPercent = 0f;
                    _jumpChancePerSecond = 0.06f;
                    _thinkInterval = 0.07f;
                    _chaseRange = 20f;
                    _comboChain = new[] { 60, 61, 62, 60, 61 };
                    break;

                case Lf2EnemyPersonality.MagicSupport:
                    _meleeRange = 0.7f;
                    _defendChance = 0.2f;
                    _attackCooldown = 0.7f;
                    _fleeHealthPercent = 0.3f;
                    _jumpChancePerSecond = 0.12f;
                    _canHealAllies = true;
                    _healCooldown = 8f;
                    _healRange = 6f;
                    _comboChain = new[] { 60, 61 };
                    break;
            }
        }
    }
}
