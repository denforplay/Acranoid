using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Block;
using Assets.Scripts.Level;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Factories;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.PopupSystem.ConcretePopups
{


    public class ChoosePackagePopup : Popup
    {
        [SerializeField] private LevelPacksConfig _levelPacksConfig;
        [SerializeField] private GameObject _scrollViewContent;

        [SerializeField] private Button _packageButtonPrefab;
        [SerializeField] private Button _menuButton;

        private IButtonMethod _mainMenuButton = new ClosePopupMethod();
        private PackageButtonFactory _packageButtonFactory;
        private List<Button> _packagesButtons;
        private void Awake()
        {
            _packagesButtons = new List<Button>();
            LevelManager.GetInstance.IsLevelRestarted = true;
            _menuButton.onClick.AddListener(() =>
            {
                if (!LevelManager.GetInstance.IsLevelCompleted)
                _mainMenuButton.Call();
            }
            );
            _packageButtonFactory = new PackageButtonFactory(_packageButtonPrefab, _scrollViewContent);
            LoadPackageData();
        }

        public void LoadPackageData()
        {
            for (int i = _levelPacksConfig.levelPacks.Count - 1; i > 0; i--)
            {
                int index = i;
                _packagesButtons.Add(_packageButtonFactory.GetNewInstance(_levelPacksConfig.levelPacks[i], _levelPacksConfig.levelPacks[i - 1], () => OnPackageClickEvent(_levelPacksConfig.levelPacks[index])));
            }
            _packagesButtons.Add(_packageButtonFactory.GetNewInstance(_levelPacksConfig.levelPacks[0], null, () => OnPackageClickEvent(_levelPacksConfig.levelPacks[0])));
        }

        private void OnPackageClickEvent(LevelPackObject _levelPackObject)
        {
            LevelManager.GetInstance.SetCurrentPack(_levelPackObject);
            PopupManager.GetInstance.SpawnPopup<ChooseLevelPopup>();
        }

        public override void DisableInput()
        {
            _menuButton.interactable = false;
        }

        public override void EnableInput()
        {
            _menuButton.interactable = true;
        }
    }

}
