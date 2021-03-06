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
using Assets.Scripts.EventBus.Events.BlockEvents;

namespace Assets.Scripts.BallMovement
{
    public class Ball : MonoBehaviour, IPoolable
    {
        [SerializeField] private BallConfig _ballConfig;
        private BallCollisions _ballCollisions;
        public Rigidbody2D _rigidbody2D;
        [SerializeField] private CircleCollider2D _triggerCircleCollider2D;
        [SerializeField] private CircleCollider2D _collisionCircleCollider2D;
        public SpriteRenderer _ballSprite;
        private Platform _rememberedParent;
        public bool isReturning = false;
        private bool isActivated = false;
        private float velocity;
        private Color _defaultColor = Color.white;
        private bool _isRage;
        public IObjectPool Origin { get; set; }
        public float Velocity => velocity;
        public void Initialize()
        {
            _triggerCircleCollider2D.enabled = false;
            velocity = _ballConfig.velocity;
            _rigidbody2D = GetComponent<Rigidbody2D>();
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
            if (velocity < _ballConfig.maximumVelocity)
                velocity += value;
        }

        private void FixedUpdate()
        {
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _ballCollisions.Call(_collisionCircleCollider2D, collision.collider);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            _ballCollisions.Call(_triggerCircleCollider2D, collider);
        }

        public void SetRageBallState(bool isRage)
        {
            if (!_isRage)
            {
                _ballSprite.color = _defaultColor;
            }
            _triggerCircleCollider2D.enabled = isRage;
        }

        public void ReturnBallOnPosition(IEvent ievent)
        {
            if (isReturning && _rememberedParent != null && this != null && gameObject.activeInHierarchy)
            {
                isActivated = false;
                EventBusManager.GetInstance.Invoke<OnBallReturnEvent>(new OnBallReturnEvent());
                _rigidbody2D.velocity = Vector2.zero;
                _rigidbody2D.isKinematic = true;
                transform.SetParent(_rememberedParent.transform);
                this.gameObject.SetActive(true);

                Vector3 positon = _rememberedParent.transform.position;
                transform.position = new Vector3(positon.x, positon.y + _collisionCircleCollider2D.radius * 2, positon.z);
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
                this.transform.SetParent(_rememberedParent.transform);
            }
        }

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnBlockDestroyEvent>((OnBlockDestroyEvent) => ChangeVelocity(_ballConfig.additionVelocity));
            EventBusManager.GetInstance.Subscribe<OnBallActivatingEvent>(BallActivate);
            EventBusManager.GetInstance.Subscribe<OnHeathInitizliedEvent>((OnHeathInitizliedEvent) =>
            {
                EventBusManager.GetInstance.Subscribe<OnHeartSpendEvent>(ReturnBallOnPosition);
            });
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ReturnBallOnPosition);
            EventBusManager.GetInstance.Subscribe<OnLevelCompletedEvent>(ReturnBallOnPosition);
        }

        private void OnDisable()
        {
            SetVelocity(_ballConfig.velocity);
            Unsubscribe();
        }

        private void OnDestroy()
        {
            _rememberedParent = null;
            Unsubscribe();
        }

        private void Unsubscribe()
        {
            EventBusManager.GetInstance.Unsubscribe<OnBlockDestroyEvent>((OnBlockDestroyEvent) => ChangeVelocity(0.25f));
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
            SetVelocity(_ballConfig.velocity);
            SetRageBallState(false);
            BallInactivate();
            BallManager.GetInstance.ReturnBall(this);
        }

        public void ReturnToPool()
        {
            SetVelocity(_ballConfig.velocity);
            gameObject.SetActive(false);
        }
    }
}