using UnityEngine;

namespace Assets.Scripts.PlatformMovement
{
    public class PlatformMove : MonoBehaviour
    {
        private const int LEFT_MOUSE_BUTTON = 0;

        private UnityEngine.Camera _camera;
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private float _mouseSensivity = 0.1f;
        private float _speed = 0.15f;
        private float _direction = 0f;
        private bool _onMouseDown = false;
        private float _borderPosition = 3f;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
            {
                _onMouseDown = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _onMouseDown = false;
            }

            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            if (_onMouseDown && Mathf.Abs(mousePos.x - _rigidbody2D.position.x) > _mouseSensivity)
            {
                Move();
            }
        }

        private void Move()
        {
            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            _direction = mousePos.x > _rigidbody2D.position.x ? 1f : -1f;
            float positionX = _rigidbody2D.position.x + _direction * _speed;
            positionX = Mathf.Clamp(positionX, -_borderPosition + (_spriteRenderer.size.x / 2), _borderPosition - (_spriteRenderer.size.x / 2));
            _rigidbody2D.MovePosition(new Vector2(positionX, _rigidbody2D.position.y));
        }
    }
}
