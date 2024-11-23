using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public interface ICrouchable
    {
        public bool IsCrouch { get; }
        public bool CanUncrouch { get; }
        public bool CanCrouch { get; }
        public void Crouch(bool isCrouch);
    }
}
