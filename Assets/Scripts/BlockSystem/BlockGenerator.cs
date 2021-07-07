using UnityEngine;
using Assets.Scripts.Level;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Abstracts.EventBus.Interfaces;

namespace Assets.Scripts.Block
{
    public class BlockGenerator : MonoBehaviour
    {
        private float horizontalDistance = (float)Screen.width / 1080f * 135f;
        private float verticalDistance = (float)Screen.height / 1920f * 45f;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void ShowBlocks(IEvent ievent)
        {
            Level.Level level = LevelManager.GetInstance.GetCurrentLevel();
            float startHorizontal = 100;
            float horizontal = 100;
            float vertical = Screen.height - 280;
            float scaler = (Screen.width - 2 * startHorizontal) / (horizontalDistance * level.blocksCountInRow);
            horizontalDistance *= scaler;
            startHorizontal += horizontalDistance / 2;
            horizontal += horizontalDistance/2;
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
                    var scale = block.gameObject.transform.localScale;
                    block.gameObject.transform.localScale = new Vector3(scaler, scale.y, scale.z);
                    block.transform.position = _camera.ScreenToWorldPoint(new Vector3(horizontal, vertical, _camera.nearClipPlane));
                    horizontal += horizontalDistance;
                }
                vertical -= verticalDistance;
                horizontal = startHorizontal;
            }
            horizontalDistance /= scaler;
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
