using UnityEngine;
using System;

namespace Assets.Scripts.PlatformMovement
{
    public class PlatformInput : MonoBehaviour
    {
        [SerializeField] private PlatformMoveConfig _platformMoveConfig;
        private const int LEFT_MOUSE_BUTTON = 0;
        private bool _onMouseDown = false;
        public static event Action OnMove;

        public Rigidbody2D _rigidBody2D;
        public UnityEngine.Camera _camera;

        private void Awake()
        {
            _rigidBody2D = GetComponent<Rigidbody2D>();
            _camera = UnityEngine.Camera.main;
        }

        private void Update()
        {
            GetInput();
        }
        private void GetInput()
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

            if (_onMouseDown && Mathf.Abs(mousePos.x - _rigidBody2D.position.x) > _platformMoveConfig.mouseSensivity)
            {
                OnMove?.Invoke();
            }
        }

    }
}
