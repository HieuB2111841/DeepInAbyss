using UnityEngine;
using Game.Entities;

namespace Game.Objects
{
    public class SpawnedEntityPool : PoolList<Entity>
    {
        private SpawnedEntityManager _manager;

        public SpawnedEntityManager Manager
        {
            get => _manager;
            set => _manager = value;
        }
    }
}
