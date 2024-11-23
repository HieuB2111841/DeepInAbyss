using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class BatEnemy : Enemy
    {
        public new BatMovement Movement
        {
            get => (BatMovement)base.Movement;
        }


        protected virtual void FixedUpdate()
        {
            if (Target != null)
            {
                Vector2 dir = Target.transform.position - transform.position;
                Movement.Fly(dir);
            }
            else
            {
                Movement.Stop();
            }
        }

    }
}