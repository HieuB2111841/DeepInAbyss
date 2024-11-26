using UnityEngine;


namespace Game.Entities
{
    public class CharacterAnimation : EntityAnimation, ICharacterComponent
    {
        public Character Character => Entity as Character;

        private int _horizontalSpeedHashCode;
        private int _verticalSpeedHashCode;
        private int _isCrouchHashCode;
        private int _isClimbHashCode;
        private int _isClimIdlebHashCode;
        private int _deathTriggerHashCode;


        private void Start()
        {
            _horizontalSpeedHashCode = Animator.StringToHash("HorizontalSpeed");
            _verticalSpeedHashCode = Animator.StringToHash("VerticalSpeed");
            _isCrouchHashCode = Animator.StringToHash("IsCrouch");
            _isClimbHashCode = Animator.StringToHash("IsClimb");
            _isClimIdlebHashCode = Animator.StringToHash("IsClimbIdle");
            _deathTriggerHashCode = Animator.StringToHash("DeathTrigger");
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

        public void Death()
        {
            _animator.SetTrigger(_deathTriggerHashCode);
        }
    }
}