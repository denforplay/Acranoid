using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
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
            _levelRepository.CurrentPack = _levelRepository.levelPacks.First();
            if (_levelRepository.CurrentLevel is null)
            {
                _levelRepository.CurrentLevel = _levelRepository.CurrentPack.First();
            }
        }

        public Level LoadNextLevel()
        {
            int nextLevelIndex = _levelRepository.CurrentPack.IndexOf(_levelRepository.CurrentLevel) + 1;
            if (nextLevelIndex < _levelRepository.CurrentPack.Count)
            {
                EventBusManager.GetInstance.Invoke<OnPackCompletedEvent>(new OnPackCompletedEvent());
            }
            Level nextLevel = _levelRepository.CurrentPack[_levelRepository.CurrentPack.IndexOf(_levelRepository.CurrentLevel) + 1];
            _levelRepository.CurrentLevel = nextLevel;
            return nextLevel;
        }

        public void SetPack(List<Level> levels)
        {
            _levelRepository.CurrentPack = levels;
        }

        public List<Level> LoadNextPack()
        {
            List<Level> nextPack = _levelRepository.levelPacks[_levelRepository.levelPacks.IndexOf(_levelRepository.CurrentPack) + 1];
            _levelRepository.CurrentPack = nextPack;
            return nextPack;
        }

        public void SetCurrentLevel(int index)
        {
            _levelRepository.CurrentLevel = _levelRepository.CurrentPack[index];
        }

        public List<Level> GetCurrentPack()
        {
            return _levelRepository.CurrentPack;
        }

        public Level GetCurrentLevel()
        {
            return _levelRepository.CurrentLevel;
        }

        public void AddPack(List<Level> levelPack)
        {
            _levelRepository.levelPacks.Add(levelPack);
        }
    }
}
