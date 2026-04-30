using UnityEngine;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2GrabProcessor
    {
        private Lf2StateMachine _attackerSm;
        private Lf2StateMachine _victimSm;
        private Transform _victimTransform;
        private bool _isGrabbing;
        private bool _isGrabAttacking;
        private int _holdTicks;
        private int _grabAttackTicks;
        private const int MaxHoldTicks = 180;
        private const float GrabOffsetX = 0.4f;
        private const float GrabOffsetY = 0f;

        public bool IsGrabbing => _isGrabbing;
        public Lf2StateMachine Victim => _victimSm;

        public void Initialize(Lf2StateMachine attackerSm)
        {
            _attackerSm = attackerSm;
        }

        public bool TryGrab(Lf2StateMachine victim, Transform victimTransform)
        {
            if (_isGrabbing) return false;
            if (victim == null) return false;

            _victimSm = victim;
            _victimTransform = victimTransform;
            _isGrabbing = true;
            _isGrabAttacking = false;
            _holdTicks = 0;
            _grabAttackTicks = 0;

            _victimSm.SetVelocityDirect(Vector2.zero);
            _victimSm.SetHoldFrame(true);

            var roles = _attackerSm?.Roles;
            _attackerSm.SetFrame(roles?.Catching ?? 150);
            _victimSm.SetFrame(roles?.Caught ?? 155);

            SyncVictimPosition();
            return true;
        }

        public bool Tick(bool attackPressed, Vector2 throwDir)
        {
            if (!_isGrabbing) return false;

            _holdTicks++;
            SyncVictimPosition();

            if (_holdTicks >= MaxHoldTicks)
            {
                Release();
                return true;
            }

            if (_isGrabAttacking)
            {
                _grabAttackTicks++;
                var attackerFrame = _attackerSm?.CurrentFrame;
                int wait = attackerFrame?.Wait ?? 0;
                if (_grabAttackTicks > wait)
                {
                    _isGrabAttacking = false;
                    _grabAttackTicks = 0;
                    var roles = _attackerSm?.Roles;
                    _attackerSm.SetFrame(roles?.Catching ?? 150);
                }
                return false;
            }

            if (throwDir.sqrMagnitude > 0.0001f && attackPressed)
            {
                Throw(throwDir);
                return true;
            }

            if (attackPressed)
            {
                var roles = _attackerSm?.Roles;
                _attackerSm.SetFrame(roles?.GrabAttack ?? 160);
                _isGrabAttacking = true;
                _grabAttackTicks = 0;
                return false;
            }

            return false;
        }

        private void SyncVictimPosition()
        {
            if (_victimTransform == null || _attackerSm == null) return;

            var attackerOwner = _attackerSm.OwnerTransform;
            if (attackerOwner == null) return;

            float dir = _attackerSm.FacingRight ? 1f : -1f;
            var victimPos = _victimTransform.position;
            victimPos.x = attackerOwner.position.x + GrabOffsetX * dir;
            victimPos.y = attackerOwner.position.y + GrabOffsetY;
            _victimTransform.position = victimPos;
        }

        private void Throw(Vector2 dir)
        {
            if (_victimSm != null)
            {
                _victimSm.SetHoldFrame(false);
                var roles = _attackerSm?.Roles;
                _victimSm.SetFrame(roles?.Throw ?? 170);
                _victimSm.SetVelocityDirect(Vector2.zero);
                _victimSm.ApplyKnockback(dir.x * 5f, dir.y * 3f);
            }
            Release();
        }

        private void Release()
        {
            if (_victimSm != null)
                _victimSm.SetHoldFrame(false);

            _isGrabbing = false;
            _isGrabAttacking = false;
            _victimSm = null;
            _victimTransform = null;
            _holdTicks = 0;
            _grabAttackTicks = 0;

            if (_attackerSm != null)
            {
                var roles = _attackerSm.Roles;
                _attackerSm.SetFrame(roles?.Standing ?? 0);
            }
        }

        public void Cancel()
        {
            if (_isGrabbing)
                Release();
        }
    }
}
