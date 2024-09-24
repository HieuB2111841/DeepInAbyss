using Managers.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Game.Entities
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class CharacterAnimation : MonoBehaviour
    {
        private SpriteRenderer _sprite;
        private Animator _animator;


        private int _horizontalSpeedHashCode;
        private int _verticalSpeedHashCode;
        private int _jumpHashCode;


        private void Awake()
        {
            this.LoadComponents();
        }

        private void Start()
        {
            _horizontalSpeedHashCode = Animator.StringToHash("HorizontalSpeed");
            _verticalSpeedHashCode = Animator.StringToHash("VerticalSpeed");
            _jumpHashCode = Animator.StringToHash("Jump");

        }

        protected virtual void LoadComponents()
        {
            this.LoadComponent(ref _sprite);
            this.LoadComponent(ref _animator);
        }

        public void SetHorizontalSpeed(float speed)
        {
            _animator.SetFloat(_horizontalSpeedHashCode, Mathf.Abs(speed));
            this.Flip(speed);
        }

        public void SetVerticalSpeed(float speed)
        {
            _animator.SetFloat(_verticalSpeedHashCode, speed);

        }


        private void Flip(float speed)
        {
            if (speed > 0.02f)
            {
                _sprite.flipX = false;
            }
            if (speed < -0.02)
            {
                _sprite.flipX = true;
            }
        }
    }
}