using UnityEngine;
using Assets.Scripts.PlatformMovement;
using Assets.Scripts.Block;
using System;

namespace Assets.Scripts.BallMovement
{
    public class BallCollisions : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private BallConfig _ballConfig;
        public void MoveCollision(Collision2D collision)
        {
            Vector2 ballPos = transform.position;
            if (collision.gameObject.TryGetComponent(out PlatformInput platorm))
            {
                Vector2 platformPos = platorm.gameObject.transform.position;
                float distanceFromCenter = platformPos.x - ballPos.x;
                float direction = ballPos.x > platformPos.x ? 1f : -1f;
                _rigidBody2D.velocity = Vector2.zero;
                _rigidBody2D.AddForce(new Vector2(direction * Math.Abs(distanceFromCenter * (_ballConfig.force)), _ballConfig.force));
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            MoveCollision(collision);
            if (collision.gameObject.TryGetComponent(out Block.Block block))
            {
                block.ApplyDamage();
            }
        }
    }
}
