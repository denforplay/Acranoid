using UnityEngine;

namespace Assets.Scripts.BallMovement
{
    [CreateAssetMenu]
    public class BallConfig : ScriptableObject
    {
        public float force = 300f;
        public float offSetX = 100f;
        public float rightDirection = 1f;
        public float leftDirection = -1;
    }
}
