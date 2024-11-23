using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public interface IJumpable
    {
        public float JumpHeight { get; }
        public float JumpCoolDown { get; }
        public bool CanJump { get; }
        public void Jump();
        public void ResetJump();
    }
}
