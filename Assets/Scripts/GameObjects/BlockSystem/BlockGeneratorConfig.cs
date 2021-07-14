using UnityEngine;

namespace Assets.Scripts.BlockSystem
{
    [CreateAssetMenu]
    public class BlockGeneratorConfig : ScriptableObject
    {
        public float screenWidth;
        public float screenHeight;
        public float blockWidth;
        public float blockHeight;
        public float startX;
        public float startY;
    }
}
