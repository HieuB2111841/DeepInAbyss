using Managers.Extension;
using UnityEngine;

namespace Game.Entities
{
    public class Character : Entity
    {
        private CharacterAnimation _animation;

        [SerializeField] private Collider2D _headCollider;
        [SerializeField] private Collider2D _bodyCollider;

        [SerializeField] private LayerMask _groundLayer;
        private bool _isGrounded = false;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _animation);

            this.CheckComponent(_headCollider, isDebug: true);
            this.CheckComponent(_bodyCollider, isDebug: true);
        }

        private void FixedUpdate()
        {
            this.GroundHandle();

            _animation.SetVerticalSpeed(_rigidbody2D.velocity.y);
            _animation.SetHorizontalSpeed(_rigidbody2D.velocity.x);
        }

        public virtual void Move(float axis)
        {
            if(_isGrounded)
            {
                float speed = axis * Stats.Speed * _rigidbody2D.mass * 100f;
                _rigidbody2D.AddForce(Vector2.right * speed, ForceMode2D.Force);
                
                this.VelocityHandle();
            }
        }

        public virtual void Jump()
        {
            if (_isGrounded)
            {
                float force = Stats.JumpHeight * _rigidbody2D.mass * 2f;
                _rigidbody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            }

        }

        private void GroundHandle()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, _groundLayer);
            if (hit != default)
            {
                _isGrounded = true;
            }
            else
            {
                _isGrounded = false;
            }
        }
    }
}