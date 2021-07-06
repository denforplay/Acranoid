using System;

namespace Assets.Scripts.Level
{
    [Serializable]
    public class Level
    {
        public string LevelName;
        public int graniteBlocksCount;
        public int blocksCountInRow;
        public int blocksCountInColumn;
        public int lifes;
    }
}
