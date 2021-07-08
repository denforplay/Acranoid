using UnityEngine;
using System;

namespace Assets.Scripts.PlatformMovement
{
    [CreateAssetMenu]
    public class PlatformMoveConfig : ScriptableObject
    {
        public float speed = 0.15f;
        public float mouseSensivity = 0.1f;
        public float borderPosition = 3.5f;
        [NonSerialized] public float rightDirection = 1f;
        [NonSerialized] public float leftDirection = -1f;
        [NonSerialized] public float direction = 0f;
    }
}
