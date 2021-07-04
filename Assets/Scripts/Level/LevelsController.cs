using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.Level
{
    public class LevelsController : Controller
    {
        private LevelRepository _levelRepository;
        public int CurrentLevel => _levelRepository.CurrentLevel;
        public int CurrentPack => _levelRepository.CurrentPack;
        public int GetCurrentLevelLifes()
        {
            return GetLastLevel().lifes;
        }

        public Level GetLastLevel()
        {
            return _levelRepository.levelPacks[CurrentPack].levels[CurrentLevel];
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
