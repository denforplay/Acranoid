using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus.Events.BallEvents;
using Assets.Scripts.EventBus.Events.PlatformEvents;
using Assets.Scripts.PlatformMovement;
using UnityEngine;

namespace Assets.Scripts.UserInput
{
    public class UserInput : MonoBehaviour
    {
        [SerializeField] public Rigidbody2D _rigidBody2D;
        [SerializeField] public Camera _camera;
        [SerializeField] private PlatformMoveConfig _platformMoveConfig;
        private const int LEFT_MOUSE_BUTTON = 0;
        private bool _isActive;
        private bool _onMouseDown = false;

        private void Update()
        {
            CheckInputForBall();
            GetInputForPlatform();
        }

        public void CheckInputForBall()
        {
            if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON) && !_isActive)
            {
                _isActive = true;
                EventBusManager.GetInstance.Invoke(new OnBallActivatingEvent());
            }
        }

        public void GetInputForPlatform()
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
                EventBusManager.GetInstance.Invoke<OnPlatformMovingEvent>(new OnPlatformMovingEvent());
            }
        }

        public void SetFalseActive(IEvent ievent)
        {
            _isActive = false;
        }

        public void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnBallInactivatingEvent>(SetFalseActive);
            EventBusManager.GetInstance.Subscribe<OnBallReturnEvent>(SetFalseActive);
        }

        public void OnDestroy()
        {
            EventBusManager.GetInstance.Unsubscribe<OnBallInactivatingEvent>(SetFalseActive);
            EventBusManager.GetInstance.Unsubscribe<OnBallReturnEvent>(SetFalseActive);
        }
    }
}
