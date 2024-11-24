using Game.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class Enemy : Entity
    {
        [SerializeField] protected Transform _target;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected LayerMask _obstacleLayer;
        [SerializeField, Min(0.1f)] protected float _findRate = 1f;


        public Transform Target => _target;
        public float ViewRadius => Stats?.ViewDistance ?? 10f;

        protected virtual void Start()
        {
            InvokeRepeating(nameof(FindTarget), 1f, _findRate);
        }

        protected virtual void FixedUpdate()
        {

        }

        public virtual void FindTarget()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, ViewRadius, _targetLayer);
            if(collider != null)
            {
                Vector2 dir = collider.transform.position - transform.position;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, ViewRadius, _obstacleLayer);
                if(hit.transform == collider.transform)
                {
                    _target = collider.transform;
                    return;
                }
            }
            _target = null;
        }
    }
}