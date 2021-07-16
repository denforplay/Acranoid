using Assets.Scripts.Scenes.SceneConfigs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem.ConcretePopups
{
    public class PackSuccesPopup : Popup
    {
        [SerializeField] private Button _nextPackButton;
        [SerializeField] private Button _mainMenuButton;

        private void Awake()
        {
            _mainMenuButton.onClick.AddListener(OpenMainMenu);
            _nextPackButton.onClick.AddListener(ShowNextPack);
        }

        private void OpenMainMenu()
        {
            PopupManager.GetInstance.DeletePopUp();
            SceneManager.LoadScene(StartSceneConfig.SCENE_NAME);
        }

        private void ShowNextPack()
        {
            PopupManager.GetInstance.DeletePopUp();
            PopupManager.GetInstance.SpawnPopup<ChooseLevelPopup>();
        }
    }
}
