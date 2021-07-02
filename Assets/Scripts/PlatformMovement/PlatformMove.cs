using UnityEngine;

namespace Assets.Scripts.PlatformMovement
{
    public class PlatformMove : MonoBehaviour
    {
        [SerializeField] private PlatformMoveConfig _platformMoveConfig;
        private float _direction = 0f;
        public SpriteRenderer _spriteRenderer { get; private set; }
        public Rigidbody2D _rigidBody2D;
        public Camera _camera;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
        }

        private void Move()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _direction = mousePos.x > _rigidBody2D.position.x ? 1f : -1f;
            float positionX = _rigidBody2D.position.x + _direction * _platformMoveConfig.speed;
            float lefterPosition = -_platformMoveConfig.borderPosition + (_spriteRenderer.size.x / 2);
            float righterPosition = _platformMoveConfig.borderPosition - (_spriteRenderer.size.x / 2);
            positionX = Mathf.Clamp(positionX, lefterPosition, righterPosition);
            _rigidBody2D.MovePosition(new Vector2(positionX, _rigidBody2D.position.y));
        }

        private void OnEnable()
        {
            PlatformInput.OnMove += Move;
        }

        private void OnDisable()
        {
            PlatformInput.OnMove -= Move;
        }
    }
}
