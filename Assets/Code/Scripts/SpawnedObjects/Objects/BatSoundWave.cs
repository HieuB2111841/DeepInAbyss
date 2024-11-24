using Game.Entities;
using Managers.Extension;
using Managers.Utils;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Game.Objects
{

    public class BatSoundWave : SpawnedObject, ILaunchableObject, ICollidable
    {
        private CircleCollider2D _collider;
        private Rigidbody2D _rigidbody;

        [SerializeField] LayerMask _collideLayer;
        public List<Transform> _collidedList = new();


        public new BatSoundWaveManager Manager => base.Manager as BatSoundWaveManager;
        public Collider2D Collider => _collider;
        public Rigidbody2D Rigidbody2D => _rigidbody;

        public LayerMask CollideLayer => _collideLayer;
        

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _collider);
            this.LoadComponent(ref _rigidbody);
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            Rigidbody2D.gravityScale = 0f;
            _collidedList.Clear();
        }

        private void FixedUpdate()
        {
            this.SizeHandle();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            this.OnCollision(collision);
        }

        public void AddForce(Vector2 direction)
        {
            Rigidbody2D.AddForce(direction.normalized * Manager.Force, ForceMode2D.Impulse);
            transform.right = direction;
        }

        public void OnCollision(Collider2D collision)
        {
            if (_collidedList.Contains(collision.transform)) return;
            else _collidedList.Add(collision.transform);

            if (CollideLayer.IsLayerInMask(collision.gameObject.layer))
            {
                if(Owner.TryGetComponent(out Entity owner))
                {
                    float damageScale = RemainingTime / Manager.TimeToDespawn;
                    owner.Stats.SendDamage(collision.transform, damageScale);
                }
            }
        }

        private void SizeHandle()
        {
            float t = 1 - (RemainingTime / Manager.TimeToDespawn);
            Vector3 increaseSize = Vector3.Lerp(Vector3.one * Manager.StartSize, Vector3.one * Manager.BiggestSize, t);
            transform.localScale = increaseSize;
        }
    }
}