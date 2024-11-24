using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class Character : Entity
    {
        [SerializeField] private Collider2D _headCollider;
        private CharacterItem _item;


        public Collider2D HeadCollider => _headCollider;
        public new CharacterAnimation Animation => base.Animation as CharacterAnimation;
        public new CharacterMovement Movement => base.Movement as CharacterMovement;
        public CharacterItem Item => _item;
        

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