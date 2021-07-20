using Assets.Scripts.Health;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class LifeBonus : BaseBonus
    {
        [SerializeField] private bool _isAdd;
        public override void Apply()
        {
            if (_isAdd)
                HealthManager.GetInstance.AddHeart(1);
            else
                HealthManager.GetInstance.SpendHeart(1);
            gameObject.SetActive(false);
        }

        public override void Remove()
        {
        }
    }
}
