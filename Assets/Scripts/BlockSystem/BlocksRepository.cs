using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.Abstracts.Repository;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Block
{
    public class BlocksRepository : Repository
    {
        public event Action OnBlocksRepoInitialied;
        public ObjectPool<BaseBlock> blocksPool;
        public int Count = 0;
        public override void Initialize()
        {
            BlocksManager.OnBlocksManagerInitializedEvent += InitializePool;
        }

        public void InitializePool()
        {
            blocksPool = new ObjectPool<BaseBlock>(BlocksManager.instance.blockPrefab);
            OnBlocksRepoInitialied?.Invoke();
        }
    }
}
