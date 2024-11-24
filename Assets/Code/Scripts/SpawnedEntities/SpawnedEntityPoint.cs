using UnityEngine;
using Game.Entities;
using System.Collections.Generic;

namespace Game.Objects
{
    public class SpawnedEntityPoint : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints = new ();
        [SerializeField] private float _updateRate = 0.5f;

        private void Awake()
        {
            this.GetAllSpawnPoints();
        }

        private void Start()
        {
            InvokeRepeating(nameof(UpdatePoints), 0f, _updateRate);
        }

        private void GetAllSpawnPoints()
        {
            foreach (Transform child in transform)
            {
                if(child.TryGetComponent(out SpawnPoint point))
                {
                    _spawnPoints.Add(point);
                }
                else
                {
                    SpawnPoint newPoint = child.gameObject.AddComponent<SpawnPoint>();
                    _spawnPoints.Add(newPoint);
                }
            }
        }

        private void UpdatePoints()
        {
            foreach(SpawnPoint point in _spawnPoints)
            {
                // Entity death or something
                if(!point.IsEntityAlive)
                    point.EmptyTime += _updateRate;
            }
        }

        public List<SpawnPoint> GetPointsByEmptyTime(float emptyTime, bool isChoiceNullEntity = true)
        {
            return _spawnPoints.FindAll(
                point => point.EmptyTime > emptyTime || (isChoiceNullEntity && point.Entity == null)
            );
        }
    }

}
