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
        public override void Initialize()
        {
            EventBusManager.GetInstance.Subscribe<OnBlocksManagerInitializedEvent>(InitializePool);
        }

        public void InitializePool(IEvent ievent)
        {
            blocksPools = new List<ObjectPool<BaseBlock>>();
            blocksPools.Add(new ObjectPool<BaseBlock>(new BlockFactory<BaseBlock>(BlocksManager.GetInstance.colorBlockPrefab, BlocksManager.GetInstance._colorBlockConfig)));
            blocksPools.Add(new ObjectPool<BaseBlock>(new BlockFactory<BaseBlock>(BlocksManager.GetInstance.graniteBlockPrefab, BlocksManager.GetInstance._graniteBlockConfig)));
            EventBusManager.GetInstance.Invoke<OnBlocksRepositoryInitializedEvent>(new OnBlocksRepositoryInitializedEvent());
        }
    }
}
