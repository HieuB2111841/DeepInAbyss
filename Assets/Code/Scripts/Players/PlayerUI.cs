using Game.UIs;
using Managers.Extension;
using UnityEngine.UIElements;

namespace Game.Players
{
    public class PlayerUI : PlayerAbstraction
    {
        private UIDocument _document;
        private VisualElement _root;

        private UISlideStat _uiHealth;
        private UIStatInfo _uiDamage;
        private UIStatInfo _uiArmor;
        private UIStatInfo _uiSpeed;


        public float UIDamage
        {
            get => _uiDamage.Value;
            set => _uiDamage.Value = value;
        }

        public float UIArmor
        {
            get => _uiArmor.Value;
            set => _uiArmor.Value = value;
        }

        public float UISpeed
        {
            get => _uiSpeed.Value;
            set => _uiSpeed.Value = value;
        }


        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _document);
            _root = _document.rootVisualElement;
            _uiHealth = _root.Q<UISlideStat>("Health");
            _uiDamage = _root.Q<UIStatInfo>("Damage");
            _uiArmor = _root.Q<UIStatInfo>("Armor");
            _uiSpeed = _root.Q<UIStatInfo>("Speed");
        }

        public void SetHealth(float? remaining = null, float? max = null)
        {
            if(remaining != null) _uiHealth.CurrentValue = (float)remaining;
            if (max != null) _uiHealth.MaxValue = (float)max;
        }
    }
}