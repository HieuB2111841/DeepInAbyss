using Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class SpawnedObjectSystem : MonoBehaviour, ISingleton<SpawnedObjectSystem>
    {
        private static SpawnedObjectSystem _instance;
        public static SpawnedObjectSystem Instance => _instance;

        [SerializeField] private SO_SpawnedManagerList _spawnedList;

        private Dictionary<string, SpawnedManager> _managers = new();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else Debug.LogError($"{nameof(SpawnedObjectSystem)} is singleton", this);

            this.LoadSpawnedObjects();
        }

        public SpawnedManager GetManager(string name)
        {
            if (TryGetManager(name, out SpawnedManager manager))
            {
                return manager;
            }
            return null;
        }

        public bool TryGetManager(string name, out SpawnedManager manager)
        {
            return _managers.TryGetValue(name, out manager);
        }

        public bool AddManager(SpawnedManager manager)
        {
            if(ContainsManager(manager.name))
                return false;

            _managers.Add(manager.name, manager);
            return true;
        }

        public bool ContainsManager(string name)
        {
            return _managers.ContainsKey(name);
        }


        public SpawnedObject Spawn(string name, Vector2 position = default, Quaternion rotation = default)
        {
            if(TryGetManager(name, out SpawnedManager manager))
            {
                SpawnedObject getObject = manager.Pool.Activate();
                getObject.transform.SetPositionAndRotation(position, rotation);

                return getObject;
            }
            return null;
        }

        public bool Despawn(SpawnedObject targetObject, string name)
        {
            if (TryGetManager(name, out SpawnedManager manager))
            {
                bool isDespawnComplete = manager.Pool.Deactivate(targetObject);
                return isDespawnComplete;
            }
            return false;
        }


        private void LoadSpawnedObjects()
        {
            if (_spawnedList == null) return;

            _managers.Clear();

            foreach (SpawnedManager spawnedManager in _spawnedList.Managers)
            {
                SpawnedManager manager = Instantiate(spawnedManager, this.transform);
                manager.name = spawnedManager.name;
                this.AddManager(manager);
            }
        }
    }
}