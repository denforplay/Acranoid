using Assets.Scripts.Abstracts.Repository;
using System.Collections.Generic;

namespace Assets.Scripts.Level
{
    public class LevelRepository : Repository
    {
        public List<Level> _levels { get; private set; }
        private int _currentLevel = 0;
        public int CurrentLevel => _currentLevel;

        public override void Initialize()
        {
            _levels = new List<Level>();
        }

        public override void Save()
        {
        }
    }
}
