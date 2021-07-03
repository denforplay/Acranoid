using System;
using UnityEngine;

namespace Assets.Scripts.BallMovement
{
    public class BallInput
    {
        private const int LEFT_MOUSE_BUTTON = 0;
        public static event Action OnBallActivatingEvent;
        private bool _isActive;

        public void CheckInput()
        {
            if (Input.GetMouseButtonUp(LEFT_MOUSE_BUTTON) && !_isActive)
            {
                _isActive = true;
                OnBallActivatingEvent?.Invoke();
            }
        }

        public void SetActive(bool active)
        {
            _isActive = active;
        }
    }
}
