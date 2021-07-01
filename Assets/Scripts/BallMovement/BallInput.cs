using System;
using UnityEngine;

namespace Assets.Scripts.BallMovement
{
    public class BallInput : MonoBehaviour
    {
        public static event Action OnBallActivatingEvent; 
        private Rigidbody2D _rigidBody2D;
        private bool _isActive;

        private void Start()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isActive)
            {
                _isActive = true;
                OnBallActivatingEvent?.Invoke();
            }
        }
    }
}
