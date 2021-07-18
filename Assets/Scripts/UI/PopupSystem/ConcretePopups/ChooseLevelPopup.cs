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
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseLevelPopup : Popup
{
    [SerializeField] private LevelPacksConfig _levelPacksConfig;
    [SerializeField] private GameObject _scrollViewPackage;
    [SerializeField] private GameObject _scrollViewContent;
    [SerializeField] private GameObject _levelScrollContent;
    [SerializeField] private Button _levelButtonPrefab;
    [SerializeField] private GameObject _levelScrollViewPrefab;
    [SerializeField] private Button _packageButtonPrefab;

    public override void Show()
    {
        LoadPackageData();
    }

    public void SetLevelsData(LevelPackObject _levelPackObject)
    {
        LevelManager.GetInstance.SetLevelPackObject(_levelPackObject);
        _levelScrollViewPrefab.SetActive(true);
        Level prevLevel = CreateLevel(_levelPackObject._jsonLevelsFiles[0]);
        Button btn = CreateLevelButton(prevLevel, null, 0);
        btn.onClick.AddListener(() => OnLevelClickEvent(0));
        for (int i = 1; i < _levelPackObject._jsonLevelsFiles.Count; i++)
        {
            Level level = CreateLevel(_levelPackObject._jsonLevelsFiles[i]);
            Button button = CreateLevelButton(level, prevLevel, i);
            prevLevel = level;
        }
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
        var btnText = button.GetComponentInChildren<TextMeshProUGUI>();
        btnText.text = levelPackObject.packName;
        btnText.gameObject.AddComponent<LocalisationObject>();
        button.onClick.AddListener(() => OnPackageClickEvent(_levelPacksConfig.levelPacks[index]));
    }

    private Button CreateLevelButton(Level level, Level prevLevel, int index)
    {
        Button button = Instantiate(_levelButtonPrefab, _levelScrollContent.transform);
        TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
        text.text = level.levelName;
        text.gameObject.AddComponent<LocalisationObject>();
        button.onClick.AddListener(() => OnLevelClickEvent(index));
        if (prevLevel != null && !prevLevel.isCompleted)
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
