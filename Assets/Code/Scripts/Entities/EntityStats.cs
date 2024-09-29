using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers.Extension;

namespace Game.Entities
{
    public class EntityStats : MonoBehaviour
    {
        [SerializeField] protected float _gravityScale = 3f;
        [SerializeField] protected float _speed = 2f;
        [SerializeField] protected float _airSpeed = 2f;
        [SerializeField] protected float _crouchSpeed = 1f;
        [SerializeField] protected float _climbSpeed = 1f;
        [SerializeField] protected float _jumpHeight = 2f;

        public float GravityScale => _gravityScale;
        public float Speed => _speed;
        public float AirSpeed => _airSpeed;
        public float CrouchSpeed => _crouchSpeed;
        public float ClimbSpeed => _climbSpeed;
        public float JumpHeight => _jumpHeight;
    }
}
