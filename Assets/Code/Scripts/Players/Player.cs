using UnityEngine;
using Game.Entities;
using Managers;
using Managers.Extension;
using Game.Objects;

namespace Game.Players
{
    public class Player : PlayerAbstraction
    {
        private static Player _instance;
        public static Player Instacne => _instance;

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


        protected override void Awake()
        {
            if (_instance == null) _instance = this;
            else Debug.LogError("Player is singleton");
            base.Awake();
        }

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
            this.ControlCharacter();
            this.TestStats();
        }

        private void FixedUpdate()
        {
            this.FixedControlCharacter();
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
            if (PlayerInput.GetJumpKey())
            {
                Character.Movement.Jump();
            }

            if (PlayerInput.GetMouseButtonDown(MouseButton.Left))
            {
                Vector2 dir = PlayerInput.MousePosition - (Vector2)Character.transform.position;
                Character.Attack(dir);
            }
        }

        private void FixedControlCharacter()
        {
            Vector2 moveAxis = PlayerInput.KeyAxis;
            Character.Movement.Move(moveAxis);
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

        private void TestStats()
        {

            if (PlayerInput.GetKeyDown(KeyCode.Q))
            {
                Character.Stats.SendDamage(Character, 1f);
            }

            if (PlayerInput.GetKeyDown(KeyCode.R))
            {
                Character.Stats.Health.Add(transform, "buff hp", 17f);
            }

            if(PlayerInput.GetKeyDown(KeyCode.Alpha1))
            {
                Character.Stats.Damage.Add(transform, "buff damage", 8f);
            }

            if (PlayerInput.GetKeyDown(KeyCode.Alpha2))
            {
                Character.Stats.Armor.Add(transform, "buff armor", 10f);
            }
        }
    }
}