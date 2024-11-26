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

        private Dictionary<string, SpawnedObjectManager> _managers = new();

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else Debug.LogError($"{nameof(SpawnedObjectSystem)} is singleton", this);

            this.LoadSpawnedObjects();
        }

        public SpawnedObjectManager GetManager(string name)
        {
            if (TryGetManager(name, out SpawnedObjectManager manager))
            {
                return manager;
            }
            return null;
        }

        public bool TryGetManager(string name, out SpawnedObjectManager manager)
        {
            return _managers.TryGetValue(name, out manager);
        }

        public bool AddManager(SpawnedObjectManager manager)
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


        public SpawnedObject Spawn(string name, Transform owner, Vector2 position = default, Quaternion rotation = default)
        {
            if(TryGetManager(name, out SpawnedObjectManager manager))
            {
                SpawnedObject getObject = manager.Pool.Activate();
                getObject.Manager = manager;
                getObject.Owner = owner;
                getObject.transform.SetPositionAndRotation(position, rotation);

                return getObject;
            }
            return null;
        }

        public T Spawn<T>(string name, Transform owner, Vector2 position = default, Quaternion rotation = default) where T : SpawnedObject
        {
            return this.Spawn(name, owner, position, rotation) as T;
        }

        public bool Despawn(SpawnedObject targetObject, string name)
        {
            if (TryGetManager(name, out SpawnedObjectManager manager))
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

            foreach (SpawnedObjectManager spawnedManager in _spawnedList.Managers)
            {
                SpawnedObjectManager manager = Instantiate(spawnedManager, this.transform);
                manager.name = spawnedManager.name;
                this.AddManager(manager);
            }
        }
    }
}