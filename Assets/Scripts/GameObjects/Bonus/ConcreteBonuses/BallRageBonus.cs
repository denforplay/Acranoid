using Assets.Scripts.GameObjects.BallMovement;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    class BallRageBonus : BaseBonus
    {
        public override void Apply()
        {
            foreach (var ball in BallManager.GetInstance._allBalls)
            {

            }
        }

        public override void Remove()
        {
            throw new System.NotImplementedException();
        }
    }
}
