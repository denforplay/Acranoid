using UnityEngine;
using Assets.Scripts.Health;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.EventBus.Events.BallEvents;
using Assets.Scripts.EventBus.Events.LevelEvents;
using System;

namespace Assets.Scripts.BallMovement
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallConfig _ballConfig;
        private BallCollisions _ballCollisions;
        private Rigidbody2D _rigidbody2D;
        private CircleCollider2D _circleCollider2D;
        private GameObject _rememberedParent;
        private Camera _camera;

        public IObjectPool Origin { get; set; }

        private void Start()
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            _ballCollisions = new BallCollisions(_ballConfig, _rigidbody2D);
            _camera = Camera.main;
        }

        public void SetVelocity(float value)
        {
            _ballConfig.velocity += value;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _ballConfig.velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _ballCollisions.Call(collision);
        }

        public void ReturnBallOnPosition(IEvent ievent)
        {
            if (_rememberedParent != null && this != null && gameObject.activeInHierarchy != false)
            {
                EventBusManager.GetInstance.Invoke<OnBallReturnEvent>(new OnBallReturnEvent());
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.isKinematic = true;
                transform.SetParent(_rememberedParent.transform);
                try
                {
                    this.gameObject.SetActive(true);
                }
                catch
                {
                }
                Vector3 positon = _rememberedParent.transform.position;
                transform.position = new Vector3(positon.x, positon.y + _circleCollider2D.radius * 2, positon.z);
            }
        }

        private void BallActivate(IEvent ievent)
        {
            _rememberedParent = transform.parent.gameObject;
            transform.SetParent(null);
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.velocity = new Vector2(_ballConfig.offSetX, _ballConfig.velocity);
        }

        private void BallInactivate()
        {
            if (_rememberedParent != null && this.gameObject.activeInHierarchy)
            {
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.isKinematic = false;
                EventBusManager.GetInstance.Invoke<OnBallInactivatingEvent>(new OnBallInactivatingEvent());
                this.transform.SetParent(_rememberedParent.transform);
                HealthManager.GetInstance.SpendHeart(1);
            }
        }

        private void OnBecameInvisible()
        {
            BallInactivate();
        }

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnBallActivatingEvent>(BallActivate);
            EventBusManager.GetInstance.Subscribe<OnHeathInitizliedEvent>((OnHeathInitizliedEvent) =>
            {
                EventBusManager.GetInstance.Subscribe<OnHeartSpendEvent>(ReturnBallOnPosition);
            });
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ReturnBallOnPosition);
            EventBusManager.GetInstance.Subscribe<OnLevelCompletedEvent>(ReturnBallOnPosition);
        }

        private void OnDestroy()
        {
            _rememberedParent = null;
            EventBusManager.GetInstance.Unsubscribe<OnHeathInitizliedEvent>((OnHeathInitizliedEvent) =>
            {
                EventBusManager.GetInstance.Subscribe<OnHeartSpendEvent>(ReturnBallOnPosition);
            });

            EventBusManager.GetInstance.Unsubscribe<OnBallActivatingEvent>(BallActivate);
            EventBusManager.GetInstance.Unsubscribe<OnHeartSpendEvent>(ReturnBallOnPosition);
            EventBusManager.GetInstance.Unsubscribe<OnLevelCompletedEvent>(ReturnBallOnPosition);
            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>(ReturnBallOnPosition);
        }
    }
}