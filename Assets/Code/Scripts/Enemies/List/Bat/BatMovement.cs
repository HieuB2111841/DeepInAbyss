using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class BatMovement : EntityMovement, IFlyable
    {
        public float FlySpeed => Entity.Stats?.AirSpeed ?? 0f;

        public bool IsFly => IsOnAir;

        public bool CanFly => true;

        protected override void Start()
        {
            Entity.Rigidbody2D.gravityScale = 0f;
        }


        public void Fly(Vector2 axis)
        {
            Vector2 speedVector = axis.normalized * FlySpeed;
            AddForce(speedVector);
            ClampVelocity(Mathf.Abs(speedVector.x), Mathf.Abs(speedVector.y));
        }

        public override void Move(Vector2 axis)
        {

        }

        public override void Stop()
        {
            base.Stop();
            Entity.Rigidbody2D.velocity = Vector2.zero;
        }
    }
}
