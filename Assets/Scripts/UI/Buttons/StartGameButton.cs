using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Level;
using Assets.Scripts.Scenes.SceneConfigs;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

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

        private void OnLevelClickEvent(int levelIndex)
        {
            LevelManager.GetInstance.SetCurrentLevel(levelIndex);
            Game.sceneManagerBase.LoadNewSceneAsync(GameSceneConfig.SCENE_NAME);
        }

        public void SetLevelsData(LevelPackObject _levelPackObject)
        {
            LevelManager.GetInstance.SetLevelPackObject(_levelPackObject);
            _levelScrollViewPrefab.SetActive(true);
            Level.Level prevLevel = JsonConvert.DeserializeObject<Level.Level>(_levelPackObject._jsonLevelsFiles[0].text);
            Button btn = CreateButton(prevLevel.levelName, _levelButtonPrefab, _levelScrollContent.transform);
            btn.onClick.AddListener(() => OnLevelClickEvent(0));
            for (int i = 1; i < _levelPackObject._jsonLevelsFiles.Count; i++)
            {
                Level.Level level = JsonConvert.DeserializeObject<Level.Level>(_levelPackObject._jsonLevelsFiles[i].text);
                Button button = CreateButton(level.levelName, _levelButtonPrefab, _levelScrollContent.transform);
                int index = i;
                button.onClick.AddListener(() => OnLevelClickEvent(index));
                if (!prevLevel.isCompleted)
                {
                    button.interactable = false;
                }
                prevLevel = level;
            }
        }

        public Button CreateButton(string btnText, Button _btnPrefab, Transform _parent)
        {
            Button button = Instantiate(_btnPrefab, _parent);
            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = btnText;
            return button;
        }

        public void LoadGameScene()
        {
            _scrollViewPackage.SetActive(true);
            for (int i = 0; i < _levelPackObjects.Count; i++)
            {
                Button button = Instantiate(_packageButtonPrefab, _scrollViewContent.transform);
                var btnText = button.GetComponentInChildren<TextMeshProUGUI>();
                btnText.text = _levelPackObjects[i].packName;
                int index = i;
                button.onClick.AddListener(() => OnPackageClickEvent(_levelPackObjects[index]));
            }
        }

        private void OnPackageClickEvent(LevelPackObject _levelPackObject)
        {
            LevelManager.GetInstance.SetCurrentPack(_levelPackObject);
            SetLevelsData(_levelPackObject);
        }
    }
}
