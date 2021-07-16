using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Assets.Scripts.PlayerData;
using Assets.Scripts.UI.PopupSystem;
using Assets.Scripts.UI.PopupSystem.ConcretePopups;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Level
{
    public class LevelsController : Controller
    {
        private LevelRepository _levelRepository;
        public int GetCurrentLevelLifes()
        {
            return GetCurrentLevel().lifes;
        }

        public override void OnCreate()
        {
            this._levelRepository = Game.GetRepository<LevelRepository>();
        }

        public override void Initialize()
        {
            LevelManager.GetInstance.Initialize(this);

        }

        public void OnInitialized()
        {
            if (_levelRepository.CurrentPack != null && _levelRepository.CurrentPack.Count != 0)
            {
                _levelRepository.CurrentLevel = _levelRepository.CurrentPack.First();
            }
        }

        public bool IsCurrentLastLevel()
        {
            Level currentLevel = _levelRepository.CurrentPack.Find(x => x.levelName == _levelRepository.CurrentLevel.levelName);
            int currentLevelIndex = _levelRepository.CurrentPack.IndexOf(currentLevel);
            return currentLevelIndex + 1 >= _levelRepository.CurrentPack.Count;
        }

        public Level LoadNextLevel()
        {
            Level currentLevel = _levelRepository.CurrentPack.Find(x => x.levelName == _levelRepository.CurrentLevel.levelName);
            int currentLevelIndex = _levelRepository.CurrentPack.IndexOf(currentLevel);
            int nextLevelIndex = currentLevelIndex + 1;
            Level nextLevel = null;
            if (nextLevelIndex >= _levelRepository.CurrentPack.Count)
            {
                EventBusManager.GetInstance.Invoke<OnPackCompletedEvent>(new OnPackCompletedEvent());
            }
            else
            {
                nextLevel = _levelRepository.CurrentPack[nextLevelIndex];
                _levelRepository.CurrentLevel = nextLevel;
            }
            return nextLevel;
        }

        public void SetPack(List<Level> levels)
        {
            _levelRepository.CurrentPack = levels;
        }

        public void SetCurrentLevel(int index)
        {
            _levelRepository.CurrentLevel = PlayerDataManager.GetInstance.GetLevelDataForKey(_levelRepository.CurrentPack[index].textAsset.name);
            _levelRepository.CurrentLevel.textAsset = _levelRepository.CurrentPack[index].textAsset;
        }

        public void SetCurrentPack(LevelPackObject _levelPack)
        {
            JsonParser jsonParser = new JsonParser();
            List<Level> pack = new List<Level>();
            foreach (var levelAsset in _levelPack._jsonLevelsFiles)
            {
                Level level = jsonParser.LoadJsonData(levelAsset);
                level.textAsset = levelAsset;
                if (PlayerDataManager.GetInstance.GetLevelDataForKey(level.textAsset.name) == null)
                PlayerDataManager.GetInstance.SetStringDataForKey(level.textAsset.name, level.textAsset.text);
                pack.Add(level);
            }

            _levelRepository.CurrentPack = pack;
        }

        public List<Level> GetCurrentPack()
        {
            return _levelRepository.CurrentPack;
        }

        public Level GetCurrentLevel()
        {
            return _levelRepository.CurrentLevel;
        }
    }
}
