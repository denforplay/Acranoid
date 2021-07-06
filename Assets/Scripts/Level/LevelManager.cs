using Assets.Scripts.Abstracts.Singeton;
using System;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private TextAsset _jsonLevelsFile;
        public static event Action OnLevelCompeted;
        public static event Action OnLevelsInitialized;
        public static event Action OnNextLevelLoaded;
        public static event Action OnLevelLoaded;
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
            OnLevelCompeted?.Invoke();
            CurrentLevel = _levelsController.LoadNextLevel();
            OnNextLevelLoaded?.Invoke();
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
            OnLevelsInitialized?.Invoke();
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
