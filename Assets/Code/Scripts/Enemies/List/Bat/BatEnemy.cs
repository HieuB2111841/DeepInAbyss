using Game.Objects;
using Game.Players;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class BatEnemy : Enemy, IAttackable
    {

        private float _attackCoolDown = 0f;

        protected new BatAnimation Animation => base.Animation as BatAnimation;
        public new BatMovement Movement => base.Movement as BatMovement;


        public float AttackSpeed => Stats?.AttackSpeed.Value ?? float.PositiveInfinity;

        public bool IsAttack => false;

        public bool CanAttack => _attackCoolDown <= 0f;

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            this.AttackHandle();
            this.Behaviour();
        }

        protected virtual void Behaviour()
        {
            if (Target != null)
            {
                Vector2 dir = Target.transform.position - transform.position;

                if (dir.magnitude > 4f)
                {
                    Movement.Fly(dir);
                }
                else
                {
                    Movement.Stop();
                    (this as IAttackable).Attack(Target);
                }
            }
            else
            {
                Movement.Stop();
            }
        }

        protected virtual void AttackHandle()
        {
            if(_attackCoolDown > 0f )
            {
                _attackCoolDown -= Time.fixedDeltaTime;
            }
        }

        public void Attack(Entity victim)
        {
            if (CanAttack)
            {
                BatSoundWave soundWave = SpawnedObjectSystem.Instance.Spawn("BatSoundWave", transform) as BatSoundWave;
                if (soundWave != null)
                {
                    Vector2 dir = victim.transform.position - transform.position;
                    soundWave.transform.position = (Vector2)transform.position + dir.normalized;
                    soundWave.AddForce(dir);
                }

                _attackCoolDown = AttackSpeed; // Reset attack
            }
        }
    }
}