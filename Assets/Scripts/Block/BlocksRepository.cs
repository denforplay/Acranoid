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
        public List<Block> gameBlocks;
        public ObjectPool<Block> blocksPool;

        public override void Initialize()
        {
            gameBlocks = new List<Block>();
            BlocksManager.OnBlocksManagerInitializedEvent += InitializePool;
        }

        public void InitializePool()
        {
            blocksPool = new ObjectPool<Block>(BlocksManager.instance.blockPrefab);
            OnBlocksRepoInitialied?.Invoke();
        }
    }
}
