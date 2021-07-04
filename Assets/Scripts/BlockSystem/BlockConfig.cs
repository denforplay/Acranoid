using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Block
{
    [CreateAssetMenu]
    public class BlockConfig : ScriptableObject
    {
        public GameObject prefab;
        public List<Sprite> _sprites;
        public Color baseColor;
        public int score;
    }
}
