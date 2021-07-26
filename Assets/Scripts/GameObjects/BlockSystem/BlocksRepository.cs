using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.BlockSystem.FactoryPattern;
using System.Collections.Generic;
using Assets.Scripts.Abstracts.EventBus.Interfaces;

namespace Assets.Scripts.Block
{
    public class BlocksRepository : Repository
    {
        public List<ObjectPool<BaseBlock>> blocksPools;
        public int Count = 0;
        private bool isInitialized;
        public override void Initialize()
        {
        }

        public void InitializePools()
        {
            blocksPools = new List<ObjectPool<BaseBlock>>();
            blocksPools.Add(new ObjectPool<BaseBlock>(new BlockFactory<BaseBlock>(BlocksManager.GetInstance.colorBlockPrefab)));
            blocksPools.Add(new ObjectPool<BaseBlock>(new BlockFactory<BaseBlock>(BlocksManager.GetInstance.graniteBlockPrefab)));
        }
    }
}
