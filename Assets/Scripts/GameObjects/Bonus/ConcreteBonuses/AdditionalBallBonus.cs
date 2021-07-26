using Assets.Scripts.BallMovement;
using Assets.Scripts.GameObjects.BallMovement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class AdditionalBallBonus : BaseBonus
    {
        private void Awake()
        {
            isInstantlyActivated = true;
        }

        public override void Apply()
        {
            Ball ball = BallManager.GetInstance.SpawnBall(this);
            ball.transform.position = BonusManager.GetInstance.CurrentDestroyedBlock.transform.position;
        }

        public override void Remove()
        {
        }
    }
}
