#if false
using Project.Gameplay.LF2;
using Project.Gameplay.Visual;
using UnityEngine;

namespace Project.Gameplay.Enemies
{
    public enum EnemyAIState
    {
        Idle,
        Approach,
        Attack,
        Flee,
        Defend,
    }

    [DisallowMultipleComponent]
    public sealed class Lf2EnemyAI : MonoBehaviour
    {
        private EnemyDefinition _def;
        private Transform _target;
        private Lf2OpointProcessor _opointProcessor;
        private Lf2EnemySpriteAnimator _animator;

        private EnemyAIState _state;
        private float _shotCooldownLeft;
        private float _defendTimeLeft;
        private Vector2 _steerDir;
        private bool _facingRight = true;

        private const float DefendDuration = 0.4f;
        private const float StateReevaluateInterval = 0.05f;
        private float _reevalTimer;

        public EnemyAIState State => _state;
        public Vector2 SteerDir => _steerDir;

        public void Configure(EnemyDefinition def, Transform target, Lf2OpointProcessor opointProcessor)
        {
            _def = def;
            _target = target;
            _opointProcessor = opointProcessor;
            _state = EnemyAIState.Approach;
            _shotCooldownLeft = 0f;
            _defendTimeLeft = 0f;
            _steerDir = Vector2.zero;
            _reevalTimer = 0f;

            if (_animator == null)
                _animator = GetComponent<Lf2EnemySpriteAnimator>();
        }

        private void Update()
        {
            if (_def == null || _target == null)
                return;

            _shotCooldownLeft -= Time.deltaTime;
            _defendTimeLeft -= Time.deltaTime;
            _reevalTimer -= Time.deltaTime;

            if (_defendTimeLeft > 0f)
            {
                _state = EnemyAIState.Defend;
                _steerDir = Vector2.zero;
                return;
            }

            if (_reevalTimer > 0f)
                return;
            _reevalTimer = StateReevaluateInterval;

            var toTarget = (Vector2)(_target.position - transform.position);
            float dist = toTarget.magnitude;

            if (dist < 0.001f)
            {
                _state = EnemyAIState.Idle;
                _steerDir = Vector2.zero;
                return;
            }

            var dirToTarget = toTarget / dist;

            if (dist < _def.fleeDistance)
            {
                _state = EnemyAIState.Flee;
                _steerDir = -dirToTarget;
            }
            else if (dist >= _def.rangedMinDistance && dist <= _def.rangedMaxDistance)
            {
                if (_shotCooldownLeft <= 0f)
                {
                    _state = EnemyAIState.Attack;
                    _steerDir = Vector2.zero;
                    FireProjectile(dirToTarget);
                    _shotCooldownLeft = _def.shotCooldown;
                }
                else
                {
                    _state = EnemyAIState.Idle;
                    _steerDir = Vector2.zero;
                }
            }
            else if (dist > _def.rangedMaxDistance)
            {
                _state = EnemyAIState.Approach;
                _steerDir = dirToTarget;
            }
            else
            {
                _state = EnemyAIState.Flee;
                _steerDir = -dirToTarget;
            }

            if (_steerDir.sqrMagnitude > 0.0001f)
            {
                _facingRight = _steerDir.x >= 0f;
                if (_animator != null)
                    _animator.SetFacing(_facingRight);
            }
        }

        public void TryDefend()
        {
            if (_def == null)
                return;

            if (Random.value < _def.defendChance)
                _defendTimeLeft = DefendDuration;
        }

        private void FireProjectile(Vector2 dirToTarget)
        {
            if (_opointProcessor == null)
                return;

            _facingRight = dirToTarget.x >= 0f;
            if (_animator != null)
            {
                _animator.SetFacing(_facingRight);
                _animator.PlayAttack();
            }

            var frame = new Lf2FrameData
            {
                Opoints = new[]
                {
                    new Lf2OpointData(
                        _def.projectileOid,
                        new Vector2(20f, 0f),
                        new Vector2(7f, 0f),
                        0)
                }
            };

            _opointProcessor.ProcessOpoints(frame, transform.position, _facingRight);
        }

    }
}
#endif
