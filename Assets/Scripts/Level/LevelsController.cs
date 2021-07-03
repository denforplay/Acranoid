using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.Level
{
    public class LevelsController : Controller
    {
        private LevelRepository _levelRepository;
        public int CurrentLevel => _levelRepository.CurrentLevel;

        public int GetCurrentLevelLifes()
        {
            return GetLastLevel().lifes;
        }

        public Level GetLastLevel()
        {
            return _levelRepository._levels[CurrentLevel];
        }

        public void AddLevel(Level level)
        {
            _levelRepository._levels.Add(level);
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
