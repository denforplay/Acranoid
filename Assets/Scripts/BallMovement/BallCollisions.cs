using UnityEngine;
using Assets.Scripts.PlatformMovement;
using Assets.Scripts.Block;
using System;

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
            BlockDamageCollision(collision);
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
