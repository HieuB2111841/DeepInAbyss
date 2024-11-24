using Managers.Extension;
using Managers.Utils;
using System;
using UnityEngine;

namespace Game.Objects
{
    internal class FireBall : SpawnedObject, ILaunchableObject, ICollidable
    {
        private CircleCollider2D _collider;
        private Rigidbody2D _rigidbody;
        [SerializeField] LayerMask _collideLayer;

        public Collider2D Collider => _collider;
        public Rigidbody2D Rigidbody2D => _rigidbody;
        public LayerMask CollideLayer => _collideLayer;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _collider);
            this.LoadComponent(ref _rigidbody);
        }

        private void FixedUpdate()
        {
            if (Rigidbody2D.velocity.magnitude > 0)
            {
                transform.up = Rigidbody2D.velocity;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            this.OnCollision(collision);
        }

        public void AddForce(Vector2 direction)
        {
            Rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
        }

        public void OnCollision(Collider2D collision)
        {
            if(CollideLayer.IsLayerInMask(collision.gameObject.layer))
            {
                SpawnedObjectSystem.Instance.Spawn("FireBallExplosion", this.Owner, this.transform.position);
                SpawnedObjectSystem.Instance.Despawn(this, "FireBall");
            }
        }
    }
}
