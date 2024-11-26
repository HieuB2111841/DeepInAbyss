using Managers.Extension;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Game.Players
{
    public class HomeManager : MonoBehaviour
    {
        private UIDocument _document;
        private VisualElement _root;

        private VisualElement _playButton;
        private VisualElement _settingsButton;
        private VisualElement _exitButton;

        private VisualElement _confilmPanel;
        private VisualElement _loadingPanel;

        private void Awake()
        {
            this.LoadComponents();
        }

        protected virtual void LoadComponents()
        {
            this.LoadComponent(ref _document);
            _root = _document.rootVisualElement;

            _playButton = _root.Q<VisualElement>("PlayButton");
            _settingsButton = _root.Q<VisualElement>("SettingsButton");
            _exitButton = _root.Q<VisualElement>("ExitButton");

            _confilmPanel = _root.Q<VisualElement>("ConfilmPanel");
            _loadingPanel = _root.Q<VisualElement>("LoadingPanel");
        }

        private void Start()
        {
            _loadingPanel.AddToClassList("hide");
            this.SetupHomeScreen();
            this.SetupConfilmPanel();
        }

        public void CallConfilmPanel()
        {
            _confilmPanel.RemoveFromClassList("hide");
        }


        private void SetupHomeScreen()
        {
            _playButton.RegisterCallback<MouseDownEvent>((e) =>
            {
                _loadingPanel.RemoveFromClassList("hide");
                SceneManager.LoadScene("VillageScene");
            });

            _settingsButton.RegisterCallback<MouseDownEvent>((e) =>
            {

            });

            _exitButton.RegisterCallback<MouseDownEvent>((e) =>
            {
                this.CallConfilmPanel();
            });
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
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            });

            _confilmPanel?.AddToClassList("hide");
        }
    }
}