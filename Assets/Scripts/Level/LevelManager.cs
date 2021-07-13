using Assets.Scripts.Abstracts.Singeton;
using System;
using UnityEngine;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Newtonsoft.Json;
using System.Collections.Generic;
using Assets.Scripts.Health;
using UnityEngine.UI;

namespace Assets.Scripts.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private LevelPackObject _levelPackObject;
        private LevelsController _levelsController;
        public bool IsInitialized { get; private set; }
        public Level CurrentLevel { get; private set; }
        public Sprite CurrentPackSprite { get; private set; }
        public string CurrentPackName { get; private set; }
        public void Initialize(LevelsController levelsController)
        {
            _levelsController = levelsController;
            _levelsController.OnInitialized();
            IsInitialized = true;
            EventBusManager.GetInstance.Invoke<OnLevelsInitialized>(new OnLevelsInitialized());
        }

        public void SetLevelPackObject(LevelPackObject levelPackObject)
        {
            _levelPackObject = levelPackObject;
            CurrentPackSprite = levelPackObject._packImage;
            CurrentPackName = levelPackObject.packName;
            _levelsController.SetCurrentPack(levelPackObject);
            EventBusManager.GetInstance.Invoke<OnPackChangedEvent>(new OnPackChangedEvent());
        }

        public void SetCurrentLevel(int level)
        {
            _levelsController.SetCurrentLevel(level);
            CurrentLevel = _levelsController.GetCurrentLevel();
        }

        public void SetCurrentPack(LevelPackObject levelPackObject)
        {
            _levelsController.SetCurrentPack(levelPackObject);
        }

        public Level LoadCurrentLevel()
        {
            CheckLevelsLoaded();
            EventBusManager.GetInstance.Invoke<OnNextLevelLoadedEvent>(new OnNextLevelLoadedEvent());
            return _levelsController.GetCurrentLevel();
        }

        public Level LoadNextLevel()
        {
            CheckLevelsLoaded();
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
