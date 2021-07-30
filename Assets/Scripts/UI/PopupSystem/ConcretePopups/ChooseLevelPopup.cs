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
    public class ChooseLevelPopup : Popup
    {
        [SerializeField] private GameObject _levelScrollViewPrefab;
        [SerializeField] private GameObject _levelScrollContent;
        [SerializeField] private Button _levelButtonPrefab;
        [SerializeField] private Button _backButton;

        private IButtonMethod _backButtonMethod = new ClosePopupMethod();

        private LevelButtonFactory _levelButtonFactory;

        private void Awake()
        {
            _backButton.onClick.AddListener(() =>
            {
                _backButtonMethod.Call();
            });
            _levelButtonFactory = new LevelButtonFactory(_levelButtonPrefab, _levelScrollContent);
            SetLevelsData(LevelManager.GetInstance._levelPackObject);
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

        public override void DisableInput()
        {
        }

        public override void EnableInput()
        {
        }

        private void OnLevelClickEvent(int levelIndex)
        {
            PopupManager.GetInstance.DeleteAllPopups();
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
    }
}
