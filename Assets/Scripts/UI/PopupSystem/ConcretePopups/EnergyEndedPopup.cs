using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.Level;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Strategies;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem.ConcretePopups
{
    public class EnergyEndedPopup : Popup
    {
        [SerializeField] private Button _addEnergyButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartLevelButton;

        private IButtonMethod _mainMenuButtonMethod = new OpenMainMenu();
        private IButtonMethod _addEnergyButtonMethod = new AddEnergy();
        private IButtonMethod _restartLevelButtonMethod = new RestartLevel();

        public override void DisableInput()
        {
            _addEnergyButton.interactable = false;
            _mainMenuButton.interactable = false;
            _restartLevelButton.interactable = false;
        }

        public override void EnableInput()
        {
            _addEnergyButton.interactable = true;
            _mainMenuButton.interactable = true;
            _restartLevelButton.interactable = true;
        }

        private void Awake()
        {
            _mainMenuButton.onClick.AddListener(() => _mainMenuButtonMethod.Call());
            _addEnergyButton.onClick.AddListener(() => _addEnergyButtonMethod.Call());
            _restartLevelButton.onClick.AddListener(() => _restartLevelButtonMethod.Call());
        }
    }
}
