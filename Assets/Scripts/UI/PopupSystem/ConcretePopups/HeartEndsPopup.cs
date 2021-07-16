using Assets.Scripts.Level;
using Assets.Scripts.Scenes.SceneConfigs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem
{
    public class HeartEndsPopup : Popup
    {
        [SerializeField] private Button _mainMenu;
        [SerializeField] private Button _restartButton;

        private void Awake()
        {
            _mainMenu.onClick.AddListener(OpenMainMenu);
            _restartButton.onClick.AddListener(RestartLevel);
        }

        private void OpenMainMenu()
        {
            PopupManager.GetInstance.DeletePopUp();
            SceneManager.LoadScene(StartSceneConfig.SCENE_NAME);
        }

        private void RestartLevel()
        {
            PopupManager.GetInstance.DeletePopUp();
            LevelManager.GetInstance.LoadCurrentLevel();
        }
    }
}
