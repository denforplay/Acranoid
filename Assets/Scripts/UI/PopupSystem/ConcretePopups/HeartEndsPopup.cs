using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.Level;
using Assets.Scripts.Localisation;
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
            LocalisationManager.GetInstance.Initialize();
        }

        private void OpenMainMenu()
        {
            PopupManager.GetInstance.DeletePopUp();
            SceneManager.LoadScene(StartSceneConfig.SCENE_NAME);
        }

        private void RestartLevel()
        {
            EnergyManager.GetInstance.SpendEnergy(1);
            PopupManager.GetInstance.DeletePopUp();
            LevelManager.GetInstance.LoadCurrentLevel();
        }
    }
}
