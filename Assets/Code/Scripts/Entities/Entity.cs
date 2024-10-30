using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.Extension;

namespace Game.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Entity : EntityAbstraction
    {
        protected Rigidbody2D _rigidbody2D;

        [SerializeField] private Collider2D _headCollider;
        [SerializeField] private Collider2D _bodyCollider;

        protected EntityMovement _movement;
        protected EntityStats _stats;


        #region Properties
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public Collider2D HeadCollider => _headCollider;
        public Collider2D BodyCollider => _bodyCollider;
        public EntityMovement Movement => _movement;
        public EntityStats Stats => _stats;

        #endregion

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _rigidbody2D);
            this.LoadComponent(ref _movement);
            this.LoadComponent(ref _stats);

            this.CheckComponent(_headCollider, isDebug: true);
            this.CheckComponent(_bodyCollider, isDebug: true);
        }

        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Climbable"))
            {
                Movement.CanClimb = true;
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Climbable"))
            {
                Movement.CanClimb = false;
            }
        }

    }
}