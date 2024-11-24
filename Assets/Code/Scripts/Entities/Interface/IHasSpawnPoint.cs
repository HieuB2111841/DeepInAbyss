using Managers.Extension;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public interface  IHasSpawnPoint
    {
        public Vector2 SpawnPoint { get; set; }
    }
}
