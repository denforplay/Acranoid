using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Popup;
using Assets.Scripts.Scenes.SceneConfigs;
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

        private void Awake()
        {
            _continueButton.onClick.AddListener(ContinueGame);
            _levelChooseButton.onClick.AddListener(CallLevelChoosePopup);
            _mainMenuButton.onClick.AddListener(OpenMainMenu);
        }

        public override void Hide()
        {
            EventBusManager.GetInstance.Invoke<OnPausePopupClosedEvent>(new OnPausePopupClosedEvent());
        }

        public void OpenMainMenu()
        {
            PopupManager.GetInstance.DeletePopUp();
            SceneManager.LoadScene(StartSceneConfig.SCENE_NAME);
        }

        public void ContinueGame()
        {
            Time.timeScale = 1;
            PopupManager.GetInstance.DeletePopUp();
        }

        public void CallLevelChoosePopup()
        {
            PopupManager.GetInstance.DeletePopUp();
            PopupManager.GetInstance.SpawnPopup<ChooseLevelPopup>();
        }
    }
}
