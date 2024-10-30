using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class CharacterMovement : EntityMovement, ICharacterComponent
    {
        private Character _character;

        public Character Character => _character;


        #region Properties
        public override bool IsCrouch
        {
            set
            {
                base.IsCrouch = value;
                Character.Animation.SetIsCrouch(value);
            }
        }

        public override bool IsClimb
        {
            set
            {
                base.IsClimb = value;
                Character.Animation.SetIsClimb(value);
            }
        }
        #endregion

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _character);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Character.Animation.SetSpeed(Character.Rigidbody2D.velocity);
        }

        public override void HorizontalMove(float axis)
        {
            base.HorizontalMove(axis);
            Character.Animation.Flip(axis);
        }

        public override void Climb(Vector2 axis)
        {
            base.Climb(axis);
            Character.Animation.SetIsClimbIdle(axis.magnitude == 0);
        }
    }
}
