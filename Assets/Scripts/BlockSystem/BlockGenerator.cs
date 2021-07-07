using UnityEngine;
using Assets.Scripts.Level;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Abstracts.EventBus.Interfaces;

namespace Assets.Scripts.Block
{
    public class BlockGenerator : MonoBehaviour
    {
        private float horizontalDistance = (float)Screen.width / 1080f * 150f;
        private float verticalDistance = (float)Screen.height / 1920f * 50f;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void ShowBlocks(IEvent ievent)
        {
            Level.Level level = LevelManager.GetInstance.GetCurrentLevel();
            float horizontal = Screen.width / 2 - level.blocksCountInRow * verticalDistance / 2;
            float vertical = Screen.height / 2 + level.blocksCountInColumn * horizontalDistance / 2;
            for (int i = 1; i <= level.blocksCountInColumn; i++)
            {
                for (int j = 1; j <= level.blocksCountInRow; j++)
                {
                    BaseBlock block;
                    if (level.graniteBlocksCount > 0)
                    {
                        level.graniteBlocksCount--;
                        block = BlocksManager.GetInstance.GetBlock(BlocksManager.GetInstance.graniteBlockPrefab);
                    }
                    else
                    {
                        block = BlocksManager.GetInstance.GetBlock(BlocksManager.GetInstance.colorBlockPrefab);
                    }
                    vertical += verticalDistance;
                    block.transform.position = _camera.ScreenToWorldPoint(new Vector3(horizontal, vertical, _camera.nearClipPlane));
                }
                vertical = Screen.height / 2 + level.blocksCountInColumn * horizontalDistance / 2; ;
                horizontal += horizontalDistance;
            }
        }

        private void OnEnable()
        {
            EventBusManager.OnEventBusManagerInitializedEvent += SubscribeOnNextLevelLoaded;
        }

        private void SubscribeOnNextLevelLoaded()
        {
            EventBusManager.GetInstance.Subscribe<OnBlocksRepositoryInitializedEvent>(ShowBlocks);
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ShowBlocks);
        }

        private void OnDisable()
        {
            EventBusManager.GetInstance.Unsubscribe<OnBlocksRepositoryInitializedEvent>(ShowBlocks);
            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>(ShowBlocks);
        }
    }
}
