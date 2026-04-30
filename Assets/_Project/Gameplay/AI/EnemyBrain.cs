using Project.Gameplay.LF2;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.AI
{
    public enum AIState
    {
        Idle,
        SeekTarget,
        Approach,
        Attack,
        Retreat,
        Defend,
        Recover
    }

    public sealed class EnemyBrain : MonoBehaviour
    {
        [Header("Think Rate")]
        [SerializeField] private float thinkInterval = 0.1f;

        [Header("Archetype")]
        [SerializeField] private AIArchetype archetype;

        private Lf2StateMachine _sm;
        private Transform _target;
        private AIState _currentState = AIState.Idle;
        private float _thinkTimer;
        private float _stateTimer;
        private bool _facingRight = true;
        private float _lastAttackTime;

        private AISensorData _sensors;

        public AIState CurrentState => _currentState;
        public AISensorData Sensors => _sensors;
        public bool FacingRight => _facingRight;

        public void Initialize(Lf2StateMachine sm, AIArchetype arch)
        {
            _sm = sm;
            archetype = arch;
            if (arch != null)
                thinkInterval = arch.thinkInterval;
        }

        public void SetTarget(Transform target) => _target = target;

        private void Update()
        {
            if (_sm == null) return;

            _thinkTimer += Time.deltaTime;
            if (_thinkTimer >= thinkInterval)
            {
                _thinkTimer = 0f;
                Think();
            }

            var input = GenerateInput();
            _sm.ProcessInput(in input, _facingRight, Time.time);
            _sm.Tick();
        }

        private void Think()
        {
            UpdateSensors();
            TransitionState();
            _stateTimer += thinkInterval;
        }

        private void UpdateSensors()
        {
            if (_target == null) return;

            var delta = _target.position - transform.position;
            _sensors.HorizontalDistance = Mathf.Abs(delta.x);
            _sensors.DepthDifference = delta.y;
            _sensors.TargetVisible = true;
            _sensors.TimeSinceLastAttack = Time.time - _lastAttackTime;

            var targetSm = _target.GetComponentInChildren<Lf2StateMachine>();
            if (targetSm != null)
            {
                _sensors.TargetStunned = targetSm.CurrentState == Lf2State.Lying
                    || targetSm.CurrentState == Lf2State.Injured;
                _sensors.TargetAirborne = targetSm.IsAirborne;
            }
            else
            {
                _sensors.TargetStunned = false;
                _sensors.TargetAirborne = false;
            }
        }

        private void TransitionState()
        {
            if (_target == null)
            {
                _currentState = AIState.Idle;
                return;
            }

            var newState = _currentState;

            switch (_currentState)
            {
                case AIState.Idle:
                    if (_sensors.TargetVisible)
                        newState = AIState.SeekTarget;
                    break;

                case AIState.SeekTarget:
                    if (_sensors.TargetStunned)
                        newState = AIState.Approach;
                    else if (_sensors.HorizontalDistance < archetype.attackRange)
                        newState = AIState.Attack;
                    else
                        newState = AIState.Approach;
                    break;

                case AIState.Approach:
                    if (_sensors.HorizontalDistance < archetype.attackRange)
                        newState = AIState.Attack;
                    break;

                case AIState.Attack:
                    if (_stateTimer > archetype.attackCooldown)
                    {
                        _stateTimer = 0f;
                        if (_sensors.TargetStunned)
                            newState = AIState.Approach;
                        else if (archetype.aggression < 0.3f && Random.value < archetype.defendChance)
                            newState = AIState.Defend;
                        else if (_sensors.HorizontalDistance > archetype.retreatRange)
                            newState = AIState.Retreat;
                        else
                            newState = AIState.Approach;
                    }
                    break;

                case AIState.Retreat:
                    if (_sensors.HorizontalDistance > archetype.retreatRange)
                        newState = AIState.Approach;
                    break;

                case AIState.Defend:
                    if (_stateTimer > archetype.defendDuration)
                    {
                        _stateTimer = 0f;
                        newState = AIState.Recover;
                    }
                    break;

                case AIState.Recover:
                    if (_stateTimer > archetype.recoverDuration)
                    {
                        _stateTimer = 0f;
                        newState = AIState.SeekTarget;
                    }
                    break;
            }

            if (newState != _currentState)
            {
                _currentState = newState;
                _stateTimer = 0f;
            }
        }

        private Lf2InputScheme.Lf2InputState GenerateInput()
        {
            var input = new Lf2InputScheme.Lf2InputState();

            if (_target == null) return input;

            var toTarget = _target.position.x - transform.position.x;
            _facingRight = toTarget > 0;

            switch (_currentState)
            {
                case AIState.Approach:
                    input.MoveDir = new Vector2(Mathf.Sign(toTarget), 0);
                    input.MovePressed = true;
                    break;

                case AIState.Retreat:
                    input.MoveDir = new Vector2(-Mathf.Sign(toTarget), 0);
                    input.MovePressed = true;
                    break;

                case AIState.Attack:
                    input.AttackPressed = true;
                    break;

                case AIState.Defend:
                    input.DefendHeld = true;
                    break;
            }

            return input;
        }

        public void OnDamaged()
        {
            if (archetype == null) return;
            if (_currentState == AIState.Attack || _currentState == AIState.Defend)
                return;

            if (Random.value < archetype.defendChance)
            {
                _currentState = AIState.Defend;
                _stateTimer = 0f;
            }
        }
    }
}
