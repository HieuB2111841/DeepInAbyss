using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public interface IWalkable
    {
        public float WalkSpeed { get; }
        public bool IsWalk { get; }
        public bool CanWalk { get; }
        public void Walk(float axis);
    }
}
