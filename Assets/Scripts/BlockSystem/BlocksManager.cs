
using Assets.Scripts.Abstracts.Singeton;
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class BlocksManager : Singleton<BlocksManager>
    {
        public static event Action OnBlocksManagerInitializedEvent;
        public ColorBlock colorBlockPrefab;
        public GraniteBlock graniteBlockPrefab;
        public BlockConfig _colorBlockConfig;
        public BlockConfig _graniteBlockConfig;
        private BlocksController _blocksController;
        private bool _isInitialized;
        public void Initialize(BlocksController blocksController)
        {
            _blocksController = blocksController;
            _isInitialized = true;
            OnBlocksManagerInitializedEvent?.Invoke();
        }

        public BaseBlock GetBlock(BaseBlock baseBlock)
        {
            CheckInitialize();
            BaseBlock block = _blocksController.GetBlock(baseBlock);
            return block;
        }

        public void ReturnBlock(ColorBlock block)
        {
            CheckInitialize();
            _blocksController.ReturnBlock(block);
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
