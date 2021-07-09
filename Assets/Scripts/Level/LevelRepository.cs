using Assets.Scripts.Abstracts.Repository;
using System.Collections.Generic;

namespace Assets.Scripts.Level
{
    public class LevelRepository : Repository
    {
        public List<List<Level>> levelPacks { get; private set; }
        public Level CurrentLevel { get; set; }
        public List<Level> CurrentPack { get; set; }
        public override void Initialize()
        {
            levelPacks = new List<List<Level>>();
        }
    }
}
