using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Scenes.SceneConfigs;

namespace Assets.Scripts.UI.ScrollView
{
    public class GenerateLevelScrollButton : MonoBehaviour
    {
        [SerializeField] private GameObject _levelScrollViewPrefab;
        [SerializeField] private GameObject _levelScrollContent;
        [SerializeField] private LevelPackObject _levelPackObject;
        [SerializeField] private Button _levelButtonPrefab;

        public void SetLevelsData()
        {
            LevelManager.GetInstance.SetLevelPackObject(_levelPackObject);
            _levelScrollViewPrefab.SetActive(true);
            for (int i =0; i < _levelPackObject._jsonLevelsFiles.Count; i++)
            {
                Button button = Instantiate(_levelButtonPrefab, _levelScrollContent.transform);
                Text btnText = button.GetComponentInChildren<Text>();
                btnText.text = $"Level{i + 1}";
                int index = i;
                button.onClick.AddListener(() => OnLevelClickEvent(index));
            }
        }

        private void OnLevelClickEvent(int levelIndex)
        {
            LevelManager.GetInstance.SetCurrentLevel(levelIndex);
            Game.sceneManagerBase.LoadNewSceneAsync(GameSceneConfig.SCENE_NAME);
        }
    }
}
