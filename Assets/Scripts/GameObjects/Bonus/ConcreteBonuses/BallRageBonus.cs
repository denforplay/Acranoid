
using Assets.Scripts.GameObjects.BallMovement;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class BallRageBonus : BaseBonus
    {
        [SerializeField] private Color _rageColor;
        [SerializeField] private Color _previousColor;
        public override void Apply()
        {
            BonusManager.GetInstance.allBonuses.FindAll(x => x.GetType() == typeof(BallRageBonus)).ForEach(ballRage =>
            {
                ballRage.Stop();
            });
            var allBals = BallManager.GetInstance.AllBalls;
            foreach (var ball in allBals)
            {
                ball.SetRageBallState(true);
                ball._ballSprite.color = _rageColor;
            }
            gameObject.SetActive(false);
            StartTimer();
        }

        public override void Remove()
        {
            var allBals = BallManager.GetInstance.AllBalls;
            foreach (var ball in allBals)
            {
                ball.SetRageBallState(false);
                ball._ballSprite.color = _previousColor;
            }
        }
    }
}
