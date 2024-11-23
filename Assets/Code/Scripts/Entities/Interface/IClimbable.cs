using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public interface IClimbable
    {
        public bool IsClimb { get; }
        public bool CanClimb { get; }
        public void Climb(Vector2 axis);
    }
}
