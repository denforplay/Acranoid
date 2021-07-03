using UnityEngine;
using System;

namespace Assets.Scripts.PlatformMovement
{
    public class PlatformInput
    {
        private PlatformMoveConfig _platformMoveConfig;
        private const int LEFT_MOUSE_BUTTON = 0;
        private bool _onMouseDown = false;
        public static event Action OnMove;

        public Rigidbody2D _rigidBody2D;
        public Camera _camera;

        public PlatformInput(PlatformMoveConfig platformMoveConfig, Rigidbody2D rigidbody2D, Camera camera)
        {
            _platformMoveConfig = platformMoveConfig;
            _camera = camera;
            _rigidBody2D = rigidbody2D;
        }

        public void GetInput()
        {
            if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
            {
                _onMouseDown = true;
            }
            else if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON))
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
