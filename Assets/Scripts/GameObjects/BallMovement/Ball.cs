using UnityEngine;
using Assets.Scripts.Health;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.EventBus.Events.BallEvents;
using Assets.Scripts.EventBus.Events.LevelEvents;
using System;
using Assets.Scripts.PlatformMovement;
using Assets.Scripts.GameObjects.BallMovement;

namespace Assets.Scripts.BallMovement
{
    public class Ball : MonoBehaviour, IPoolable
    {
        [SerializeField] private BallConfig _ballConfig;
        private BallCollisions _ballCollisions;
        public Rigidbody2D _rigidbody2D;
        private CircleCollider2D _circleCollider2D;
        private Platform _rememberedParent;
        public bool isReturning = true;
        private bool isActivated = false;
        private float velocity;
        public IObjectPool Origin { get; set; }
        public float Velocity => velocity;
        public void Initialize()
        {
            velocity = _ballConfig.velocity;
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            _ballCollisions = new BallCollisions(_ballConfig, _rigidbody2D);
        }

        public void SetParent(Platform go)
        {
            _rememberedParent = go;
        }

        public void SetVelocity(float value)
        {
            velocity = value;
        }

        public void ChangeVelocity(float value)
        {
            velocity += value;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _ballCollisions.Call(collision);
        }

        public void ReturnBallOnPosition(IEvent ievent)
        {
            if (isReturning && _rememberedParent != null && this != null && gameObject.activeInHierarchy != false)
            {
                isActivated = false;
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

        public void BallActivate(IEvent ievent)
        {
            if (!isActivated)
            {
                isActivated = true;
                transform.SetParent(null);
                _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody2D.velocity = new Vector2(_ballConfig.offSetX, _ballConfig.velocity);
            }
        }

        private void BallInactivate()
        {
            if (_rememberedParent != null && this.gameObject.activeInHierarchy)
            {
                isActivated = false;
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.isKinematic = false;
                EventBusManager.GetInstance.Invoke<OnBallInactivatingEvent>(new OnBallInactivatingEvent());
                this.transform.SetParent(_rememberedParent.transform);
            }
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

        private void OnBecameInvisible()
        {
            BallInactivate();
            HealthManager.GetInstance.SpendHeart(1);
        }



        public void ReturnToPool()
        {
        }
    }
}