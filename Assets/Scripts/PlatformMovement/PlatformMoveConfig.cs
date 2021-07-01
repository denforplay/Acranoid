using UnityEngine;

namespace Assets.Scripts.PlatformMovement
{
    [CreateAssetMenu]
    public class PlatformMoveConfig : ScriptableObject
    {
        public float speed = 0.15f;
        public float borderPosition = 3f;
        public float mouseSensivity = 0.1f;

    }
}
