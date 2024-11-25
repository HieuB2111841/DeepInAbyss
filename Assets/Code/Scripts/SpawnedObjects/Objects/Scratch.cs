using Game.Entities;
using Managers.Extension;
using Managers.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class Scratch : SpawnedObject, ISendDamageObject, ICollidableObject
    {
        private CircleCollider2D _collider;
        private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _sprite;

        [SerializeField] LayerMask _collideLayer;
        [SerializeField] float _damageScale = 1f;

        public Collider2D Collider => _collider;
        public Rigidbody2D Rigidbody2D => _rigidbody;
        public LayerMask CollideLayer => _collideLayer;

        public float DamageScale => _damageScale;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _collider);
            this.LoadComponent(ref _rigidbody);
            this.LoadComponent(ref _sprite);
        }
        
        private void FixedUpdate()
        {
            Vector2 offset = Vector2.down * 0.2f;
            if (_sprite.flipX) offset += Vector2.left * 1f;
            else offset += Vector2.right * 1f;

            transform.position = (Vector2)Owner.transform.position + offset;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            this.OnCollision(collision);
        }

        public void OnCollision(Collider2D collision)
        {
            if (collision.transform == Owner) return;
            if (CollideLayer.IsLayerInMask(collision.gameObject.layer))
            {
                if (Owner.TryGetComponent(out Entity owner))
                {
                    owner.Stats.SendDamage(collision.transform, DamageScale);
                }
            }
        }

        public void SetDirection(Vector2 dir)
        {
            _sprite.flipX = dir.x < 0;
        }
    }
}