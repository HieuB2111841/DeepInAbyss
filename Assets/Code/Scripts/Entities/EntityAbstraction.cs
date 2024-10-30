using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class EntityAbstraction : MonoBehaviour
    {
        private Entity _entity;

        public Entity Entity => _entity;


        protected virtual void Awake()
        {
            this.LoadComponents();
        }

        protected virtual void LoadComponents()
        {
            this.LoadComponent(ref _entity);
        }
    }
}
