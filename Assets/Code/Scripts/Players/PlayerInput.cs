using Managers;
using UnityEngine;

namespace Game.Players
{

    public class PlayerInput : PlayerAbstraction
    {
        private Camera _camera;

        [SerializeField] private Vector2 _mousePosition;
        [SerializeField] private Vector2 _mouseScreenPosition;
        [SerializeField] private Vector2 _mouseViewPortPosition;

        [SerializeField] private Vector2 _mouseAxis;

        [SerializeField] private Vector2 _keyAxis;


        public Vector2 MousePosition => _mousePosition;
        public Vector2 MouseScreenPosition => _mouseScreenPosition;
        public Vector2 MouseViewPortPosition => _mouseViewPortPosition;
        public Vector2 MouseAxis => _mouseAxis;
        public Vector2 KeyAxis => _keyAxis;

        private void Start()
        {
            _camera = Player.PlayerCamera.MainCamera ?? Camera.main;
        }

        private void Update()
        {
            this.MouseUpdate();
            this.KeyUpdate();
        }

        private void MouseUpdate()
        {
            _mouseScreenPosition = Input.mousePosition;
            _mousePosition = _camera.ScreenToWorldPoint(_mouseScreenPosition);
            _mouseViewPortPosition = _camera.ScreenToViewportPoint(_mouseScreenPosition);

            _mouseAxis = new Vector2(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y"));
        }

        private void KeyUpdate()
        {
            _keyAxis = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical"));
        }

        public bool GetKey(KeyCode key) => Input.GetKey(key);
        public bool GetKeyDown(KeyCode key) => Input.GetKeyDown(key);
        public bool GetKeyUp(KeyCode key) => Input.GetKeyUp(key);


        public bool GetMouseButton(MouseButton button)
        {
            if (button == MouseButton.None) return false;
            return Input.GetMouseButton((int) button);
        }

        public bool GetMouseButtonDown(MouseButton button)
        {
            if (button == MouseButton.None) return false;
            return Input.GetMouseButtonDown((int)button);
        }

        public bool GetMouseButtonUp(MouseButton button)
        {
            if (button == MouseButton.None) return false;
            return Input.GetMouseButtonUp((int)button);
        }

        public bool GetJumpKey()
        {
            return Input.GetAxisRaw("Jump") != 0f;
        }
    }
    
}