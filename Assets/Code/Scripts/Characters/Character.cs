using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class Character : Entity
    {
        [SerializeField] private Collider2D _headCollider;
        private CharacterAnimation _animation;
        private CharacterItem _item;


        public Collider2D HeadCollider => _headCollider;
        public CharacterAnimation Animation => _animation;
        public CharacterItem Item => _item;

        public new CharacterMovement Movement
        {
            get => (CharacterMovement)base.Movement;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _animation);
            this.LoadComponent(ref _item);

            this.CheckComponent(_headCollider, isDebug: true);
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
    }
}