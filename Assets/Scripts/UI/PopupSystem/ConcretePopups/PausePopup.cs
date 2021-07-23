using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Popup;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using DG.Tweening;
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
            _continueButton.onClick.AddListener(() =>
            {
                DisableInput();
                transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, 2f).OnComplete(() =>
                {
                    EventBusManager.GetInstance.Invoke<OnPausePopupClosedEvent>(new OnPausePopupClosedEvent());
                    Time.timeScale = 1;
                    Destroy(gameObject);
                    _continueButtonMethod.Call();
                });
            });
            _levelChooseButton.onClick.AddListener(() => _levelChooseButtonMethod.Call());
            _mainMenuButton.onClick.AddListener(() =>
            {
                DisableInput();
                transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, 2f).OnComplete(() =>
                {
                    Time.timeScale = 1;
                    Destroy(gameObject);
                    _mainMenuButtonMethod.Call();
                });
            });
        }

        public override void Hide()
        {
            DisableInput();
            transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, 2f).OnComplete(() =>
            {
                EventBusManager.GetInstance.Invoke<OnPausePopupClosedEvent>(new OnPausePopupClosedEvent());
                Time.timeScale = 1;
                Destroy(gameObject);
            });
        }

        public override void DisableInput()
        {
            _continueButton.enabled = false;
            _levelChooseButton.enabled = false;
            _mainMenuButton.enabled = false;
        }

        public override void EnableInput()
        {
            _continueButton.enabled = true;
            _levelChooseButton.enabled = true;
            _mainMenuButton.enabled = true;
        }
    }
}
