using Assets.Scripts.Abstracts.Singeton;
using System;
using UnityEngine;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Newtonsoft.Json;
using Assets.Scripts.PlayerData;
using Assets.Scripts.UI.PopupSystem.ConcretePopups;
using Assets.Scripts.Block;
using Assets.Scripts.EventBus.Events.BlockEvents;

namespace Assets.Scripts.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        private bool _isLevelCompleted = false;
        private LevelsController _levelsController;
        public LevelPackObject _levelPackObject;
        public static bool IsInitialized { get; private set; }
        public Level CurrentLevel { get; private set; }
        public Sprite CurrentPackSprite { get; private set; }
        public string CurrentPackName { get; private set; }
        public bool IsLevelCompleted => _isLevelCompleted;
        public bool IsLevelRestarted;

        public void Initialize(LevelsController levelsController)
        {
            _levelsController = levelsController;
            _levelsController.OnInitialized();
            IsInitialized = true;
            EventBusManager.GetInstance.Invoke<OnLevelsInitialized>(new OnLevelsInitialized());
        }
        
        public void SetLevelCompleted(bool isCompleted)
        {
            _isLevelCompleted = true;
        }
        public Level CreateLevel(TextAsset _levelAsset)
        {
            Level prevLevel = JsonConvert.DeserializeObject<Level>(_levelAsset.text);
            var testLevel = PlayerDataManager.GetInstance.GetLevelDataForKey(_levelAsset.name);
            if (testLevel != null)
            {
                prevLevel = testLevel;
            }

            return prevLevel;
        }

        public bool IsCurrentLastLevel()
        {
            return _levelsController.IsCurrentLastLevel();
        }    

        public void SetLevelPackObject(LevelPackObject levelPackObject)
        {
            _levelPackObject = levelPackObject;
            CurrentPackSprite = levelPackObject._packImage;
            CurrentPackName = levelPackObject.packName;
            _levelsController.SetCurrentPack(levelPackObject);
            EventBusManager.GetInstance.Invoke<OnPackChangedEvent>(new OnPackChangedEvent());
        }

        public int GetPackCount(LevelPackObject levelPack)
        {
            return levelPack._jsonLevelsFiles.Count;
        }

        public int GetPackProgress(LevelPackObject levelPack)
        {
            int counter = 0;
            foreach (var levelFile in levelPack._jsonLevelsFiles)
            {
                var testLevel = CreateLevel(levelFile);
                if (testLevel.isCompleted)
                {
                    counter++;
                }
            }

            return counter;
        }

        public void SetCurrentLevel(System.Object sender, int level)
        {
            _levelsController.SetCurrentLevel(level);
            CurrentLevel = _levelsController.GetCurrentLevel();
            LoadCurrentLevel();
        }

        public void SetCurrentPack(LevelPackObject levelPackObject)
        {
            _levelPackObject = levelPackObject;
            _levelsController.SetCurrentPack(levelPackObject);
            EventBusManager.GetInstance.Invoke<OnPackChangedEvent>(new OnPackChangedEvent());
        }

        public Level LoadCurrentLevel()
        {
            CheckLevelsLoaded();
            _isLevelCompleted = true;
            EventBusManager.GetInstance.Invoke<OnNextLevelLoadedEvent>(new OnNextLevelLoadedEvent());
            _isLevelCompleted = false;
            return _levelsController.GetCurrentLevel();
        }

        public Level LoadNextLevel()
        {
            CheckLevelsLoaded();
            _isLevelCompleted = true;
            CurrentLevel = _levelsController.LoadNextLevel();
            EventBusManager.GetInstance.Invoke<OnNextLevelLoadedEvent>(new OnNextLevelLoadedEvent());
            _isLevelCompleted = false;
            return _levelsController.GetCurrentLevel();
        }

        public Level GetCurrentLevel()
        {
            CheckLevelsLoaded();
            return _levelsController.GetCurrentLevel();
        }

        public int GetCurrentLevelLifes()
        {
            CheckLevelsLoaded();
            if (GetCurrentLevel() != null)
            {
                return GetCurrentLevel().lifes;
            }
            else
            {
                return 0;
            }
        }

        private void CheckLevelsLoaded()
        {
            if (!IsInitialized)
            {
                throw new ArgumentNullException("Levels is not initialized yet");
            }
        }
    }
}
