using UnityEngine;

namespace Game.Objects
{
    internal interface ICollidableObject : IPhysicalObject
    {
        public LayerMask CollideLayer { get; }
    }
}
