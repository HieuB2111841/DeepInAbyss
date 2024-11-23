using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public interface IFlyable
    {
        public float FlySpeed { get; }
        public bool IsFly { get; }
        public bool CanFly { get; }
        public void Fly(Vector2 axis);
    }
}
