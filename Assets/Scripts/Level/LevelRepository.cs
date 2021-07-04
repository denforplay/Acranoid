using Assets.Scripts.Abstracts.Repository;
using System.Collections.Generic;

namespace Assets.Scripts.Level
{
    public class LevelRepository : Repository
    {
        public List<LevelPack> levelPacks { get; private set; }
        private int _currentLevel = 0;
        private int _currentPack = 0;
        public int CurrentLevel => _currentLevel;
        public int CurrentPack => _currentPack;
        public override void Initialize()
        {
            levelPacks = new List<LevelPack>();
        }
    }
}
