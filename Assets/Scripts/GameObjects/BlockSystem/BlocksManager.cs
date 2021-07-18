using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus.Events.LevelEvents;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Block
{
    public class BlocksManager : Singleton<BlocksManager>
    {
        public ColorBlock colorBlockPrefab;
        public GraniteBlock graniteBlockPrefab;
        public BlockConfig _colorBlockConfig;
        public BlockConfig _graniteBlockConfig;
        private BlocksController _blocksController;
        private bool _isInitialized;
        public List<BaseBlock> allBlocks;
        public void Initialize(BlocksController blocksController)
        {
            allBlocks = new List<BaseBlock>();
            _blocksController = blocksController;
            _isInitialized = true;
            EventBusManager.GetInstance.Subscribe<OnLevelCompletedEvent>(ReturnAllBlocks);
            EventBusManager.GetInstance.Invoke<OnBlocksManagerInitializedEvent>(new OnBlocksManagerInitializedEvent());
        }

        public BaseBlock GetBlock(BaseBlock baseBlock)
        {
            CheckInitialize();
            BaseBlock block = _blocksController.GetBlock(baseBlock);
            allBlocks.Add(block);
            return block;
        }

        public void ReturnBlock(BaseBlock block)
        {
            CheckInitialize();
            _blocksController.ReturnBlock(block);
        }

        public void ReturnAllBlocks(IEvent ievent)
        {
            if (allBlocks != null)
            foreach (var block in allBlocks)
            {
                if (block != null && block.gameObject.activeInHierarchy)
                {
                    block.ReturnToPool();
                }
            }

            _blocksController.ReturnAllBlocks();
        }
        private void CheckInitialize()
        {
            if (!_isInitialized)
            {
                throw new ArgumentNullException("Block manager not initialized yet");
            }
        }
    }
}
