using UnityEngine;

namespace Assets.Scripts.BallMovement
{
    [CreateAssetMenu]
    public class BallConfig : ScriptableObject
    {
        public float velocity = 5f;
        public float offSetX = 100f;
        public float rightDirection = 1f;
        public float leftDirection = -1;
        public float additionVelocity = 0.1f;
        public float maximumVelocity = 12f;
    }
}
