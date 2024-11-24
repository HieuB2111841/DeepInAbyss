using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public abstract class SpawnedObject : MonoBehaviour
    {
        [SerializeField] private SpawnedManager _manager;
        [SerializeField] private Transform _owner;
        [SerializeField] private float _remainingTimeToDespawn = 10f;


        public SpawnedManager Manager
        {
            get => _manager;
            set => _manager = value;
        }

        public Transform Owner
        {
            get => _owner;
            set => _owner = value;
        }

        public float RemainingTime
        {
            get => _remainingTimeToDespawn;
            set => _remainingTimeToDespawn = value;
        }

        public virtual void Awake()
        {
            this.LoadComponents();
        }

        protected virtual void LoadComponents()
        {

        }
    }
}