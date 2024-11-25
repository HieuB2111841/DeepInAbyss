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
        public void Attack(Vector2 direction);
    }
}
