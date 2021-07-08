using UnityEngine;
using System;

namespace Assets.Scripts.PlatformMovement
{
    public class PlatformInput : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D _rigidBody2D;
        [SerializeField] public Camera _camera;
        [SerializeField] private PlatformMoveConfig _platformMoveConfig;
        private const int LEFT_MOUSE_BUTTON = 0;
        private bool _onMouseDown = false;
        public static event Action OnMove;

        private void Update()
        {
            GetInput();
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
