using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class SpawnedPool : PoolList<SpawnedObject> 
    {
        private SpawnedManager _manager;

        public SpawnedManager Manager
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
