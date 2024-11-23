using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public abstract class EntityMovement : EntityAbstraction
    {
        protected bool _isGrounded = false;


        [SerializeField] protected LayerMask _groundLayer;

        #region Properties

        public virtual bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }


        public bool IsOnAir
        {
            get => !IsGrounded;
        }


        #endregion


        protected virtual void Start()
        {
            
        }

        public abstract void Move(Vector2 axis);
        public virtual void Stop() { }


        protected virtual void FixedUpdate()
        {
            this.GroundHandle();
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
