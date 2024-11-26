using Game.Entities;
using Managers.Extension;
using Managers.Utils;
using UnityEngine;

namespace Game.Objects
{
    public class Cherry : SpawnedObject, ILaunchableObject, ICollidableObject
    {
        private CircleCollider2D _collider;
        private Rigidbody2D _rigidbody;
        [SerializeField] LayerMask _collideLayer;

        [SerializeField] private float _healAmount = 10f;
        [SerializeField] private float _healRatioAmount = 0.05f;


        public Collider2D Collider => _collider;
        public Rigidbody2D Rigidbody2D => _rigidbody;
        public LayerMask CollideLayer => _collideLayer;

        public float HealAmount
        {
            get => _healAmount;
            set=> _healAmount = value;
        }

        public float HealRatioAmount
        {
            get => _healRatioAmount;
            set => _healRatioAmount = value;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _collider);
            this.LoadComponent(ref _rigidbody);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision(collision);
        }

        public void AddForce(Vector2 direction)
        {
            Rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
        }

        public void OnCollision(Collision2D collision)
        {
            if (CollideLayer.IsLayerInMask(collision.gameObject.layer))
            {
                if (collision.transform.TryGetComponent(out Entity entity))
                {
                    float totalAmount = HealAmount + entity.Stats.Health.ValueRatio(HealRatioAmount);
                    entity.Stats.Heal(transform, "Ate Cherry", totalAmount);
                    Manager.Pool.Deactivate(this);
                }
            }
        }
    }
}