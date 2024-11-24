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

        [SerializeField] private Collider2D _bodyCollider;

        protected EntityAnimation _animation;
        protected EntityMovement _movement;
        protected EntityStats _stats;


        #region Properties
        public Rigidbody2D Rigidbody2D => _rigidbody2D;
        public Collider2D BodyCollider => _bodyCollider;
        public EntityAnimation Animation => _animation;
        public EntityMovement Movement => _movement;
        public EntityStats Stats => _stats;

        #endregion

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _rigidbody2D);
            this.LoadComponent(ref _bodyCollider);
            this.LoadComponent(ref _animation);
            this.LoadComponent(ref _movement);
            this.LoadComponent(ref _stats);

            this.CheckComponent(_bodyCollider, isDebug: true);
        }
    }
}