using Managers.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


namespace Game.Entities
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class EntityAnimation : EntityAbstraction
    {
        protected SpriteRenderer _sprite;
        protected Animator _animator;

        protected override void LoadComponents()
        {
            base.LoadComponents();

            this.LoadComponent(ref _sprite);
            this.LoadComponent(ref _animator);
        }

        public virtual void Flip(bool isFlip)
        {
            _sprite.flipX = isFlip;
        }

        public virtual void Flip(float axis, float sensivity = 0.02f)
        {
            if (axis > sensivity)
            {
                Flip(false);
            }
            if (axis < -sensivity)
            {
                Flip(true);
            }
        }
    }
}
