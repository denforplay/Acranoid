using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level
{
    [CreateAssetMenu]
    public class LevelPackObject : ScriptableObject
    {
        public string packName;
        public List<TextAsset> _jsonLevelsFiles;
    }
}
