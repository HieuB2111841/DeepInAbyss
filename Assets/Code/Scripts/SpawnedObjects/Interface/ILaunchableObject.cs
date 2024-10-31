using System;
using UnityEngine;

namespace Game.Objects
{
    internal interface ILaunchableObject : IPhysicalObject
    {
        public void AddForce(Vector2 direction);
    }
}
