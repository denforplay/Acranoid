using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Block;
using Assets.Scripts.Level;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Factories;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
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
        [SerializeField] private GameObject _levelScrollContent;
        [SerializeField] private Button _levelButtonPrefab;
        [SerializeField] private GameObject _levelScrollViewPrefab;
        [SerializeField] private Button _packageButtonPrefab;
        [SerializeField] private Button _menuButton;

        private IButtonMethod _mainMenuButton = new ClosePopupMethod();
        private LevelButtonFactory _levelButtonFactory;
        private PackageButtonFactory _packageButtonFactory;

        private void Awake()
        {
            LevelManager.GetInstance.IsLevelRestarted = true;
            _menuButton.onClick.AddListener(() =>
            {
                if (!LevelManager.GetInstance.IsLevelCompleted)
                _mainMenuButton.Call();
            }
            );
            _packageButtonFactory = new PackageButtonFactory(_packageButtonPrefab, _scrollViewContent);
            _levelButtonFactory = new LevelButtonFactory(_levelButtonPrefab, _levelScrollContent);
            LoadPackageData();
        }

        public void SetLevelsData(LevelPackObject _levelPackObject)
        {
            LevelManager.GetInstance.SetLevelPackObject(_levelPackObject);
            _levelScrollViewPrefab.SetActive(true);
            Level.Level currentLevel = LevelManager.GetInstance.CreateLevel(_levelPackObject._jsonLevelsFiles.Last());
            for (int i = _levelPackObject._jsonLevelsFiles.Count - 2; i >= 0; i--)
            {
                Level.Level level = LevelManager.GetInstance.CreateLevel(_levelPackObject._jsonLevelsFiles[i]);
                int index = i;
                Button button = _levelButtonFactory.GetNewInstance(currentLevel, level, () => OnLevelClickEvent(index + 1));
                currentLevel = level;
            }

            _levelButtonFactory.GetNewInstance(currentLevel, null, () => OnLevelClickEvent(0));
        }


        public void LoadPackageData()
        {
            for (int i = _levelPacksConfig.levelPacks.Count - 1; i > 0; i--)
            {
                int index = i;
                _packageButtonFactory.GetNewInstance(_levelPacksConfig.levelPacks[i], _levelPacksConfig.levelPacks[i - 1], () => OnPackageClickEvent(_levelPacksConfig.levelPacks[index]));
            }
            _packageButtonFactory.GetNewInstance(_levelPacksConfig.levelPacks[0], null, () => OnPackageClickEvent(_levelPacksConfig.levelPacks[0]));
        }
        private void OnLevelClickEvent(int levelIndex)
        {
            PopupManager.GetInstance.DeletePopUp();
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                BlocksManager.GetInstance.ReturnAllBlocks(null);
            }
            else if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                Game.sceneManagerBase.LoadNewSceneAsync(GameSceneConfig.SCENE_NAME);
            }
            LevelManager.GetInstance.SetCurrentLevel(this, levelIndex);
            LevelManager.GetInstance.IsLevelRestarted = false;
        }

        private void OnPackageClickEvent(LevelPackObject _levelPackObject)
        {
            LevelManager.GetInstance.SetCurrentPack(_levelPackObject);
            SetLevelsData(_levelPackObject);
        }

        public override void DisableInput()
        {
        }

        public override void EnableInput()
        {
        }
    }

}
