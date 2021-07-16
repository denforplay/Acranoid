using Assets.Scripts.Level;
using Assets.Scripts.Scenes.SceneConfigs;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem
{
    public class LevelSuccessPopup : Popup
    {
        [SerializeField] private Button _mainMenu;
        [SerializeField] private Button _nextLevel;

        private void Awake()
        {
            _mainMenu.onClick.AddListener(OpenMainMenu);
            _nextLevel.onClick.AddListener(LoadNextLevel);
        }

        private void OpenMainMenu()
        {
            PopupManager.GetInstance.DeletePopUp();
            SceneManager.LoadScene(StartSceneConfig.SCENE_NAME);
        }

        private void LoadNextLevel()
        {
            PopupManager.GetInstance.DeletePopUp();
            LevelManager.GetInstance.LoadNextLevel();
        }
    }
}
