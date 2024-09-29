using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class Character : Entity
    {
        private CharacterAnimation _animation;

        #region Properties
        public override bool IsCrouch
        {
            protected set
            {
                base.IsCrouch = value;
                _animation.SetIsCrouch(value);
            }
        }

        public override bool IsClimb
        {
            protected set
            {
                base.IsClimb = value;
                _animation.SetIsClimb(value);
            }
        }
        #endregion

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _animation);

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            _animation.SetSpeed(_rigidbody2D.velocity);
        }

        public override void HorizontalMove(float axis)
        {
            base.HorizontalMove(axis);
            _animation.Flip(axis);
        }

        public override void Climb(Vector2 axis)
        {
            base.Climb(axis);
            _animation.SetIsClimbIdle(axis.magnitude == 0);
        }
    }
}