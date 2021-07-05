
using System;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class BlocksManager : MonoBehaviour
    {
        public static event Action OnBlocksManagerInitializedEvent;
        public ColorBlock blockPrefab;
        [SerializeField] private BlockConfig _blockConfig;
        public static BlocksManager instance;
        private BlocksController _blocksController;
        private bool _isInitialized;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void Initialize(BlocksController blocksController)
        {
            _blocksController = blocksController;
            _isInitialized = true;
            OnBlocksManagerInitializedEvent?.Invoke();
        }

        public BaseBlock GetBlock()
        {
            CheckInitialize();
            BaseBlock block = _blocksController.GetBlock();
            block.SetData(_blockConfig);
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
