using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.Extension;

namespace Game.Entities
{
    public class EntityStats : MonoBehaviour
    {
        protected float _speed = 2f;
        protected float _jumpHeight = 2f;

        public float Speed => _speed;
        public float JumpHeight => _jumpHeight;
    }
}
