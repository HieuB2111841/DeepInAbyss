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
        private int _isCrouchHashCode;
        private int _isClimbHashCode;
        private int _isClimIdlebHashCode;


        private void Awake()
        {
            this.LoadComponents();
        }

        private void Start()
        {
            _horizontalSpeedHashCode = Animator.StringToHash("HorizontalSpeed");
            _verticalSpeedHashCode = Animator.StringToHash("VerticalSpeed");
            _isCrouchHashCode = Animator.StringToHash("IsCrouch");
            _isClimbHashCode = Animator.StringToHash("IsClimb");
            _isClimIdlebHashCode = Animator.StringToHash("IsClimbIdle");

        }

        protected virtual void LoadComponents()
        {
            this.LoadComponent(ref _sprite);
            this.LoadComponent(ref _animator);
        }

        public void SetSpeed(Vector2 direction)
        {
            SetHorizontalSpeed(direction.x);
            SetVerticalSpeed(direction.y);
        }

        public void SetHorizontalSpeed(float speed)
        {
            _animator.SetFloat(_horizontalSpeedHashCode, Mathf.Abs(speed));
        }

        public void SetVerticalSpeed(float speed)
        {
            _animator.SetFloat(_verticalSpeedHashCode, speed);

        }

        public void SetIsCrouch(bool state)
        {
            _animator.SetBool(_isCrouchHashCode, state);
        }

        public void SetIsClimb(bool state)
        {
            _animator.SetBool(_isClimbHashCode, state);
            if (state == false)
            {
                this.SetIsClimbIdle(false);
            }
        }

        public void SetIsClimbIdle(bool state)
        {
            _animator.SetBool(_isClimIdlebHashCode, state);
        }

        public void Flip(float axis)
        {
            if (axis > 0.02f)
            {
                _sprite.flipX = false;
            }
            if (axis < -0.02f)
            {
                _sprite.flipX = true;
            }
        }
    }
}