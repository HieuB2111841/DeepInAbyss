using UnityEngine;
using Managers.Extension;

namespace Game.Players
{
    public class PlayerCamera : PlayerAbstraction
    {
        private Camera _mainCamera;

        public Camera MainCamera => _mainCamera;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            if(!this.LoadComponent(ref _mainCamera))
            {
                _mainCamera = Camera.main;
            }
        }
    }
}