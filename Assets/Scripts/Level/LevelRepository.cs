using Assets.Scripts.Abstracts.Repository;
using System.Collections.Generic;

namespace Assets.Scripts.Level
{
    public class LevelRepository : Repository
    {
        public List<LevelPack> levelPacks { get; private set; }
        public int CurrentLevel { get; set; }
        public int CurrentPack { get; set; }
        public override void Initialize()
        {
            levelPacks = new List<LevelPack>();
        }
    }
}
