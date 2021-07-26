using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus.Events.LevelEvents;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Block
{
    public class BlocksManager : Singleton<BlocksManager>
    {
        [SerializeField] private GameObject _blocksGO;
        public ColorBlock colorBlockPrefab;
        public GraniteBlock graniteBlockPrefab;
        public BlockConfig _colorBlockConfig;
        public BlockConfig _graniteBlockConfig;
        private BlocksController _blocksController;
        public static bool _isInitialized;
        public List<List<BaseBlock>> allBlocks;
        private int _currentRow = 0;

        private new void Awake()
        {
            IsDestroy = true;
            base.Awake();
            allBlocks = new List<List<BaseBlock>>();
        }
        public void Initialize(BlocksController blocksController)
        {
            _blocksController = blocksController;
            EventBusManager.GetInstance.Subscribe<OnLevelCompletedEvent>(ReturnAllBlocks);
            _isInitialized = true;
            EventBusManager.GetInstance.Invoke<OnBlocksManagerInitializedEvent>(new OnBlocksManagerInitializedEvent());
        }

        public void SetNewRow()
        {
            _currentRow++;
        }

        public BaseBlock GetBlock(BaseBlock baseBlock)
        {
            CheckInitialize();
            List<BaseBlock> currentList;
            if (allBlocks.Count <= _currentRow)
            {
                currentList = new List<BaseBlock>();
                allBlocks.Add(currentList);
            }
            BaseBlock block = _blocksController.GetBlock(baseBlock);
            if (block.transform.parent == null)
            {
                block.transform.SetParent(_blocksGO.transform);
            }

            allBlocks[_currentRow].Add(block);
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
                throw new ArgumentNullException("Block manager not initialized yet" + this.GetType());
            }
        }

        private void OnDestroy()
        {
            Debug.LogError(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            Debug.Log("Block manager destroyed");
        }
    }
}
