using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level
{
    [CreateAssetMenu]
    public class LevelPacksConfig : ScriptableObject
    {
        public List<LevelPackObject> levelPacks;
    }
}
