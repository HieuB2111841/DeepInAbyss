using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class EntityMovement : EntityAbstraction
    {

        protected float _currentSpeed;
        protected bool _isGrounded = false;
        protected bool _isClimb = false;

        protected bool _canUncrouch = true;
        protected bool _canClimb = false;
        protected bool _canJump = true;
        protected float _jumpCoolDown = 0.5f;

        [SerializeField] protected LayerMask _groundLayer;

        protected List<Collider2D> _colliders = new();
        protected ContactFilter2D _crouchContactFilter = new();


        #region Properties
        public virtual float Speed
        {
            get => _currentSpeed;
            set => _currentSpeed = value;
        }

        public virtual bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }

        public virtual bool IsCrouch
        {
            get => Entity.HeadCollider?.isTrigger ?? false;
            set
            {
                Entity.HeadCollider.isTrigger = value;
            }
        }

        public virtual bool IsClimb
        {
            get => _isClimb;
            set
            {
                _isClimb = value;
                if (_isClimb) Entity.Rigidbody2D.gravityScale = 0f;
                else Entity.Rigidbody2D.gravityScale = Entity.Stats.GravityScale;
            }
        }

        public bool IsOnAir
        {
            get => !IsGrounded && !IsClimb;
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

        public virtual bool CanClimb
        {
            get => _canClimb;
            set
            {
                _canClimb = value;
            }
        }

        public virtual bool CanJump
        {
            get => _canJump;
            set => _canJump = value;
        }
        #endregion


        protected virtual void Start()
        {
            _crouchContactFilter.SetLayerMask(_groundLayer);
        }

        public virtual void Move(Vector2 axis)
        {
            Crouch(axis.y < 0);
            if ((CanClimb && axis.y != 0) || IsClimb)
            {
                Climb(axis);
            }
            else
            {
                this.HorizontalMove(axis.x);
            }
        }


        protected virtual void FixedUpdate()
        {
            this.GroundHandle();
            this.CrouchHandle();
            this.ClimbHandle();
            this.SpeedHandle();
        }

        public virtual void HorizontalMove(float axis)
        {
            this.MoveByDirection(Vector2.right * Speed * axis);
            this.ClampVelocity(xClamp: Speed);
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


        public virtual void Crouch(bool state)
        {
            if (IsGrounded && CanUncrouch)
            {
                IsCrouch = state;
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
        }

        /// <summary>
        ///     Change speed according to character state
        /// </summary>
        protected virtual void SpeedHandle()
        {
            if (IsClimb) Speed = Entity.Stats.ClimbSpeed;
            else if (IsCrouch) Speed = Entity.Stats.CrouchSpeed;
            else if (IsOnAir) Speed = Entity.Stats.AirSpeed;
            else Speed = Entity.Stats.Speed;
        }

        /// <summary>
        ///     Always check the character is collided with ground or not
        /// </summary>
        protected virtual void GroundHandle()
        {
            Vector2 origin = (Vector2)transform.position + (Vector2.down * 0.6f);
            Collider2D[] hits = Physics2D.OverlapCircleAll(origin, 0.2f, _groundLayer);
            if (hits.Length > 0) IsGrounded = true;
            else IsGrounded = false;
        }

        protected virtual void CrouchHandle()
        {
            if (IsGrounded)
            {
                // check headCollider collide with ground or not
                if (Entity.HeadCollider.OverlapCollider(_crouchContactFilter, _colliders) > 0)
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

        protected virtual void ClimbHandle()
        {
            if (!CanClimb || IsGrounded)
            {
                IsClimb = false;
            }
        }

        protected virtual void ResetJump()
        {
            CanJump = true;
        }

        /// <summary>
        ///     Limit each direction of velocity according to the passed parameter. 
        ///     <br/>
        ///     If that direction is null, that direction of velocity will be kept unchanged.
        /// </summary>
        protected virtual void ClampVelocity(float? xClamp = null, float? yClamp = null)
        {
            Vector2 velocity = Entity.Rigidbody2D.velocity;

            if (xClamp != null && Mathf.Abs(velocity.x) > xClamp)
            {
                velocity.x = (float)xClamp * Mathf.Sign(velocity.x);
            }

            if (yClamp != null && Mathf.Abs(velocity.y) > yClamp)
            {
                velocity.y = (float)yClamp * Mathf.Sign(velocity.y);
            }

            Entity.Rigidbody2D.velocity = velocity;
        }

        protected virtual void SetVelocity(float? x = null, float? y = null)
        {
            float xVelocity = x ?? Entity.Rigidbody2D.velocity.x;
            float yVelocity = y ?? Entity.Rigidbody2D.velocity.y;
            Entity.Rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
        }

        /// <summary>
        ///     Move character by <paramref name="direction"/> after calculating the force
        /// </summary>
        /// <param name="direction"></param>
        protected virtual void MoveByDirection(Vector2 direction)
        {
            Vector2 tranfer = direction * Entity.Rigidbody2D.mass * 100f;
            Entity.Rigidbody2D.AddForce(tranfer, ForceMode2D.Force);
        }

        protected virtual void AddForce(Vector2 force)
        {
            Vector2 finalForce = force * Entity.Rigidbody2D.mass * 6f;
            Entity.Rigidbody2D.AddForce(finalForce, ForceMode2D.Impulse);
        }
    }
}
