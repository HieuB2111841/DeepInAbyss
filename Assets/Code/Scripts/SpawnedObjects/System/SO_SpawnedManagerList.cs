using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Objects
{
    [CreateAssetMenu(menuName = "Managers/Spawned Manager List", fileName = "New Spawned Manager List")]
    public class SO_SpawnedManagerList : ScriptableObject
    {
        [SerializeField] private List<SpawnedObjectManager> _managers = new();

        public List<SpawnedObjectManager> Managers => _managers;
    }
}