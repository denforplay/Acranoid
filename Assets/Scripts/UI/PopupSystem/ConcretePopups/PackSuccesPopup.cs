using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem.ConcretePopups
{
    public class PackSuccesPopup : Popup
    {
        [SerializeField] private Button _nextPackButton;
        [SerializeField] private Button _mainMenuButton;

        private IButtonMethod _nextPackButtonMethod = new ShowNextPack();
        private IButtonMethod _mainMenuButtonMethod = new OpenMainMenu();

        public override void DisableInput()
        {
            _nextPackButton.enabled = false;
            _mainMenuButton.enabled = false;
        }

        public override void EnableInput()
        {
            _nextPackButton.enabled = true;
            _mainMenuButton.enabled = true;
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
            }
            );
            _nextPackButton.onClick.AddListener(() => _nextPackButtonMethod.Call());
        }
    }
}
