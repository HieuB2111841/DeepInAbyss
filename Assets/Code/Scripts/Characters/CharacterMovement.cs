using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class CharacterMovement : EntityMovement, ICharacterComponent, IWalkable, IJumpable, ICrouchable, IClimbable
    {
        private Character _character;
        public Character Character => _character;


        protected float _currentSpeed;

        protected bool _canJump = true;
        protected float _jumpCoolDown = 0.5f;

        protected bool _canUncrouch = true;
        protected List<Collider2D> _couchColliders = new();
        protected ContactFilter2D _crouchContactFilter = new();

        protected bool _isClimb = false;
        protected bool _canClimb = false;

        #region Properties
        public virtual float Speed
        {
            get => _currentSpeed;
            set => _currentSpeed = value;
        }

        public virtual float WalkSpeed => Character.Stats?.Speed ?? 0f;
        public virtual bool IsWalk
        {
            get => false;
        }

        public virtual bool CanWalk
        {
            get => false;
        }


        public virtual float JumpCoolDown => _jumpCoolDown;
        public virtual float JumpHeight => Character?.Stats.JumpHeight ?? 0;
        public virtual bool CanJump
        {
            get => _canJump;
            set => _canJump = value;
        }


        public virtual bool IsCrouch
        {
            get => Character.HeadCollider?.isTrigger ?? false;
            set
            {
                Character.HeadCollider.isTrigger = value;
                Character.Animation.SetIsCrouch(value);
            }
        }

        public virtual bool CanCrouch
        {
            get => IsGrounded && !IsClimb;
        }

        public virtual bool CanUncrouch
        {
            get => _canUncrouch;
            set
            {
                _canUncrouch = value;
                if (!_canUncrouch)
                {
                    IsCrouch = true;
                }
            }
        }

        public virtual bool IsClimb
        {
            get => _isClimb;
            set
            {
                _isClimb = value;
                Character.Animation.SetIsClimb(value);
                if (_isClimb) Entity.Rigidbody2D.gravityScale = 0f;
                else Entity.Rigidbody2D.gravityScale = Entity.Stats.GravityScale;
            }
        }

        public virtual bool CanClimb
        {
            get => _canClimb;
            set
            {
                _canClimb = value;
            }
        }

        #endregion


        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _character);
        }

        protected override void Start()
        {
            base.Start();
            _crouchContactFilter.SetLayerMask(_groundLayer);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            this.CrouchHandle();
            this.ClimbHandle();
            this.SpeedHandle();
        }

        public override void Move(Vector2 axis)
        {
            Crouch(axis.y < 0);
            if ((CanClimb && axis.y != 0) || IsClimb)
            {
                Climb(axis);
            }
            else
            {
                this.Walk(axis.x);
            }
        }

        public virtual void Jump()
        {
            if (CanJump && CanUncrouch && (IsGrounded || IsClimb))
            {
                this.SetVelocity(y: 0f);
                this.AddForce(Vector2.up * Entity.Stats.JumpHeight);
                IsClimb = false;

                CanJump = false;
                Invoke(nameof(ResetJump), _jumpCoolDown);
            }
        }

        public virtual void ResetJump()
        {
            CanJump = true;
        }

        public virtual void Crouch(bool isCrouch)
        {
            if (CanCrouch && CanUncrouch)
            {
                IsCrouch = isCrouch;
            }
        }

        protected virtual void CrouchHandle()
        {
            if (IsGrounded)
            {
                // check headCollider collide with ground or not
                if (Character.HeadCollider.OverlapCollider(_crouchContactFilter, _couchColliders) > 0)
                {
                    CanUncrouch = false;
                }
                else CanUncrouch = true;
            }
            else
            {
                IsCrouch = false;
            }
        }

        public virtual void Climb(Vector2 axis)
        {
            IsClimb = true;
            if (axis.magnitude == 0)
            {
                this.SetVelocity(0f, 0f);
            }
            else
            {
                this.MoveByDirection(Speed * axis.normalized);

                float xClamp = Mathf.Abs(axis.x) * Speed;
                float yClamp = Mathf.Abs(axis.y) * Speed;

                this.ClampVelocity(xClamp, yClamp);
            }
            Character.Animation.SetIsClimbIdle(axis.magnitude == 0);
        }

        protected virtual void ClimbHandle()
        {
            if (!CanClimb || IsGrounded)
            {
                IsClimb = false;
            }
        }

        public virtual void Walk(float axis)
        {
            this.MoveByDirection(Vector2.right * Speed * axis);
            this.ClampVelocity(xClamp: Speed);
            Character.Animation.Flip(axis);
        }

        /// <summary>
        ///     Change speed according to character state
        /// </summary>
        protected virtual void SpeedHandle()
        {
            if (IsClimb) Speed = Entity.Stats.ClimbSpeed;
            else if (IsCrouch) Speed = Entity.Stats.CrouchSpeed;
            else if (IsOnAir) Speed = Entity.Stats.AirSpeed;
            else Speed = WalkSpeed;

            Character.Animation.SetSpeed(Character.Rigidbody2D.velocity);
        }

    }
}
