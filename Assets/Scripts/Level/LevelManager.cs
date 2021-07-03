using System;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class LevelManager : MonoBehaviour
    {
        public static event Action OnLevelsInitialized;
        public static LevelManager instance;

        [SerializeField] private TextAsset _jsonLevelsFile;

        public bool IsInitialized { get; private set; }
        private LevelsController _levelsController;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void LoadJsonLevels()
        {
            Levels levels = JsonUtility.FromJson<Levels>(_jsonLevelsFile.text);

            foreach (Level level in levels.levels)
            {
                _levelsController.AddLevel(level);
            }
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
