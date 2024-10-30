using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class Character : Entity
    {
        private CharacterAnimation _animation;
        private CharacterItem _item;

        public CharacterAnimation Animation => _animation;
        public CharacterItem Item => _item;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _animation);
            this.LoadComponent(ref _item);
        }
    }
}