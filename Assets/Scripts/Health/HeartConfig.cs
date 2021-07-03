using UnityEngine;
using Assets.Scripts.PlatformMovement;

namespace Assets.Scripts.Health
{
    [CreateAssetMenu]
    public class HeartConfig : ScriptableObject
    {
        public float borderPosition;
        public GameObject heartPrefab;
        public Sprite activeHeart;
        public Sprite inActiveHeart;
        public float heartSize;
    }
}
