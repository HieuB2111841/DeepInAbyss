using Game.Objects;
using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class Character : Entity, IAttackable
    {
        [SerializeField] private Collider2D _headCollider;
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
            if (CanAttack)
            {
                Scratch scratch = SpawnedObjectSystem.Instance.Spawn("Scratch", transform) as Scratch;
                if (scratch != null)
                {
                    Vector2 offset = Vector2.right * Mathf.Sign(direction.x) * 1f + Vector2.down * 0.2f;
                    scratch.transform.position = (Vector2)transform.position + offset;
                    scratch.SetDirection(direction);
                    Animation.Flip(direction.x < 0);
                }

                _attackCoolDown = AttackSpeed; // Reset attack
            }
        }
    }
}