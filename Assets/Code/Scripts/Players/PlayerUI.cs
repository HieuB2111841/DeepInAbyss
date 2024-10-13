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

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadComponent(ref _document);
            _root = _document.rootVisualElement;
            _uiHealth = _root.Q<UISlideStat>("Health");
        }

        public void SetHealth(float? remaining = null, float? max = null)
        {
            if(remaining != null) _uiHealth.CurrentValue = (float)remaining;
            if (max != null) _uiHealth.MaxValue = (float)max;
        }
    }
}