using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class SpawnedObjectManager : MonoBehaviour
    {
        [SerializeField] private SpawnedObject _prefab;
        private SpawnedObjectPool _pool;

        [SerializeField] private float _timeToDespawn = 10f;

        private List<SpawnedObject> _prepareDespawnObjects = new();

        public SpawnedObject Prefab => _prefab;
        public SpawnedObjectPool Pool => _pool;


        protected virtual void Awake()
        {
            this.LoadComponents();
            this.Init();
        }


        public float TimeToDespawn
        {
            get => _timeToDespawn;
            set => _timeToDespawn = value;
        }

        private void FixedUpdate()
        {
            _prepareDespawnObjects.Clear();
            foreach (SpawnedObject obj in Pool.Activities)
            {
                if (obj.RemainingTime > 0f)
                {
                    obj.RemainingTime -= Time.fixedDeltaTime;
                }

                if (obj.RemainingTime <= 0f)
                {
                    _prepareDespawnObjects.Add(obj);
                }
            }

            foreach (SpawnedObject obj in _prepareDespawnObjects)
            {
                Pool.Deactivate(obj);
            }
        }


        protected virtual void Init()
        {
            if (_prefab == null) Debug.LogError("Prefab is null", this);
            Pool.Prefab = Prefab;
            Pool.Manager = this;
        }

        protected virtual void LoadComponents()
        {
            _pool = this.gameObject.AddComponent<SpawnedObjectPool>();
        }
    }
}