using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.Level;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem
{
    public class LevelSuccessPopup : Popup
    {
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _nextLevelButton;

        private IButtonMethod _mainMenuButtonMethod = new OpenMainMenu();
        private IButtonMethod _nextLevelButtonMethod = new LoadNextLevel();

        public override void DisableInput()
        {
            _mainMenuButton.enabled = false;
            _nextLevelButton.enabled = false;
        }

        public override void EnableInput()
        {
            _mainMenuButton.enabled = true;
            _nextLevelButton.enabled = true;
        }

        private void Awake()
        {
            _mainMenuButton.onClick.AddListener(() =>
            {
                DisableInput();
                transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, _duration).OnComplete(() =>
                {
                    Time.timeScale = 1;
                    _mainMenuButtonMethod.Call();
                });
            });
            _nextLevelButton.onClick.AddListener(() =>
            {
                DisableInput();
                transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, _duration).OnComplete(() =>
                {
                    Time.timeScale = 1;
                    _nextLevelButtonMethod.Call();
                });
            });
        }
    }
}
