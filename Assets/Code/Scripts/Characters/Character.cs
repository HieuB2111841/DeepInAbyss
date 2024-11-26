using Game.Objects;
using Managers.Extension;
using System.Collections;
using UnityEngine;

namespace Game.Entities
{
    public class Character : Entity, IAttackable
    {
        [SerializeField] private Collider2D _headCollider;
        [SerializeField] private Transform _spawnPoint;
        private CharacterAbility _ability;

        [SerializeField] private float _attackCoolDown = 0f;


        public Collider2D HeadCollider => _headCollider;
        public new CharacterAnimation Animation => base.Animation as CharacterAnimation;
        public new CharacterMovement Movement => base.Movement as CharacterMovement;
        public CharacterAbility Ability => _ability;

        public float AttackSpeed => Stats?.AttackSpeed.Value ?? float.PositiveInfinity;

        public float AttackRange => Stats?.AttackRange ?? 0f;

        public bool IsAttack => false;

        public bool CanAttack => _attackCoolDown <= 0f;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _animation);
            this.LoadComponent(ref _ability);

            this.CheckComponent(_headCollider, isDebug: true);
        }

        protected virtual void Start()
        {
            Stats.OnDeath += Stats_OnDeath;
        }

        private void Stats_OnDeath(Stats.Agent obj)
        {
            StartCoroutine(Despawn());
        }

        protected virtual void FixedUpdate()
        {
            this.AttackHandle();
        }

        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Climbable"))
            {
                Movement.CanClimb = true;
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Climbable"))
            {
                Movement.CanClimb = false;
            }
        }

        protected virtual void AttackHandle()
        {
            if (_attackCoolDown > 0f)
            {
                _attackCoolDown -= Time.fixedDeltaTime;
            }
        }

        public void Attack(Vector2 direction)
        {
            if (Movement.IsLockMovement) return;
            if (CanAttack)
            {
                Scratch scratch = SpawnedObjectSystem.Instance.Spawn("Scratch", transform) as Scratch;
                if (scratch != null)
                {
                    scratch.SetDirection(direction);
                    Animation.Flip(direction.x < 0);
                }

                _attackCoolDown = AttackSpeed; // Reset attack
            }
        }

        public virtual void Spawn()
        {
            Stats.ResetStats();
            Movement.IsLockMovement = false;
            if(_spawnPoint != null)
            {
                transform.position = _spawnPoint.position;
            }
            Rigidbody2D.gravityScale = Stats.GravityScale;
            gameObject.SetActive(true);
        }

        protected virtual IEnumerator Despawn()
        {
            Movement.IsLockMovement = true;
            Animation.Death();
            Rigidbody2D.gravityScale = 0f;

            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}