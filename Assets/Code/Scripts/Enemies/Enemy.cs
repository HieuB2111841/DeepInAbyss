using Game.Objects;
using Game.Players;
using Managers.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Entities
{
    public class Enemy : Entity
    {
        [SerializeField] protected NavMeshAgent _agent;
        [SerializeField] protected SpawnedEntityManager _spawnManager;

        [Header("Target Founder")]
        [SerializeField] protected Transform _target;
        [SerializeField] protected LayerMask _targetLayer;
        [SerializeField] protected LayerMask _obstacleLayer;
        [SerializeField] protected bool _isFindTarget = true;
        [SerializeField, Min(0.1f)] protected float _findRate = 1f;

        public NavMeshAgent Agent => _agent;
        public SpawnedEntityManager SpawnManager
        {
            get => _spawnManager;
            set => _spawnManager = value;
        }

        public Transform Target
        {
            get => _target;
            protected set => _target = value;
        } 
        public bool IsFindTarget
        {
            get => _isFindTarget;
            protected set => _isFindTarget = value;
        }
        public float ViewRadius => Stats?.ViewDistance ?? 10f;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _agent);
        }

        protected virtual void Start()
        {
            InvokeRepeating(nameof(FindTarget), 1f, _findRate);
        }

        protected virtual void FixedUpdate()
        {
            Animation.Flip(Agent.velocity.x);
        }

        public virtual void FindTarget()
        {
            if (!IsFindTarget) return;

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