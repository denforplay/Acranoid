using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.Level;
using Assets.Scripts.Localisation;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem
{
    public class HeartEndsPopup : Popup
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartLevelButton;
        [SerializeField] private Button _AddBallLevelButton;

        IButtonMethod _mainMenuButtonMethod = new OpenMainMenu();
        IButtonMethod _restartLevelButtonMethod = new RestartLevel();
        IButtonMethod _addBallMethod = new AddBallMethod();

        public override void DisableInput()
        {
            _AddBallLevelButton.enabled = false;
            _mainMenuButton.enabled = false;
            _restartLevelButton.enabled = false;
        }

        public override void EnableInput()
        {
            _AddBallLevelButton.enabled = true;
            _mainMenuButton.enabled = true;
            _restartLevelButton.enabled = true;
        }

        private void Awake()
        {
            _AddBallLevelButton.onClick.AddListener(() =>
            {
                DisableInput();
                transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, _duration).OnComplete(() =>
                {
                    Time.timeScale = 1;
                    _addBallMethod.Call();
                });
            });
            _mainMenuButton.onClick.AddListener(() =>
            {
                DisableInput();
                transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, _duration).OnComplete(() =>
                {
                    Time.timeScale = 1;
                    _mainMenuButtonMethod.Call();
                });
            });
            _restartLevelButton.onClick.AddListener(() =>
            {
                DisableInput();
                transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, _duration).OnComplete(() =>
                {
                    Time.timeScale = 1;
                    _restartLevelButtonMethod.Call();
                });
            });
            LocalisationManager.GetInstance.Initialize();
        }
    }
}
