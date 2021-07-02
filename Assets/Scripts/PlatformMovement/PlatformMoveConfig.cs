using UnityEngine;

namespace Assets.Scripts.PlatformMovement
{
    [CreateAssetMenu]
    public class PlatformMoveConfig : ScriptableObject
    {
        public float speed = 0.15f;
        public float mouseSensivity = 0.1f;
        public float borderPosition = 3.5f;
    }
}
