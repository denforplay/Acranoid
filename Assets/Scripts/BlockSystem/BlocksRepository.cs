using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.BlockSystem.FactoryPattern;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Block
{
    public class BlocksRepository : Repository
    {
        public event Action OnBlocksRepoInitialied;
        public List<ObjectPool<BaseBlock>> blocksPools;
        public int Count = 0;
        public override void Initialize()
        {
            BlocksManager.OnBlocksManagerInitializedEvent += InitializePool;
        }

        public void InitializePool()
        {
            blocksPools = new List<ObjectPool<BaseBlock>>();
            blocksPools.Add(new ObjectPool<BaseBlock>(new BlockFactory<BaseBlock>(BlocksManager.instance.colorBlockPrefab, BlocksManager.instance._colorBlockConfig)));
            blocksPools.Add(new ObjectPool<BaseBlock>(new BlockFactory<BaseBlock>(BlocksManager.instance.graniteBlockPrefab, BlocksManager.instance._graniteBlockConfig)));
            OnBlocksRepoInitialied?.Invoke();
        }
    }
}
