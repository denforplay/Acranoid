using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.BallMovement;
using Assets.Scripts.Block;
using Assets.Scripts.PlatformMovement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus
{
    public class BonusManager : Singleton<BonusManager>
    {
        [SerializeField] private Ball _ball;
        [SerializeField] private Platform _platform;
        [SerializeField] private List<BaseBonus> _baseBonuses;
        private ColorBlock _currentDestroyedBlock;
        public ColorBlock CurrentDestroyedBlock => _currentDestroyedBlock;
        public Ball Ball => _ball;
        public Platform Platform => _platform;
        private new void Awake()
        {
            IsDestroy = true;
            base.Awake();
        }

        public BaseBonus GetBonus(int index)
        {
            return _baseBonuses[index];
        }

        public void GenerateBonus(ColorBlock colorBlock, BaseBonus bonus)
        {
            _currentDestroyedBlock = colorBlock;
            if (!bonus.isInstantlyActivated)
            {
                BaseBonus spawnedBonus = Instantiate(bonus);
                spawnedBonus.transform.position = colorBlock.transform.position;
            }
            else
            {
                bonus.Apply();
            }
        }
    }
}