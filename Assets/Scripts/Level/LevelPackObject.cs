using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Level
{
    [CreateAssetMenu]
    public class LevelPackObject : ScriptableObject
    {
        public Sprite _packImage;
        public string packName;
        public List<TextAsset> _jsonLevelsFiles;
    }
}
