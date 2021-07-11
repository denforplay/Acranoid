using System;
using UnityEngine;

namespace Assets.Scripts.Level
{
    [Serializable]
    public class Level
    {
        [NonSerialized] public TextAsset textAsset;
        public bool isCompleted;
        public string levelName;
        public int[] blocksData;
        public int lifes;
    }
}
