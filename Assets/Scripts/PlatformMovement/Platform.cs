using UnityEngine;

namespace Assets.Scripts.PlatformMovement
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private PlatformMoveConfig _platformMoveConfig;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidBody2D;
        private Camera _camera;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
        }

        private void Move()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _platformMoveConfig.direction = mousePos.x > _rigidBody2D.position.x ? _platformMoveConfig.rightDirection : _platformMoveConfig.leftDirection;
            float positionX = _rigidBody2D.position.x + _platformMoveConfig.direction * _platformMoveConfig.speed;
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
