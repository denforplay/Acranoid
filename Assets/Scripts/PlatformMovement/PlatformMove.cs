using UnityEngine;

namespace Assets.Scripts.PlatformMovement
{
    class PlatformMove : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private float _speed = 0.15f;
        private float direction = 0f;
        private bool OnMouseDown = false;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnMouseDown = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                OnMouseDown = false;
            }

            if (OnMouseDown)
            {
                Move();
            }
        }

        private void Move()
        {
            Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePos.x > _rigidbody2D.position.x ? 1f : -1f;
            _rigidbody2D.MovePosition(new Vector2(_rigidbody2D.position.x + direction * _speed, _rigidbody2D.position.y));
        }
    }
}
