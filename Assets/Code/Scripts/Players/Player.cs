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

        private void Start()
        {
            this.InitUI();
        }

        private void Update()
        {
            this.ControlCamera();
        }

        private void FixedUpdate()
        {
            this.ControlCharacter();
            this.HandleUI();
        }

        private void ControlCamera()
        {
            float distance = PlayerCamera.DistanceFromCenterCamera(Character.transform.position, out Vector2 direction);
            if (distance > Setting.MaxDistanceFromCameraCenter)
            {
                PlayerCamera.MoveSmoothCamera(Character.transform.position, Setting.CameraMoveSpeed * Time.deltaTime);
            }
            if (distance > Setting.MaxDistanceFromCameraCenter * 10f)
            {
                PlayerCamera.MoveCameratoTo(Character.transform.position);
            }
        }

        private void ControlCharacter()
        {
            Vector2 moveAxis = PlayerInput.KeyAxis;
            Character.Movement.Move(moveAxis);

            if (PlayerInput.GetJumpKey())
            {
                Character.Movement.Jump();
            }
        }

        private void InitUI()
        {
            Character.Stats.Health.OnRemainingValueChanged += (value) => UI.SetHealth(remaining: value);
            Character.Stats.Health.OnValueChanged += (value) => UI.SetHealth(max: value);
            Character.Stats.Damage.OnValueChanged += (value) => UI.UIDamage = value;
            Character.Stats.Armor.OnValueChanged += (value) => UI.UIArmor = value;
            // Character.Stats.Speed.OnValueChanged += (value) => UI.UISpeed = value;
        }

        private void HandleUI()
        {

        }
    }
}