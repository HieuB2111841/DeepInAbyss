using UnityEngine;
using Game.Entities;
using Managers.Extension;

namespace Game.Players
{
    public class Player : PlayerAbstraction
    {
        private PlayerSetting _setting;
        private PlayerInput _input;
        private PlayerCamera _camera;
        private PlayerUI _ui;
        private Character _character;


        [SerializeField] private bool _isCharacterInsideCamera;
        public PlayerSetting Setting => _setting;
        public PlayerInput PlayerInput => _input;
        public PlayerCamera PlayerCamera => _camera;
        public PlayerUI UI => _ui;
        public Character Character => _character;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _setting, isDebug: true);
            this.LoadComponent(ref _input, isDebug: true);
            this.LoadComponent(ref _camera, isDebug: true);
            this.LoadComponent(ref _ui, isDebug: true);
            this.LoadComponent(ref _character, isDebug: true);
        }

        private void Update()
        {
            this.ControlCamera();
        }

        private void FixedUpdate()
        {
            this.ControlCharacter();
        }

        private void ControlCamera()
        {
            float distance = PlayerCamera.DistanceFromCenterCamera(Character.transform.position, out Vector2 direction);
            if (distance > Setting.MaxDistanceFromCameraCenter)
            {
                PlayerCamera.TranslateCamera(direction.normalized * Setting.CameraMoveSpeed * Time.deltaTime);
            }
            if (distance > Setting.MaxDistanceFromCameraCenter * 2f)
            {
                PlayerCamera.TranslateCamera(direction.normalized * Setting.CameraMoveSpeed * 2f * Time.deltaTime);
            }
        }

        private void ControlCharacter()
        {
            Vector2 moveAxis = PlayerInput.KeyAxis;

            Character.Move(moveAxis);

            if (PlayerInput.GetJumpKey())
            {
                Character.Jump();
            }
        }
    }
}