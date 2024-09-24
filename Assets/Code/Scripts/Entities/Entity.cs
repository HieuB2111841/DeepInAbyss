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

        public EntityStats Stats => _stats;

        protected virtual void Awake()
        {
            this.LoadComponents();
        }

        protected virtual void LoadComponents()
        {
            this.LoadComponent(ref _rigidbody2D);
            this.LoadComponent(ref _stats);
        }


        protected virtual void VelocityHandle()
        {
            Vector2 horizontalVelocity = _rigidbody2D.velocity * Vector2.right;
            if(horizontalVelocity.magnitude > Stats.Speed)
            {
                horizontalVelocity = horizontalVelocity.normalized * Stats.Speed;
            }

            _rigidbody2D.velocity = new Vector2(horizontalVelocity.x, _rigidbody2D.velocity.y);
        }
    }
}