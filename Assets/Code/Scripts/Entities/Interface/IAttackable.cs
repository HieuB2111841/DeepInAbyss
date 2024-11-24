using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public interface IAttackable
    {
        public float AttackSpeed { get; }
        public float AttackRange { get; }
        public bool IsAttack { get; }
        public bool CanAttack { get; }
        public void Attack(Entity victim);
        public void Attack(Transform victim) => Attack(victim.GetComponent<Entity>());
    }
}
