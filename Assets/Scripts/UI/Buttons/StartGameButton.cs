using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Level;
using Assets.Scripts.Scenes.SceneConfigs;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using Assets.Scripts.PlayerData;

namespace Assets.Scripts.UI.Buttons
{
    public class StartGameButton : MonoBehaviour
    {
        [SerializeField] private List<LevelPackObject> _levelPackObjects;
        [SerializeField] private GameObject _scrollViewPackage;
        [SerializeField] private GameObject _scrollViewContent;
        [SerializeField] private GameObject _levelScrollContent;
        [SerializeField] private Button _levelButtonPrefab;
        [SerializeField] private GameObject _levelScrollViewPrefab;
        [SerializeField] private Button _packageButtonPrefab;


        public void SetLevelsData(LevelPackObject _levelPackObject)
        {
            LevelManager.GetInstance.SetLevelPackObject(_levelPackObject);
            _levelScrollViewPrefab.SetActive(true);
            Level.Level prevLevel = CreateLevel(_levelPackObject._jsonLevelsFiles[0]);
            Button btn = CreateLevelButton(prevLevel, null, 0);
            btn.onClick.AddListener(() => OnLevelClickEvent(0));
            for (int i = 1; i < _levelPackObject._jsonLevelsFiles.Count; i++)
            {
                Level.Level level = CreateLevel(_levelPackObject._jsonLevelsFiles[i]);
                Button button = CreateLevelButton(level, prevLevel, i);
                prevLevel = level;
            }
        }


        public void LoadGameScene()
        {
            _scrollViewPackage.SetActive(true);
            for (int i = 0; i < _levelPackObjects.Count; i++)
            {
                CreatePackageButton(_levelPackObjects[i], i);
            }
        }

        private void CreatePackageButton(LevelPackObject levelPackObject, int index)
        {
            Button button = Instantiate(_packageButtonPrefab, _scrollViewContent.transform);
            var btnText = button.GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = levelPackObject.packName;
            button.onClick.AddListener(() => OnPackageClickEvent(_levelPackObjects[index]));
        }

        private Button CreateLevelButton(Level.Level level, Level.Level prevLevel, int index)
        {
            Button button = Instantiate(_levelButtonPrefab, _levelScrollContent.transform);
            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = level.levelName;
            button.onClick.AddListener(() => OnLevelClickEvent(index));
            if (prevLevel != null && !prevLevel.isCompleted)
            {
                button.interactable = false;
            }
            return button;
        }

        private Level.Level CreateLevel(TextAsset _levelAsset)
        {
            Level.Level prevLevel = JsonConvert.DeserializeObject<Level.Level>(_levelAsset.text);
            var testLevel = PlayerDataManager.GetInstance.GetLevelDataForKey(_levelAsset.name);
            if (testLevel != null)
            {
                prevLevel = testLevel;
            }

            return prevLevel;
        }

        private void OnLevelClickEvent(int levelIndex)
        {
            LevelManager.GetInstance.SetCurrentLevel(levelIndex);
            Game.sceneManagerBase.LoadNewSceneAsync(GameSceneConfig.SCENE_NAME);
        }

        private void OnPackageClickEvent(LevelPackObject _levelPackObject)
        {
            LevelManager.GetInstance.SetCurrentPack(_levelPackObject);
            SetLevelsData(_levelPackObject);
        }
    }
}
