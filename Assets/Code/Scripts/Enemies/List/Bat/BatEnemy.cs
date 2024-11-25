using Game.Objects;
using UnityEngine;

namespace Game.Entities
{
    public class BatEnemy : Enemy, IHasSpawnPoint, IAttackable
    {
        [SerializeField] private Vector2 _spawnPoint = Vector2.zero;
        [SerializeField] private float _attackCoolDown = 0f;
        [SerializeField] private float _patience = 5f;

        protected new BatAnimation Animation => base.Animation as BatAnimation;
        public new BatMovement Movement => base.Movement as BatMovement;

        public float AttackSpeed => Stats?.AttackSpeed.Value ?? float.PositiveInfinity;
        public float AttackRange => Stats?.AttackRange ?? 0f;
        public bool IsAttack => false;

        public bool CanAttack => _attackCoolDown <= 0f;



        public Vector2 SpawnPoint 
        { 
            get => _spawnPoint; 
            set => _spawnPoint = value; 
        }


        protected override void Start()
        {
            base.Start();
            Agent.stoppingDistance = AttackRange * 4/5;
            Agent.speed = Stats.AirSpeed;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            this.AttackHandle();
            this.Behavior();
        }

        protected virtual void Behavior()
        {
            Vector2 distanceFromSpawn = (Vector2)transform.position - _spawnPoint;

            // Too far spawn point
            if (distanceFromSpawn.magnitude > ViewRadius * 2.5f)
            {
                Agent.SetDestination(SpawnPoint);
                Agent.stoppingDistance = 0.1f;
                _patience = 0;
                Target = null;
                IsFindTarget = false;
                return;
            }

            // Inside spawn point
            if (distanceFromSpawn.magnitude < ViewRadius / 5f)
            {
                IsFindTarget = true;
            }

            if (Target != null)
            {
                _patience = 5f;
                Agent.stoppingDistance = AttackRange * 4 / 5;
                Agent.SetDestination(Target.transform.position);

                Vector2 dir = Target.position - transform.position;
                if (dir.magnitude <= AttackRange)
                {
                    (this as IAttackable).Attack(dir);
                }
            }
            else
            {
                _patience -= Time.fixedDeltaTime;
                if(_patience < 0)
                {
                    Agent.SetDestination(SpawnPoint);
                    Agent.stoppingDistance = 0.1f;
                    _patience = 0;
                }
            }
        }

        protected virtual void AttackHandle()
        {
            if(_attackCoolDown > 0f )
            {
                _attackCoolDown -= Time.fixedDeltaTime;
            }
        }

        public void Attack(Vector2 direction)
        {
            if (CanAttack)
            {
                BatSoundWave soundWave = SpawnedObjectSystem.Instance.Spawn("BatSoundWave", transform) as BatSoundWave;
                if (soundWave != null)
                {
                    soundWave.transform.position = (Vector2)transform.position + direction.normalized * 0.2f;
                    soundWave.AddForce(direction);
                }

                _attackCoolDown = AttackSpeed; // Reset attack
            }
        }
    }
}