using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.BallMovement;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus.Events.PlatformEvents;
using Assets.Scripts.GameObjects.BallMovement;
using Assets.Scripts.GameObjects.Bonus;
using UnityEngine;

namespace Assets.Scripts.PlatformMovement
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private PlatformMoveConfig _platformMoveConfig;
        [SerializeField] private SpriteRenderer _borderSpriteRenderer;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidBody2D;
        private Camera _camera;

        private Vector2 _screen;

        public void AddSpeed(float value)
        {
            _platformMoveConfig.speed += value;
        }
        private void Start()
        {
            _screen = new Vector2(Screen.width, Screen.height);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            _screen = _camera.ScreenToWorldPoint(_screen);

        }

        private void Move(IEvent ievent)
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _platformMoveConfig.direction = mousePos.x > _rigidBody2D.position.x ? _platformMoveConfig.rightDirection : _platformMoveConfig.leftDirection;
            float positionX = _rigidBody2D.position.x + _platformMoveConfig.direction * _platformMoveConfig.speed;
            float lefterPosition = -_screen.x + (_spriteRenderer.size.x / 2) + _borderSpriteRenderer.size.x;
            float righterPosition = _screen.x - (_spriteRenderer.size.x / 2) - _borderSpriteRenderer.size.x;
            positionX = Mathf.Clamp(positionX, lefterPosition, righterPosition);
            _rigidBody2D.MovePosition(new Vector2(positionX, _rigidBody2D.position.y));
        }

        private void ReturnBall(IEvent ievent)
        {
            Ball ball = BallManager.GetInstance.activeBall;
            try
            {
                ball.gameObject.SetActive(true);
            }
            catch
            {
            }
            ball.isReturning = true;

            ball.SetParent(this);
            ball.ReturnBallOnPosition(null);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<BaseBonus>(out BaseBonus baseBonus))
            {
                baseBonus.Apply();
            }
        }

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnPlatformMovingEvent>(Move);
            EventBusManager.GetInstance.Subscribe<OnHeartSpendEvent>(ReturnBall);
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ReturnBall);
        }

        private void OnDisable()
        {
            EventBusManager.GetInstance.Unsubscribe<OnPlatformMovingEvent>(Move);
            EventBusManager.GetInstance.Unsubscribe<OnHeartSpendEvent>(ReturnBall);
            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>(ReturnBall);
        }
    }
}
