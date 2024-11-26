using Game.Objects;
using UnityEngine;

namespace Game.Entities
{
    public class BatEnemy : Enemy, IHasSpawnPoint, IAttackable
    {
        [SerializeField] private Vector2 _spawnPoint = Vector2.zero;
        [SerializeField] private float _attackCoolDown = 0f;
        [SerializeField] private float _patience = 5f;

        private bool _isGoBack = false;
        private float _healCooldown = 0.15f;

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
            Stats.OnDeath += Stats_OnDeath;
        }

        private void Stats_OnDeath(Stats.Agent obj)
        {
            EntityDeath deathVFX = SpawnedObjectSystem.Instance.Spawn<EntityDeath>("EntityDeath", transform, transform.position);
            if(deathVFX != null)
            {
                deathVFX.Init(transform.localScale.x * 2f);
            }

            Cherry cherry = SpawnedObjectSystem.Instance.Spawn<Cherry>("Cherry", transform, transform.position);
            if (cherry != null)
            {
                float randomX = Random.Range(-1f, 1f);
                float randomY = Random.Range(-1f, 1f);
                cherry.AddForce(new Vector2(randomX, randomY).normalized);
            }

            SpawnManager.Despawn(this);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            this.AttackHandle();
            this.GoBackHandle();
            this.Behavior();
        }

        public float distanceFromSpawn;
        protected virtual void Behavior()
        {
            distanceFromSpawn = ((Vector2)transform.position - _spawnPoint).magnitude;

            // Too far spawn point
            if (distanceFromSpawn > ViewRadius * 2.5f)
            {
                _isGoBack = true;
                return;
            }

            // Inside spawn point
            if (distanceFromSpawn < ViewRadius / 5f)
            {
                _isGoBack = false;
            }

            if (Target != null)
            {
                Agent.SetDestination(Target.transform.position);
                _patience = 5f;
                Agent.stoppingDistance = AttackRange * 4 / 5;

                Vector2 dir = Target.position - transform.position;
                if (dir.magnitude <= AttackRange)
                {
                    (this as IAttackable).Attack(dir);
                }
            }
            else
            {
                if (distanceFromSpawn > ViewRadius / 5f)
                {
                    _patience -= Time.fixedDeltaTime;
                    if (_patience < 0)
                    {
                        _isGoBack = true;
                    }
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

        protected virtual void GoBackHandle()
        {
            if(_isGoBack )
            {
                Agent.SetDestination(SpawnPoint);
                Agent.stoppingDistance = 0.1f;
                _patience = 0;
                Target = null;
                IsFindTarget = false;

                if(!Stats.Health.IsFull)
                {
                    _healCooldown -= Time.fixedDeltaTime;
                    if (_healCooldown <= 0f)
                    {
                        Stats.Heal(transform, "Go back spawn point", Stats.Health.Value / 10f);
                        _healCooldown =  0.15f;
                    }
                }
            }
            else
            {
                _healCooldown = 0.15f;
                IsFindTarget = true;
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
                    Animation.Flip(direction.x);
                }

                _attackCoolDown = AttackSpeed; // Reset attack
            }
        }
    }
}