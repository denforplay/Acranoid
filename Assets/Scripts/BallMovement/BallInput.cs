using UnityEngine;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;

namespace Assets.Scripts.BallMovement
{
    public class BallInput
    {
        private const int LEFT_MOUSE_BUTTON = 0;
        private bool _isActive;

        public void CheckInput()
        {
            if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON) && !_isActive)
            {
                _isActive = true;
                EventBusManager.GetInstance.Invoke(new OnBallActivatingEvent());
            }
        }

        public void SetActive(bool active)
        {
            _isActive = active;
        }
    }
}
