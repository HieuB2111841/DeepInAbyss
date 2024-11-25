using UnityEngine;

namespace Game.Objects
{
    internal interface ICollidableObject : IPhysicalObject
    {
        public LayerMask CollideLayer { get; }
        public void OnCollision(Collider2D collision);
    }
}
