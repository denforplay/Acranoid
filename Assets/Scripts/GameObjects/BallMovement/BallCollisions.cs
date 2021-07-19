using UnityEngine;
using Assets.Scripts.PlatformMovement;
using Assets.Scripts.Block;
using System;
using Assets.Scripts.GameObjects.Bonus;

namespace Assets.Scripts.BallMovement
{
    public class BallCollisions
    {
        private Rigidbody2D _rigidBody2D;
        private BallConfig _ballConfig;

        public BallCollisions(BallConfig ballConfig, Rigidbody2D rigidBody2D)
        {
            _ballConfig = ballConfig;
            _rigidBody2D = rigidBody2D;
        }

        public void Call(Collision2D collision)
        {
            MoveCollision(collision);
            BlockDamageCollision(collision);
        }

        private void MoveCollision(Collision2D collision)
        {
            Vector2 ballPos = _rigidBody2D.transform.position;
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {
                Vector2 platformPos = platform.gameObject.transform.position;
                float distanceFromCenter = platformPos.x - ballPos.x;
                float direction = ballPos.x > platformPos.x ? 1f : -1f;
                _rigidBody2D.velocity = Vector2.zero;
                _rigidBody2D.velocity = (new Vector2(direction * Math.Abs(distanceFromCenter * (_ballConfig.velocity)), _ballConfig.velocity));
            }
        }

        private void BlockDamageCollision(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BaseBlock block))
            {
                block.ApplyDamage();
            }
        }
    }
}
