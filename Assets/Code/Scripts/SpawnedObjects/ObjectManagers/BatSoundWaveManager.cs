using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    public class BatSoundWaveManager : SpawnedObjectManager
    {
        [SerializeField] private float _startSize = 0.5f;
        [SerializeField] private float _biggestSize = 1f;
        [SerializeField] private float _force = 5f;

        public float StartSize => _startSize;
        public float BiggestSize => _biggestSize;
        public float Force => _force;
    }
}