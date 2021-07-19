
using Assets.Scripts.BallMovement;
using Assets.Scripts.PlatformMovement;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class PlatformSpeedBonus : BaseBonus
    {
        [SerializeField] private bool _isIncrease = true;
        [SerializeField] private float _speed = 1f;
        public override void Apply()
        {
            StartTimer();
            SetSpeed(_isIncrease ? _speed : -_speed);
            this.gameObject.SetActive(false);
        }

        public override void Remove()
        {
            SetSpeed(_isIncrease ? -_speed : _speed);
        }

        public void SetSpeed(float speed)
        {
            Platform platform = BonusManager.GetInstance.Platform;
            if (platform.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidBody))
            {
                platform.AddSpeed(speed);
            }

        }
    }
}
