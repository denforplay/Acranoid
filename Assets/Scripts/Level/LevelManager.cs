using Assets.Scripts.Abstracts.Singeton;
using System;
using UnityEngine;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Newtonsoft.Json;
using System.Collections.Generic;
using Assets.Scripts.Health;

namespace Assets.Scripts.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private LevelPackObject _levelPackObject;
        private LevelsController _levelsController;
        public bool IsInitialized { get; private set; }
        public Level CurrentLevel { get; private set; }

        public void Initialize(LevelsController levelsController)
        {
            _levelsController = levelsController;
            LoadJsonPack(_levelPackObject);
            _levelsController.OnInitialized();
            IsInitialized = true;
            EventBusManager.GetInstance.Invoke<OnLevelsInitialized>(new OnLevelsInitialized());
        }

        private void LoadJsonPack(LevelPackObject levelPackObject)
        {
            List<Level> levelsPack = new List<Level>();
            JsonParser jsonParser = new JsonParser();
            foreach (var levelData in levelPackObject._jsonLevelsFiles)
            {
                Level level = jsonParser.LoadJsonData(levelData);
                level.textAsset = levelData;
                levelsPack.Add(level);
            }

            _levelsController.SetPack(levelsPack);
            _levelsController.AddPack(levelsPack);
        }

        public void SetLevelPackObject(LevelPackObject levelPackObject)
        {
            _levelPackObject = levelPackObject;
            LoadJsonPack(levelPackObject);
            EventBusManager.GetInstance.Invoke<OnPackChangedEvent>(new OnPackChangedEvent());
        }

        public void SetCurrentLevel(int level)
        {
            _levelsController.SetCurrentLevel(level);
            CurrentLevel = _levelsController.GetCurrentLevel();
        }

        public void SetCurrentPack(int index)
        {
            _levelsController.SetCurrentPack(index);
        }

        public Level LoadNextLevel()
        {
            CheckLevelsLoaded();
            JsonParser jsonParser = new JsonParser();
            jsonParser.SetJsonData(CurrentLevel);
            EventBusManager.GetInstance.Invoke<OnLevelCompletedEvent>(new OnLevelCompletedEvent());
            CurrentLevel = _levelsController.LoadNextLevel();
            EventBusManager.GetInstance.Invoke<OnNextLevelLoadedEvent>(new OnNextLevelLoadedEvent());
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
            return GetCurrentLevel().lifes;
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
