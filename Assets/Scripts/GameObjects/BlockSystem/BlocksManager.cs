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
        public List<List<ColorBlock>> allBlocks;
        private int _currentRow = 0;
        public void Initialize(BlocksController blocksController)
        {
            allBlocks = new List<List<ColorBlock>>();
            _blocksController = blocksController;
            _isInitialized = true;
            EventBusManager.GetInstance.Subscribe<OnLevelCompletedEvent>(ReturnAllBlocks);
            EventBusManager.GetInstance.Invoke<OnBlocksManagerInitializedEvent>(new OnBlocksManagerInitializedEvent());
        }

        public void SetNewRow()
        {
            _currentRow++;
        }

        public BaseBlock GetBlock(BaseBlock baseBlock)
        {
            CheckInitialize();
            List<ColorBlock> currentList;
            if (allBlocks.Count <= _currentRow)
            {
                currentList = new List<ColorBlock>();
                allBlocks.Add(currentList);
            }
            BaseBlock block = _blocksController.GetBlock(baseBlock);
            if (block is ColorBlock)
            allBlocks[_currentRow].Add(block as ColorBlock);
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
                foreach (var row in allBlocks)
                {
                    foreach (var block in row)
                    {
                        if (block != null && block.gameObject.activeInHierarchy)
                        {
                            block.ReturnToPool();
                        }
                    }

                }
            foreach(var blocks in allBlocks)
            {
                blocks.Clear();
            }
            allBlocks.Clear();
            _currentRow = 0;
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
