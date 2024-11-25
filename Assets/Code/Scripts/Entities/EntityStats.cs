using UnityEngine;
using Managers.Extension;
using Game.Entities.Stats;
using static UnityEngine.EventSystems.EventTrigger;
using System;
using Game.Objects;

namespace Game.Entities
{
    public class EntityStats : MonoBehaviour
    {
        protected Entity _owner;
        [SerializeField] protected SO_EntityStats _baseStats;

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
        [SerializeField] protected SlideStat _health = new();
        [SerializeField] protected Stat _shield = new();
        
        [SerializeField] protected Stat _damage = new();
        [SerializeField] protected Stat _attackSpeed = new();
        [SerializeField] protected Stat _armor = new();


        public event Action<Agent> BeforeTakeDamage;
        public event Action<Agent> AfterTakeDamage;


        #region Properties
        public Entity Owner => _owner;

        public float GravityScale => _gravityScale;
        public float JumpHeight => _jumpHeight;

        public float Speed => _speed;
        public float AirSpeed => _airSpeed;
        public float CrouchSpeed => _crouchSpeed;
        public float ClimbSpeed => _climbSpeed;

        public float ViewDistance => _viewDistance;
        public float AttackRange => _attackRange;

        public SlideStat Health => _health;
        public Stat Shield => _shield;
        public Stat Damage => _damage;
        public Stat AttackSpeed => _attackSpeed;
        public Stat Armor => _armor;
        #endregion

        private void Awake()
        {
            this.LoadComponent(ref _owner);
        }

        private void Start()
        {
            this.LoadBaseStats();
        }

        protected void LoadBaseStats()
        {
            if (_baseStats == null) return;
            string reason = "base stats";

            _gravityScale = _baseStats.GravityScale;
            _jumpHeight = _baseStats.JumpHeight;

            _speed = _baseStats.Speed;
            _airSpeed = _baseStats.AirSpeed;
            _crouchSpeed = _baseStats.CrouchSpeed;
            _climbSpeed = _baseStats.ClimbSpeed;

            _viewDistance = _baseStats.ViewDistance;
            _attackRange = _baseStats.AttackRange;

            Health.Add(Owner.transform, reason, _baseStats.Health);
            Damage.Add(Owner.transform, reason, _baseStats.Damage);
            AttackSpeed.Add(Owner.transform, reason, _baseStats.AttackSpeed);
            Armor.Add(Owner.transform, reason, _baseStats.Armor);
        }

        public void SendDamage(Entity victim, float damageScale = 1f)
        {
            Agent sender = new Agent(Owner.transform, "send damage", Damage.Value * damageScale);
            victim.Stats.TakeDamage(sender);
        }

        public void SendDamage(Transform victim, float damageScale = 1f)
        {
            if (victim.TryGetComponent(out Entity entity))
            {
                SendDamage(entity, damageScale);
            }
        }

        public void TakeDamage(Agent sender)
        {
            BeforeTakeDamage?.Invoke(sender);

            float damage = sender.Value - (10f / (10f + Armor.Value));
            float finalDamage = damage - Shield.Value;

            // Damage after reduct by shield
            if(finalDamage >= 0)
            {
                Agent shieldBreaker = sender.CopyWith(reason: "shield break", value: -Shield.Value, duration: 0f);

                // Set shield value to 0
                Shield.Add(shieldBreaker);
                sender.Set(value: -finalDamage, duration: 0f);
            }
            else
            {
                Agent shieldBreaker = sender.CopyWith(reason: "shield break", value: -damage, duration: 0f);

                // Reduct shield value by damage amount
                Shield.Add(shieldBreaker);
                sender.Set(value: 0f, duration: 0f);
            }

            TextPopup textPopup = SpawnedObjectSystem.Instance.Spawn("TextPopup", Owner.transform, Owner.transform.position) as TextPopup;
            if(textPopup != null)
            {
                textPopup.SetUp((sender.Value).ToString("###.#"));
            }

            // Reduct remaining health
            Health.AddToRemaining(sender);
            AfterTakeDamage?.Invoke(sender);
        }
    }
}
