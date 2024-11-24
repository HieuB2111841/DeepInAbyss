using UnityEngine;
using Game.Entities;
using System.Collections.Generic;

namespace Game.Objects
{

    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private float _emptyTime = 0f;

        public Entity Entity
        {
            get => _entity;
            set
            {
                _entity = value;
                EmptyTime = 0f;
            }
        }

        public float EmptyTime
        {
            get => _emptyTime;
            set => _emptyTime = value;
        }

        public bool IsEmpty => EmptyTime > 0 || Entity == null;
        public bool IsEntityAlive => _entity?.gameObject.activeSelf ?? false;
    }
}
