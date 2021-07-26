using Assets.Scripts.BallMovement;
using Assets.Scripts.GameObjects.BallMovement;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class BallSpeedBonus : BaseBonus
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
            foreach (var ball in BallManager.GetInstance.AllBalls)
            ball.ChangeVelocity(speed);
        }
    }
}
