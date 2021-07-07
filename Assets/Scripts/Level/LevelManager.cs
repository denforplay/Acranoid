using Assets.Scripts.Abstracts.Singeton;
using System;
using UnityEngine;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;

namespace Assets.Scripts.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private TextAsset _jsonLevelsFile;
        private LevelsController _levelsController;

        public bool IsInitialized { get; private set; }
        public Level CurrentLevel { get; private set; }

        private void LoadJsonLevels()
        {
            LevelPackages levelPackages = JsonUtility.FromJson<LevelPackages>(_jsonLevelsFile.text);

            foreach (LevelPack levelPack in levelPackages.levelPacks)
            {
                _levelsController.AddPack(levelPack);
            }
        }

        public Level LoadNextLevel()
        {
            CheckLevelsLoaded();
            EventBusManager.GetInstance.Invoke<OnLevelCompletedEvent>(new OnLevelCompletedEvent());
            CurrentLevel = _levelsController.LoadNextLevel();
            EventBusManager.GetInstance.Invoke<OnNextLevelLoadedEvent>(new OnNextLevelLoadedEvent());
            return CurrentLevel;
        }

        public Level GetCurrentLevel()
        {
            CheckLevelsLoaded();
            return _levelsController.GetLastLevel();
        }

        public int GetCurrentLevelLifes()
        {
            return GetCurrentLevel().lifes;
        }

        public void Initialize(LevelsController levelsController)
        {
            _levelsController = levelsController;
            LoadJsonLevels();
            IsInitialized = true;
            EventBusManager.GetInstance.Invoke<OnLevelsInitialized>(new OnLevelsInitialized());
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
