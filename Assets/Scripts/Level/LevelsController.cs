using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.Level
{
    public class LevelsController : Controller
    {
        private LevelRepository _levelRepository;
        public int GetCurrentLevelLifes()
        {
            return GetLastLevel().lifes;
        }

        public Level LoadNextLevel()
        {
            if (++_levelRepository.CurrentLevel < _levelRepository.levelPacks[_levelRepository.CurrentPack].levels.Length)
            {
                _levelRepository.CurrentLevel = _levelRepository.CurrentLevel;
            }
            else
            {
                _levelRepository.CurrentLevel = 0;
                if (++_levelRepository.CurrentPack >= _levelRepository.levelPacks.Count)
                {
                    _levelRepository.CurrentPack = 0;
                }
            }

            return _levelRepository.levelPacks[_levelRepository.CurrentPack].levels[_levelRepository.CurrentLevel];
        }

        public Level GetLastLevel()
        {
            return _levelRepository.levelPacks[_levelRepository.CurrentPack].levels[_levelRepository.CurrentLevel];
        }

        public void AddPack(LevelPack levelPack)
        {
            _levelRepository.levelPacks.Add(levelPack);
        }

        public override void OnCreate()
        {
            this._levelRepository = Game.GetRepository<LevelRepository>();
        }

        public override void Initialize()
        {
            LevelManager.instance.Initialize(this);
        }
    }
}
