using Game.UIs;
using Managers.Extension;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Game.Players
{
    public class PlayerUI : PlayerAbstraction
    {
        [SerializeField] private Sprite _baseAttackSprite;

        private UIDocument _document;
        private VisualElement _root;

        private UISlideStat _uiHealth;
        private UIStatInfo _uiDamage;
        private UIStatInfo _uiArmor;
        private UIStatInfo _uiSpeed;

        private UIAbilityInfo _baseAttack;

        private VisualElement _settingsPanel;
        private VisualElement _deathPanel;
        private VisualElement _confilmPanel;
        private VisualElement _loadingPanel;


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

            _baseAttack = _root.Q<UIAbilityInfo>("BaseAttack");
            _settingsPanel = _root.Q<VisualElement>("SettingsPanel");
            _deathPanel = _root.Q<VisualElement>("DeathPanel");
            _confilmPanel = _root.Q<VisualElement>("ConfilmPanel");
            _loadingPanel = _root.Q<VisualElement>("LoadingPanel");
        }

        private void Start()
        {
            if(_baseAttackSprite != null)
            {
                _baseAttack.SetImage(_baseAttackSprite);
            }

            _loadingPanel.AddToClassList("hide");
            this.SetupActionsBar();
            this.SetupSettingPanel();
            this.SetupDeathPanel();
            this.SetupConfilmPanel();
        }


        public void SetHealth(float? remaining = null, float? max = null)
        {
            if(remaining != null) _uiHealth.CurrentValue = (float)remaining;
            if (max != null) _uiHealth.MaxValue = (float)max;
        }

        public void SetBaseAttackFilter(bool isShow)
        {
            _baseAttack.ShowFilter(isShow);
        }

        public void ShowDeathPanel()
        {
            _deathPanel.RemoveFromClassList("hide");
        }

        public void CallConfilmPanel()
        {
            _confilmPanel.RemoveFromClassList("hide");
        }

        private void SetupActionsBar()
        {
            _root.Q<VisualElement>("Settings")?.RegisterCallback<MouseDownEvent>((e) =>
            {
                _settingsPanel?.RemoveFromClassList("hide");
            });
        }

        private void SetupSettingPanel()
        {
            VisualElement closeButton = _root.Q<VisualElement>("SettingsPanelCloseButton");
            closeButton?.RegisterCallback<MouseDownEvent>((e) =>
            {
                _settingsPanel?.AddToClassList("hide");
            });

            VisualElement goHomeButton = _root.Q<VisualElement>("SettingsPanelGoHomeButton");
            goHomeButton?.RegisterCallback<MouseDownEvent>((e) =>
            {
                this.CallConfilmPanel();
            });

            VisualElement continueButton = _root.Q<VisualElement>("SettingsPanelContinueButton");
            continueButton?.RegisterCallback<MouseDownEvent>((e) =>
            {
                _settingsPanel?.AddToClassList("hide");
            });

            _settingsPanel?.AddToClassList("hide");
        }

        private void SetupDeathPanel()
        {
            VisualElement goHomeButton = _root.Q<VisualElement>("DeathPanelGoHomeButton");
            goHomeButton?.RegisterCallback<MouseDownEvent>((e) =>
            {
                this.CallConfilmPanel();
            });

            VisualElement playAgainButton = _root.Q<VisualElement>("DeathPanelPlayAgainButton");
            playAgainButton?.RegisterCallback<MouseDownEvent>((e) =>
            {
                Player.Character.Spawn();
                _deathPanel?.AddToClassList("hide");
            });

            _deathPanel?.AddToClassList("hide");
        }

        private void SetupConfilmPanel()
        {
            VisualElement noButton = _root.Q<VisualElement>("ConfilmPanelNoButton");
            noButton?.RegisterCallback<MouseDownEvent>((e) =>
            {
                _confilmPanel.AddToClassList("hide");
            });

            VisualElement yesButton = _root.Q<VisualElement>("ConfilmPanelYesButton");
            yesButton?.RegisterCallback<MouseDownEvent>((e) =>
            {
                _loadingPanel.RemoveFromClassList("hide");
                SceneManager.LoadScene("HomeScene");
            });

            _confilmPanel?.AddToClassList("hide");
        }
    }
}