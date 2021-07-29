
using Assets.Scripts.GameObjects.BallMovement;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class BallRageBonus : BaseBonus
    {
        [SerializeField] private int _ballsLayer;
        [SerializeField] private int _blocksLayer;
        [SerializeField] private Color _rageColor;
        [SerializeField] private Color _previousColor;
        public override void Apply()
        {
            var allBals = BallManager.GetInstance.AllBalls;
            foreach(var ball in allBals)
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
