using UnityEngine;

namespace Game.Objects
{
    public class EntityDeath : SpawnedObject, IVirtualEffectObject
    {
        private void OnEnable()
        {
            transform.localScale = Vector3.one;    
        }

        public void Init(float size)
        {
            transform.localScale = Vector3.one * size;
        }
    }
}