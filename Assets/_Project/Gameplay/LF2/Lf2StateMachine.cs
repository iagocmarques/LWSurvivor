using Project.Gameplay.Audio;
using Project.Gameplay.Player;
using UnityEngine;

namespace Project.Gameplay.LF2
{
    public sealed class Lf2StateMachine
    {
        public const float PixelToUnit = 0.1f;

        private Lf2CharacterData _character;
        private Lf2FrameChain _chain;
        private int _currentFrameId;

        private Vector2 _velocity;
        private float _groundY;
        private bool _isGrounded;
        private float _gravity = -0.05f;

        private bool _facingRight = true;

        private Lf2State _currentState;
        private int _ticksInState;

        private int _currentPic;

        private bool _holdFrame;

        private readonly Lf2RunDetector _runDetector = new Lf2RunDetector();
        private Lf2ItrProcessor _itrProcessor;

        private Lf2FrameRoleIds _roles;

        private bool _hasBufferedAttack;
        private int _bufferedAttackTick;
        private int _tickCount;

        private Lf2GrabProcessor _grabProcessor;
        private Lf2OpointProcessor _opointProcessor;
        private Transform _ownerTransform;
        private bool _alwaysProcessItr;
        private System.Func<int, bool> _trySpendMana;
        private int _attackStartFrameId = -1;
        private int _attackFrameIndex;

        public Lf2CharacterData Character => _character;
        public Lf2FrameData CurrentFrame => _chain?.Current;
        public int CurrentFrameId => _currentFrameId;
        public Lf2State CurrentState => _currentState;
        public int CurrentPic => _currentPic;
        public Vector2 Velocity => _velocity;
        public bool IsGrounded => _isGrounded;
        public bool IsAirborne => !_isGrounded;
        public bool FacingRight => _facingRight;
        public int TicksInState => _ticksInState;
        public bool IsFinished => _chain?.IsFinished ?? true;
        public float Gravity { get => _gravity; set => _gravity = value; }
        public Lf2FrameRoleIds Roles => _roles;
        public int AttackStartFrameId => _attackStartFrameId;
        public int AttackFrameIndex => _attackFrameIndex;

        public void Initialize(Lf2CharacterData character, float groundY)
        {
            _character = character;
            _groundY = groundY;
            _chain = new Lf2FrameChain(character);
            _velocity = Vector2.zero;
            _isGrounded = true;
            _facingRight = true;
            _ticksInState = 0;

            _roles = character?.RoleIds ?? new Lf2FrameRoleIds();

            SetFrame(_roles.Standing);
        }

        public void SetItrProcessor(Lf2ItrProcessor processor)
        {
            _itrProcessor = processor;
        }

        public Lf2GrabProcessor GrabProcessor => _grabProcessor;
        public Transform OwnerTransform => _ownerTransform;

        public void InitializeGrabProcessor()
        {
            _grabProcessor = new Lf2GrabProcessor();
            _grabProcessor.Initialize(this);
            if (_itrProcessor != null)
                _itrProcessor.SetGrabProcessor(_grabProcessor);
        }

        public void SetOpointProcessor(Lf2OpointProcessor processor)
        {
            _opointProcessor = processor;
        }

        public void SetOwnerTransform(Transform owner)
        {
            _ownerTransform = owner;
        }

        public void SetVelocityDirect(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public void SetFacingRight(bool facingRight)
        {
            _facingRight = facingRight;
        }

        public void SetAlwaysProcessItr(bool value)
        {
            _alwaysProcessItr = value;
        }

        public void SetHoldFrame(bool hold)
        {
            _holdFrame = hold;
        }

        public void SetManaSpendCallback(System.Func<int, bool> callback)
        {
            _trySpendMana = callback;
        }

        private SpecialMoveId DetectAttackSpecial(in Lf2InputScheme.Lf2InputState input)
        {
            const float threshold = 0.5f;
            float moveX = input.MoveDir.x;
            float moveY = input.MoveDir.y;

            bool isUp = moveY > threshold;
            bool isDown = moveY < -threshold;
            bool isForward = (_facingRight && moveX > threshold) || (!_facingRight && moveX < -threshold);

            if (isUp || isDown)
                return SpecialMoveId.DragonPunch;

            if (isForward)
                return SpecialMoveId.EnergyBlast;

            return SpecialMoveId.None;
        }

        private SpecialMoveId DetectJumpSpecial(in Lf2InputScheme.Lf2InputState input)
        {
            const float threshold = 0.5f;
            float moveX = input.MoveDir.x;
            float moveY = input.MoveDir.y;

            bool isUp = moveY > threshold;
            bool isDown = moveY < -threshold;
            bool isForward = (_facingRight && moveX > threshold) || (!_facingRight && moveX < -threshold);

            if (isUp)
                return SpecialMoveId.LeapAttack;

            if (isForward)
                return SpecialMoveId.Shrafe;

            if (isDown)
                return SpecialMoveId.DownJump;

            return SpecialMoveId.None;
        }

        private int GetSpecialFrameId(SpecialMoveId special)
        {
            switch (special)
            {
                case SpecialMoveId.EnergyBlast: return _roles.EnergyBlast;
                case SpecialMoveId.Shrafe: return _roles.Shrafe;
                case SpecialMoveId.LeapAttack: return _roles.LeapAttack;
                case SpecialMoveId.DragonPunch: return _roles.DragonPunch;
                case SpecialMoveId.DownAttack: return _roles.DownAttack;
                case SpecialMoveId.DownJump: return _roles.DownJump;
                default: return 0;
            }
        }

        private int GetSpecialCost(SpecialMoveId special)
        {
            switch (special)
            {
                case SpecialMoveId.EnergyBlast: return _roles.EnergyBlastMpCost;
                case SpecialMoveId.Shrafe: return _roles.ShrafeMpCost;
                case SpecialMoveId.LeapAttack: return _roles.LeapAttackMpCost;
                case SpecialMoveId.DragonPunch: return _roles.DragonPunchMpCost;
                case SpecialMoveId.DownAttack: return _roles.DownAttackMpCost;
                case SpecialMoveId.DownJump: return _roles.DownJumpMpCost;
                default: return 0;
            }
        }

        public bool HasFrame(int frameId)
        {
            return _character?.Frames != null && _character.Frames.ContainsKey(frameId);
        }

        private bool TryExecuteSpecial(SpecialMoveId special)
        {
            int frameId = GetSpecialFrameId(special);
            if (!HasFrame(frameId))
                return false;
            if (_trySpendMana != null && !_trySpendMana(GetSpecialCost(special)))
                return false;
            SetFrame(frameId);
            return true;
        }

        private void ProcessFrameOpoints(Lf2FrameData frame)
        {
            if (_opointProcessor != null && frame.Opoints != null && frame.Opoints.Length > 0)
            {
                var ownerPos = _ownerTransform != null ? (Vector2)_ownerTransform.position : Vector2.zero;
                _opointProcessor.ProcessOpoints(frame, ownerPos, _facingRight);
            }
        }

        public bool SetFrame(int frameId)
        {
            if (_chain == null) return false;
            if (!_chain.SetFrame(frameId)) return false;

            _currentFrameId = frameId;
            var frame = _chain.Current;

            if (frame != null)
            {
                bool wasAttacking = _currentState == Lf2State.Attacking || _currentState == Lf2State.Catching;
                _currentState = frame.State;
                _currentPic = frame.Pic;

                bool isAttacking = _currentState == Lf2State.Attacking || _currentState == Lf2State.Catching;
                if (isAttacking)
                {
                    if (!wasAttacking)
                    {
                        _attackStartFrameId = frameId;
                        _attackFrameIndex = 0;
                    }
                    else
                    {
                        _attackFrameIndex++;
                    }
                }
                else
                {
                    _attackStartFrameId = -1;
                    _attackFrameIndex = 0;
                }

                ApplyFrameVelocity(frame);
                ProcessFrameOpoints(frame);
                PlayFrameSound(frame);
            }

            _ticksInState = 0;
            return true;
        }

        public void Tick()
        {
            if (_chain?.Current == null) return;

            _ticksInState++;

            if (!_isGrounded)
                _velocity.y += _gravity;

            if (_holdFrame)
                return;

            if (_currentState == Lf2State.Attacking || _currentState == Lf2State.Catching)
            {
                if (_ticksInState > _chain.Current.Wait)
                {
                    int next = _chain.Current.Next;
                    if (next == 999 || next < 0 || !HasFrame(next))
                        SetFrame(_roles.Standing);
                    else
                        SetFrame(next);
                }
            }
            else
            {
                bool advanced = _chain.Tick();
                if (advanced && _chain.Current != null)
                {
                    _currentFrameId = _chain.Current.Id;
                    _currentState = _chain.Current.State;
                    _currentPic = _chain.Current.Pic;
                    ApplyFrameVelocity(_chain.Current);
                    ProcessFrameOpoints(_chain.Current);
                    PlayFrameSound(_chain.Current);
                }

                if (_chain.IsFinished && _isGrounded)
                {
                    if (_currentState != Lf2State.Caught)
                        SetFrame(_roles.Standing);
                }
            }

            if (_itrProcessor != null)
            {
                bool shouldTickItr = _alwaysProcessItr
                    || _currentState == Lf2State.Attacking
                    || _currentState == Lf2State.Catching
                    || _currentState == Lf2State.Caught
                    || _currentState == Lf2State.Standing
                    || _currentState == Lf2State.Walking
                    || _currentState == Lf2State.Running;
                if (shouldTickItr)
                    _itrProcessor.Tick();
            }
        }

        public Lf2Action ProcessInput(in Lf2InputScheme.Lf2InputState input, bool facingRight, float time)
        {
            _facingRight = facingRight;
            _tickCount++;

            if (_hasBufferedAttack && _tickCount - _bufferedAttackTick >= 8)
                _hasBufferedAttack = false;

            var action = Lf2Action.None;

            if (_currentState == Lf2State.Defending)
            {
                if (input.DefendHeld)
                {
                    if (input.AttackPressed)
                    {
                        var special = DetectAttackSpecial(in input);
                        if (special != SpecialMoveId.None && TryExecuteSpecial(special))
                        {
                            _holdFrame = false;
                            _hasBufferedAttack = false;
                            return Lf2Action.Special;
                        }
                    }

                    if (input.JumpPressed)
                    {
                        var special = DetectJumpSpecial(in input);
                        if (special != SpecialMoveId.None && TryExecuteSpecial(special))
                        {
                            _holdFrame = false;
                            _hasBufferedAttack = false;
                            return Lf2Action.Special;
                        }
                    }

                    _holdFrame = true;
                    SetFrame(_roles.Defend);
                    return Lf2Action.Defend;
                }

                _holdFrame = false;
                SetFrame(_roles.Standing);
                return Lf2Action.None;
            }

            if ((_currentState == Lf2State.Catching || _currentState == Lf2State.Caught)
                && _grabProcessor != null && _grabProcessor.IsGrabbing)
            {
                var throwDir = input.MoveDir;
                bool atk = input.AttackPressed;
                bool ended = _grabProcessor.Tick(atk, throwDir);
                if (!ended && atk && throwDir.sqrMagnitude > 0.0001f)
                    return Lf2Action.Throw;
                if (!ended && atk)
                    return Lf2Action.Attack;
                if (ended)
                {
                    SetFrame(_roles.Standing);
                    return Lf2Action.None;
                }
                return Lf2Action.Grab;
            }

            if (_currentState == Lf2State.Caught && _holdFrame)
                return Lf2Action.None;

            _holdFrame = false;

            if (_currentState == Lf2State.Attacking)
            {
                bool wantsAttack = input.AttackPressed || _hasBufferedAttack;
                if (wantsAttack && _chain?.Current != null && IsInCancelWindow(_chain.Current))
                {
                    int next = _chain.Current.Next;
                    if (next != 999 && next >= 0)
                    {
                        SetFrame(next);
                        _hasBufferedAttack = false;
                        return Lf2Action.Attack;
                    }

                    SetFrame(_roles.Standing);
                    _hasBufferedAttack = false;
                    return Lf2Action.None;
                }

                if (input.AttackPressed)
                {
                    _hasBufferedAttack = true;
                    _bufferedAttackTick = _tickCount;
                }

                return Lf2Action.None;
            }

            if (_currentState == Lf2State.Standing || _currentState == Lf2State.Walking || _currentState == Lf2State.Running)
            {
                if (_hasBufferedAttack && _tickCount - _bufferedAttackTick < 8)
                {
                    _hasBufferedAttack = false;
                    _runDetector.Cancel();
                    SetFrame(ResolveAttackFrame(input));
                    return Lf2Action.Attack;
                }
                _hasBufferedAttack = false;

                if (input.DefendHeld)
                {
                    if (input.AttackPressed)
                    {
                        var special = DetectAttackSpecial(in input);
                        if (special != SpecialMoveId.None && TryExecuteSpecial(special))
                        {
                            _runDetector.Cancel();
                            return Lf2Action.Special;
                        }
                    }

                    if (input.JumpPressed)
                    {
                        var special = DetectJumpSpecial(in input);
                        if (special != SpecialMoveId.None && TryExecuteSpecial(special))
                        {
                            _runDetector.Cancel();
                            return Lf2Action.Special;
                        }
                    }

                    _runDetector.Cancel();
                    SetFrame(_roles.Defend);
                    return Lf2Action.Defend;
                }

                if (input.JumpPressed)
                {
                    _runDetector.Cancel();

                    float moveX = input.MoveDir.x;
                    if (Mathf.Abs(moveX) > 0.1f)
                        _facingRight = moveX > 0f;

                    SetFrame(_roles.Jump);

                    var jf = _chain?.Current;
                    if (jf != null && jf.Dvx == 0 && Mathf.Abs(moveX) > 0.1f)
                        _velocity.x = (moveX > 0f ? 1f : -1f) * 3f * PixelToUnit;

                    _isGrounded = false;
                    return Lf2Action.Jump;
                }

                if (input.AttackPressed && _currentState == Lf2State.Running)
                {
                    SetFrame(_roles.AttackNeutral);
                    return Lf2Action.Attack;
                }

                if (input.AttackPressed)
                {
                    _runDetector.Cancel();
                    SetFrame(ResolveAttackFrame(input));
                    return Lf2Action.Attack;
                }

                if (_runDetector.Update(input.MoveDir, input.MovePressed, time))
                {
                    SetFrame(_roles.Running);
                    return Lf2Action.Run;
                }

                if (_runDetector.IsRunning)
                {
                    if (_currentState != Lf2State.Running)
                        SetFrame(_roles.Running);
                    return Lf2Action.Run;
                }

                if (input.MovePressed)
                {
                    if (_currentState != Lf2State.Walking)
                        SetFrame(_roles.Walking);
                    return Lf2Action.Walk;
                }

                if (_currentState != Lf2State.Standing)
                    SetFrame(_roles.Standing);
            }

            return action;
        }

        private static bool IsInCancelWindow(Lf2FrameData frame)
        {
            if (frame.Itrs != null && frame.Itrs.Length > 0) return true;
            if (frame.ExtraProps != null && frame.ExtraProps.TryGetValue("cancel", out var v) && v == "1") return true;
            return false;
        }

        private int ResolveAttackFrame(in Lf2InputScheme.Lf2InputState input)
        {
            float moveX = input.MoveDir.x;
            if (Mathf.Abs(moveX) > 0.1f)
            {
                bool isForward = (_facingRight && moveX > 0f) || (!_facingRight && moveX < 0f);
                int frameId = isForward ? _roles.AttackForward : _roles.AttackBack;
                if (_character?.Frames != null && _character.Frames.ContainsKey(frameId))
                    return frameId;
            }

            return _roles.AttackNeutral;
        }

        private void ApplyFrameVelocity(Lf2FrameData frame)
        {
            if (frame.Dvx != 0)
            {
                var dir = _facingRight ? 1f : -1f;
                _velocity.x = frame.Dvx * dir * PixelToUnit;
            }

            if (frame.Dvy != 0)
            {
                _velocity.y = frame.Dvy * PixelToUnit;
                if (frame.Dvy > 0)
                    _isGrounded = false;
            }
        }

        private static void PlayFrameSound(Lf2FrameData frame)
        {
            if (frame.SoundId <= 0) return;
            var audio = Lf2AudioManager.Instance;
            if (audio != null)
                audio.PlaySfx(frame.SoundId);
        }

        public void SetPosition(ref Vector3 position)
        {
            position.x += _velocity.x;
            position.y += _velocity.y;

            if (position.y <= _groundY)
            {
                position.y = _groundY;
                _velocity = Vector2.zero;
                _isGrounded = true;

                if (_currentState == Lf2State.Jumping)
                    SetFrame(_roles.Standing);
                else if (_currentState == Lf2State.Falling || _currentState == Lf2State.FallingAlt)
                    SetFrame(_roles.Lying);
            }
        }

        public void ApplyKnockback(float dvx, float dvy)
        {
            var dir = _facingRight ? -1f : 1f;
            _velocity.x = dvx * dir;
            _velocity.y = dvy;
            if (dvy > 0) _isGrounded = false;
        }
    }

    public enum Lf2Action
    {
        None,
        Walk,
        Run,
        Jump,
        Attack,
        Defend,
        Special,
        Grab,
        Throw,
        Pickup,
    }

    public enum SpecialMoveId
    {
        None,
        EnergyBlast,
        Shrafe,
        LeapAttack,
        DragonPunch,
        DownAttack,
        DownJump,
    }
}
