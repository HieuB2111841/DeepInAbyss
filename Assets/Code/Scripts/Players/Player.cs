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

        public PlayerSetting Setting => _setting;
        public PlayerInput PlayerInput => _input;
        public PlayerCamera PlayerCamera => _camera;
        public PlayerUI UI => _ui;
        public Character Character => _character;

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _setting);
            this.LoadComponent(ref _input);
            this.LoadComponent(ref _camera);
            this.LoadComponent(ref _ui);
            this.LoadComponent(ref _character);
        }


        private void FixedUpdate()
        {
            this.ControlCharacter();
        }

        public void ControlCharacter()
        {
            Vector2 moveAxis = PlayerInput.KeyAxis;

            Character.Move(moveAxis.x);

            if(PlayerInput.GetJumpKey())
            {
                Character.Jump();
            }
        }
    }
}