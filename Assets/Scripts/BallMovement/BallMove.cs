using System;
using UnityEngine;
using Assets.Scripts.PlatformMovement;

namespace Assets.Scripts.BallMovement
{
    public class BallMove : MonoBehaviour
    {
        [SerializeField] BallConfig _ballConfig;
        private Rigidbody2D _rigidBody2D;
        private void Start()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _rigidBody2D.bodyType = RigidbodyType2D.Kinematic;
        }

        private void BallActivate()
        {
            transform.SetParent(null);
            _rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidBody2D.AddForce(new Vector2(_ballConfig.offSetX, _ballConfig.force));
        }

 

        private void OnEnable()
        {
            BallInput.OnBallActivatingEvent += BallActivate;
        }

        private void OnDisable()
        {
            BallInput.OnBallActivatingEvent -= BallActivate;
        }
    }
}
