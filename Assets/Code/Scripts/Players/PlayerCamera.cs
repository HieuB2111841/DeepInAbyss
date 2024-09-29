using UnityEngine;
using Managers.Extension;

namespace Game.Players
{
    public class PlayerCamera : PlayerAbstraction
    {
        public static readonly Vector2 centerCamera = Vector2.one * 0.5f;


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

        public void TranslateCamera(Vector2 direction)
        {
            MainCamera.transform.Translate(direction);
        }


        /// <returns>
        ///     Returns true if <paramref name="position"/> inside viewport
        /// </returns>
        public bool CheckPointInSideCamera(Vector2 position)
        {
            Vector2 viewportPos = MainCamera.WorldToViewportPoint(position);

            if (viewportPos.x < 0 || viewportPos.x > 1) return false;
            if (viewportPos.y < 0 || viewportPos.y > 1) return false;

            return true;
        }


        /// <param name="direction">
        ///     Direction from center of camera to <paramref name="position"/> 
        /// </param>
        /// <returns>
        ///     Returns distance from <paramref name="position"/> to  center of camera
        /// </returns>
        public float DistanceFromCenterCamera(Vector2 position, out Vector2 direction)
        {
            Vector2 viewportPos = MainCamera.WorldToViewportPoint(position);

            direction = viewportPos - centerCamera;
            return direction.magnitude;
        }

        /// <returns>
        ///     Returns distance from <paramref name="position"/> to  center of camera
        /// </returns>
        public float DistanceFromCenterCamera(Vector2 position) => DistanceFromCenterCamera(position, out _);


    }
}