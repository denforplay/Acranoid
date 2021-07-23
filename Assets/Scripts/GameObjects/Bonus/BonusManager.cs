using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.Block;
using Assets.Scripts.PlatformMovement;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus
{
    public class BonusManager : Singleton<BonusManager>
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private List<BaseBonus> _baseBonuses;
        private ColorBlock _currentDestroyedBlock;
        public ColorBlock CurrentDestroyedBlock => _currentDestroyedBlock;
        public Platform Platform => _platform;
        public List<BaseBonus> allBonuses;
        private new void Awake()
        {
            IsDestroy = true;
            allBonuses = new List<BaseBonus>();
            base.Awake();
        }

        public BaseBonus GetBonus(int index)
        {
            allBonuses.Add(_baseBonuses[index]);
            return _baseBonuses[index];
        }

        public void ReturnBonus(BaseBonus baseBonus)
        {
            allBonuses.Remove(baseBonus);
        }

        public void ReturnAllBonuses(BaseBonus baseBonus)
        {
            allBonuses.Clear();
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