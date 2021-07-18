using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Popup;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem
{
    public class PausePopup : Popup
    {
        [SerializeField] Button _continueButton;
        [SerializeField] Button _levelChooseButton;
        [SerializeField] Button _mainMenuButton;

        private IButtonMethod _levelChooseButtonMethod = new ShowNextPack();
        private IButtonMethod _mainMenuButtonMethod = new OpenMainMenu();
        private IButtonMethod _continueButtonMethod = new ContinueGame();
        private void Awake()
        {
            _continueButton.onClick.AddListener(() => _continueButtonMethod.Call());
            _levelChooseButton.onClick.AddListener(() => _levelChooseButtonMethod.Call());
            _mainMenuButton.onClick.AddListener(() => _mainMenuButtonMethod.Call());
        }

        public override void Hide()
        {
            EventBusManager.GetInstance.Invoke<OnPausePopupClosedEvent>(new OnPausePopupClosedEvent());
        }
    }
}
