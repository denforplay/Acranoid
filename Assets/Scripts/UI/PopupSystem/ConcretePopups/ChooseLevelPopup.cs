using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Block;
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.Level;
using Assets.Scripts.PlayerData;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Factories;
using Assets.Scripts.UI.Buttons.Strategies.ButtonMethods;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;
using DG.Tweening;
using Newtonsoft.Json;
using System.Linq;
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
    [SerializeField] private Button _menuButton;

    private IButtonMethod _mainMenuButton = new ClosePopupMethod();
    private LevelButtonFactory _levelButtonFactory;
    private PackageButtonFactory _packageButtonFactory;

    private void Awake()
    {
        _menuButton.onClick.AddListener(() => _mainMenuButton.Call());
        _packageButtonFactory = new PackageButtonFactory(_packageButtonPrefab, _scrollViewContent);
        _levelButtonFactory = new LevelButtonFactory(_levelButtonPrefab, _levelScrollContent);
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
            int index = i;
            Button button = _levelButtonFactory.GetNewInstance(currentLevel, level, () => OnLevelClickEvent(index));
            currentLevel = level;
        }

        Level nextLevel = CreateLevel(_levelPackObject._jsonLevelsFiles.First());
        _levelButtonFactory.GetNewInstance(nextLevel, null, () => OnLevelClickEvent(0));
    }


    public void LoadPackageData()
    {
        for (int i = 0; i < _levelPacksConfig.levelPacks.Count; i++)
        {
            int index = i;
            _packageButtonFactory.GetNewInstance(_levelPacksConfig.levelPacks[i], () => OnPackageClickEvent(_levelPacksConfig.levelPacks[index]));
        }
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
        EnergyManager.GetInstance.SpendEnergy(1);
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
