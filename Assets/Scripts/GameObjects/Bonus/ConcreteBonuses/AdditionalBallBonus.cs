using Assets.Scripts.BallMovement;
using Assets.Scripts.GameObjects.BallMovement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class AdditionalBallBonus : BaseBonus
    {
        public override void Apply()
        {
            List<Ball> allBalls = new List<Ball>(BallManager.GetInstance._allBalls);
            foreach (var ball in allBalls)
            {
                var ballClone = BallManager.GetInstance.SpawnBall();
                ballClone.transform.position = ball.transform.position + Vector3.one;
                ballClone.isReturning = false;
                ballClone.SetVelocity(8f);
                ballClone.BallActivate(null);
            }

            gameObject.SetActive(false);
        }

        public override void Remove()
        {
        }
    }
}
