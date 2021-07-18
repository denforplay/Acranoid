using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.Level;
using Assets.Scripts.Localisation;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem
{
    public class HeartEndsPopup : Popup
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartLevelButton;

        IButtonMethod _mainMenuButtonMethod = new OpenMainMenu();
        IButtonMethod _restartLevelButtonMethod = new RestartLevel();

        private void Awake()
        {
            _mainMenuButton.onClick.AddListener(() => _mainMenuButtonMethod.Call());
            _restartLevelButton.onClick.AddListener(() => _restartLevelButtonMethod.Call());
            LocalisationManager.GetInstance.Initialize();
        }
    }
}
