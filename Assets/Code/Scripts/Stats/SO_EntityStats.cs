using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Stats
{
    [CreateAssetMenu(menuName = "Game/Entity Stats", fileName = "New Entity Stats")]
    public class SO_EntityStats : ScriptableObject
    {
        [Header("Physics")]
        [SerializeField] protected float _gravityScale = 3f;
        [SerializeField] protected float _jumpHeight = 2f;

        [Header("Speed")]
        [SerializeField] protected float _speed = 2f;
        [SerializeField] protected float _airSpeed = 2f;
        [SerializeField] protected float _crouchSpeed = 1f;
        [SerializeField] protected float _climbSpeed = 1f;

        [Header("Utilities")]
        [SerializeField] protected float _viewDistance = 10f;
        [SerializeField] protected float _attackRange = 5f;

        [Header("Stats")]
        [SerializeField] protected float _health = 100f;
        [SerializeField] protected float _damage = 10f;
        [SerializeField] protected float _attackSpeed = 2f;
        [SerializeField] protected float _armor = 10f;


        public float GravityScale => _gravityScale;
        public float JumpHeight => _jumpHeight;

        public float Speed => _speed;
        public float AirSpeed => _airSpeed;
        public float CrouchSpeed => _crouchSpeed;
        public float ClimbSpeed => _climbSpeed;

        public float ViewDistance => _viewDistance;
        public float AttackRange => _attackRange;

        public float Health => _health;
        public float Damage => _damage;
        public float AttackSpeed => _attackSpeed;
        public float Armor => _armor;

    }
}