using Assets.Scripts.Abstracts.EventBus;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Block;
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.EventBus;
using Assets.Scripts.Level;
using Assets.Scripts.Localisation;
using Assets.Scripts.PlayerData;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.PopupSystem;
using Newtonsoft.Json;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevelPopup : Popup
{
    [SerializeField] private LevelPacksConfig _levelPacksConfig;
    [SerializeField] private GameObject _scrollViewContent;
    [SerializeField] private GameObject _levelScrollContent;
    [SerializeField] private Button _levelButtonPrefab;
    [SerializeField] private GameObject _levelScrollViewPrefab;
    [SerializeField] private Button _packageButtonPrefab;

    public override void Show()
    {
        base.Show();
        LoadPackageData();
    }

    public void SetLevelsData(LevelPackObject _levelPackObject)
    {
        LevelManager.GetInstance.SetLevelPackObject(_levelPackObject);
        _levelScrollViewPrefab.SetActive(true);
        Level currentLevel = CreateLevel(_levelPackObject._jsonLevelsFiles.Last());
        for (int i = _levelPackObject._jsonLevelsFiles.Count - 2; i >= 0; i--)
        {
            Level level = CreateLevel(_levelPackObject._jsonLevelsFiles[i]);
            Button button = CreateLevelButton(currentLevel, level, i);
            currentLevel = level;
        }

        Level nextLevel = CreateLevel(_levelPackObject._jsonLevelsFiles.First());
        CreateLevelButton(nextLevel, null, 0);
    }


    public void LoadPackageData()
    {
        for (int i = 0; i < _levelPacksConfig.levelPacks.Count; i++)
        {
            CreatePackageButton(_levelPacksConfig.levelPacks[i], i);
        }
    }

    private void CreatePackageButton(LevelPackObject levelPackObject, int index)
    {
        Button button = Instantiate(_packageButtonPrefab, _scrollViewContent.transform);
        Image btnImage = button.GetComponentsInChildren<Image>()[1];
        btnImage.sprite = levelPackObject._packImage;
        TextMeshProUGUI[] btnTexts = button.GetComponentsInChildren<TextMeshProUGUI>();
        btnTexts[0].text = levelPackObject.packName;
        btnTexts[0].gameObject.AddComponent<LocalisationObject>();
        button.onClick.AddListener(() => OnPackageClickEvent(_levelPacksConfig.levelPacks[index]));
    }

    private Button CreateLevelButton(Level currentLevel, Level nextLevel, int index)
    {
        Button button = Instantiate(_levelButtonPrefab, _levelScrollContent.transform);
        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        text.text = currentLevel.levelName;
        text.gameObject.AddComponent<LocalisationObject>();
        button.onClick.AddListener(() => OnLevelClickEvent(index));
        if (nextLevel != null && !nextLevel.isCompleted)
        {
            button.interactable = false;
        }
        return button;
    }

    private Level CreateLevel(TextAsset _levelAsset)
    {
        Level prevLevel = JsonConvert.DeserializeObject<Level>(_levelAsset.text);
        var testLevel = PlayerDataManager.GetInstance.GetLevelDataForKey(_levelAsset.name);
        if (testLevel != null)
        {
            prevLevel = testLevel;
        }

        return prevLevel;
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
        EnergyManager.GetInstance.SpendEnergy(1);
    }

    private void OnPackageClickEvent(LevelPackObject _levelPackObject)
    {
        LevelManager.GetInstance.SetCurrentPack(_levelPackObject);
        SetLevelsData(_levelPackObject);
    }
}
