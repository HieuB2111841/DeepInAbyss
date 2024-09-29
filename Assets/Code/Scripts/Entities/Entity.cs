using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.Extension;

namespace Game.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : MonoBehaviour
    {
        protected Rigidbody2D _rigidbody2D;
        protected EntityStats _stats;

        [SerializeField] private Collider2D _headCollider;
        [SerializeField] private Collider2D _bodyCollider;

        [SerializeField] protected LayerMask _groundLayer;

        protected float _currentSpeed;
        protected bool _isGrounded = false;
        protected bool _isClimb = false;

        protected bool _canUncrouch = true;
        protected bool _canClimb = false;
        protected bool _canJump = true;
        protected float _jumpCoolDown = 0.5f;

        protected List<Collider2D> _colliders = new();
        protected ContactFilter2D _crouchContactFilter = new();


        #region Properties

        protected Collider2D HeadCollider => _headCollider;
        protected Collider2D BodyCollider => _bodyCollider;
        public EntityStats Stats => _stats;

        public virtual float Speed
        {
            get => _currentSpeed;
            protected set => _currentSpeed = value;
        }

        public virtual bool IsGrounded
        {
            get => _isGrounded;
            protected set => _isGrounded = value;
        }

        public virtual bool IsCrouch
        {
            get => HeadCollider?.isTrigger ?? false;
            protected set
            {
                HeadCollider.isTrigger = value;
            }
        }

        public virtual bool IsClimb
        {
            get => _isClimb;
            protected set
            {
                _isClimb = value;
                if (_isClimb) _rigidbody2D.gravityScale = 0f;
                else _rigidbody2D.gravityScale = Stats.GravityScale;
            }
        }

        public bool IsOnAir
        {
            get => !IsGrounded && !IsClimb;
        }

        public virtual bool CanUncrouch
        {
            get => _canUncrouch;
            protected set
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
            protected set
            {
                _canClimb = value;
            }
        }

        public virtual bool CanJump
        {
            get => _canJump;
            protected set => _canJump = value;
        }
        #endregion

        protected virtual void Awake()
        {
            this.LoadComponents();
        }

        protected virtual void LoadComponents()
        {
            this.LoadComponent(ref _rigidbody2D);
            this.LoadComponent(ref _stats);

            this.CheckComponent(_headCollider, isDebug: true);
            this.CheckComponent(_bodyCollider, isDebug: true);
        }

        protected virtual void Start()
        {
            _crouchContactFilter.SetLayerMask(_groundLayer);
        }

        protected virtual void FixedUpdate()
        {
            this.GroundHandle();
            this.CrouchHandle();
            this.ClimbHandle();
            this.SpeedHandle();
        }

        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Climbable"))
            {
                CanClimb = true;
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Climbable"))
            {
                CanClimb = false;
            }
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
                this.AddForce(Vector2.up * Stats.JumpHeight);
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
            if (IsClimb) Speed = Stats.ClimbSpeed;
            else if (IsCrouch) Speed = Stats.CrouchSpeed;
            else if (IsOnAir) Speed = Stats.AirSpeed;
            else Speed = Stats.Speed;
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
                if (HeadCollider.OverlapCollider(_crouchContactFilter, _colliders) > 0)
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
            Vector2 velocity = _rigidbody2D.velocity;

            if (xClamp != null && Mathf.Abs(velocity.x) > xClamp)
            {
                velocity.x = (float)xClamp * Mathf.Sign(velocity.x);
            }

            if (yClamp != null && Mathf.Abs(velocity.y) > yClamp)
            {
                velocity.y = (float)yClamp * Mathf.Sign(velocity.y);
            }

            _rigidbody2D.velocity = velocity;
        }

        protected virtual void SetVelocity(float? x = null, float? y = null)
        {
            float xVelocity = x ?? _rigidbody2D.velocity.x;
            float yVelocity = y ?? _rigidbody2D.velocity.y;
            _rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
        }

        /// <summary>
        ///     Move character by <paramref name="direction"/> after calculating the force
        /// </summary>
        /// <param name="direction"></param>
        protected virtual void MoveByDirection(Vector2 direction)
        {
            Vector2 tranfer = direction * _rigidbody2D.mass * 100f;
            _rigidbody2D.AddForce(tranfer, ForceMode2D.Force);
        }

        protected virtual void AddForce(Vector2 force)
        {
            Vector2 finalForce = force * _rigidbody2D.mass * 6f;
            _rigidbody2D.AddForce(finalForce, ForceMode2D.Impulse);
        }
    }
}