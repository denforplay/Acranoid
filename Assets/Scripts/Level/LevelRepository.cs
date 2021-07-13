using Assets.Scripts.Abstracts.Repository;
using System.Collections.Generic;

namespace Assets.Scripts.Level
{
    public class LevelRepository : Repository
    {
        public Level CurrentLevel { get; set; }
        public List<Level> CurrentPack { get; set; }
        public override void Initialize()
        {
            CurrentPack = new List<Level>();
        }
    }
}
