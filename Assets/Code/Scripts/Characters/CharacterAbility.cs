using Managers.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class CharacterAbility : MonoBehaviour, ICharacterComponent
    {
        private Character _character;

        public Character Character => _character;


        protected virtual void Awake()
        {
            this.LoadComponents();
        }

        protected virtual void LoadComponents()
        {
            this.LoadComponent(ref _character);
        }
    }
}