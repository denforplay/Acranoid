using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.Block;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
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
            return _baseBonuses[index];
        }

        public void AddDroppedBonus(BaseBonus bonus)
        {
            _baseBonuses.Add(bonus);
        }

        public void ReturnBonus(BaseBonus baseBonus)
        {
            Destroy(baseBonus.gameObject);
            allBonuses.Remove(baseBonus);
        }

        public void ReturnAllBonuses(IEvent ievent)
        {
            foreach (var bonus in allBonuses)
            {
                Destroy(bonus.gameObject);
            }

            allBonuses.Clear();
        }

        public void GenerateBonus(ColorBlock colorBlock, BaseBonus bonus)
        {
            _currentDestroyedBlock = colorBlock;
            if (!bonus.isInstantlyActivated)
            {
                BaseBonus spawnedBonus = Instantiate(bonus);
                allBonuses.Add(spawnedBonus);
                spawnedBonus.transform.position = colorBlock.transform.position;
            }
            else
            {
                bonus.Apply();
            }
        }

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ReturnAllBonuses);
        }

        private void OnDisable()
        {
            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>(ReturnAllBonuses);
        }
    }
}