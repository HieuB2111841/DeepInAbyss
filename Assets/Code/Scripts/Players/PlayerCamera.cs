using UnityEngine;
using Managers.Extension;

namespace Game.Players
{
    public class PlayerCamera : PlayerAbstraction
    {
        public static readonly Vector2 centerCamera = Vector2.one * 0.5f;
        public static readonly Vector3 cameraZ = Vector3.back * 10f;
        

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

        public void MoveSmoothCamera(Vector2 position, float deltaTime = -1f)
        {
            if(deltaTime == -1f) deltaTime = Time.deltaTime;

            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, (Vector3)position + cameraZ, deltaTime);
        }

        public void MoveCameratoTo(Vector2 position)
        {
            MainCamera.transform.position = (Vector3)position + cameraZ;
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