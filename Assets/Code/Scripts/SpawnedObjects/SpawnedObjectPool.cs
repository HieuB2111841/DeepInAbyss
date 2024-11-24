using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class SpawnedObjectPool : PoolList<SpawnedObject> 
    {
        private SpawnedObjectManager _manager;

        public SpawnedObjectManager Manager
        {
            get => _manager;
            set => _manager = value;
        } 

        public override SpawnedObject Activate()
        {
            SpawnedObject activateOject = base.Activate();
            activateOject.RemainingTime = Manager.TimeToDespawn;

            return activateOject;
        }

    }
}
