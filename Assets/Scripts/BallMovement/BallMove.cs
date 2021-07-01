using System;
using UnityEngine;

namespace Assets.Scripts.BallMovement
{                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
    public class BallMove : MonoBehaviour
    {
        private Rigidbody2D _rigidBody2D;
        private bool _isActive;
        private const float force = 300f;
        private const float offSetX = 100f;

        private void Start()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _rigidBody2D.bodyType = RigidbodyType2D.Kinematic;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isActive)
            {
                BallActivate();
            }
        }

        private void BallActivate()
        {
            _isActive = true;
            transform.SetParent(null);
            _rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidBody2D.AddForce(new Vector2(offSetX, force));

        }
    }
}
