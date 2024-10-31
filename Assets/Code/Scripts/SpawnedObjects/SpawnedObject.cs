using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public abstract class SpawnedObject : MonoBehaviour
    {
        [SerializeField] private float _remainingTimeToDespawn = 10f;

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