using System;
using UnityEngine;

namespace Game.Objects
{
    internal interface IPhysicalObject
    {
        public Collider2D Collider { get; }
        public Rigidbody2D Rigidbody2D { get; }
    }
}
