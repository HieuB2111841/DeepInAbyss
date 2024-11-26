using Game.Entities;
using Managers.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Objects
{
    public class SpawnedEntityManager : MonoBehaviour
    {
        [SerializeField] private Entity _prefab;
        [SerializeField] private float _timeToSpawn = 60f;
        private SpawnedEntityPool _pool;
        private SpawnedEntityPoint _point;

        public Entity Prefab => _prefab;
        public SpawnedEntityPool Pool => _pool;
        public SpawnedEntityPoint Point => _point;

        protected virtual void Awake()
        {
            this.LoadComponents();
            this.Init();
        }

        private void FixedUpdate()
        {
            List<SpawnPoint> spawnPoints = Point.GetPointsByEmptyTime(_timeToSpawn);
            foreach(SpawnPoint point in spawnPoints)
            {
                Entity getEntity = Spawn(point.transform.position);
                point.Entity = getEntity;
            }
        }

        protected virtual void Init()
        {
            if (_prefab == null) Debug.LogError("Prefab is null", this);
            Pool.Prefab = Prefab;
            Pool.Manager = this;
        }

        public virtual Entity Spawn(Vector2 position)
        {
            Entity getEntity = Pool.Activate();
            if(getEntity is Enemy enemy)
            {
                enemy.SpawnManager = this;
                enemy.Stats.ResetStats();
                enemy.Agent.Warp(position);
            }

            else getEntity.transform.position = position;

            if (getEntity is IHasSpawnPoint hasSpawnPoint)
            {
                hasSpawnPoint.SpawnPoint = position;
            }

            return getEntity;
        }

        public virtual bool Despawn(Entity entity)
        {
            return Pool.Deactivate(entity);
        }

        protected virtual void LoadComponents()
        {
            this.LoadComponent(ref _point);
            _pool = this.gameObject.AddComponent<SpawnedEntityPool>();
        }
    }
}
