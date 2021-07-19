using Assets.Scripts.Abstracts.EventBus.Interfaces;
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
        [SerializeField] private List<BaseBonus> _bonuses;
        [SerializeField] private float _bonusChance = 1.0f;
        private ColorBlock _currentDestroyedBlock;
        public ColorBlock CurrentDestroyedBlock => _currentDestroyedBlock;
        public Ball Ball => _ball;
        public Platform Platform => _platform;
        private new void Awake()
        {
            IsDestroy = true;
            base.Awake();
        }

        public void GenerateBonus(ColorBlock colorBlock)
        {
            _currentDestroyedBlock = colorBlock;
            float bonusChance = Random.Range(0f, 1f);
            if (bonusChance >= 0 && bonusChance <= _bonusChance)
            {
                BaseBonus bonus = _bonuses[Random.Range(0, _bonuses.Count)];
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
}
