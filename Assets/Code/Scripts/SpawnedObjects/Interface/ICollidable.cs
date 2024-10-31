using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Game.Objects
{
    internal interface ICollidable : IPhysicalObject
    {
        public LayerMask CollideLayer { get; }
        public void OnCollision(Collider2D collision);
    }
}
