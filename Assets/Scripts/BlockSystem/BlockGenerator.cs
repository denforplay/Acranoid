using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.BlockSystem;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Level;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.Block
{
    public class BlockGenerator : MonoBehaviour
    {
        private const int EMPTY_BLOCK = -2;
        private const int NEW_ROW = -1;
        private const int GRANITE_BLOCK = 0;
        private const int COLOR_BLOCK = 1;
        [SerializeField] List<BaseBlock> _blockConfigs;
        [SerializeField] private BlockGeneratorConfig _blockGeneratorConfig;
        private float horizontalDistance;
        private float verticalDistance;
        private float horizontal;
        private float vertical;
        private float startX;
        private float countInRow;
        private float scaler;
        private bool initialized;
        private Camera _camera;
        private void Start()
        {
            horizontalDistance = (float)Screen.width / _blockGeneratorConfig.screenWidth * _blockGeneratorConfig.blockWidth;
            verticalDistance = (float)Screen.height / _blockGeneratorConfig.screenHeight * _blockGeneratorConfig.blockHeight;
            startX = (float)Screen.width / _blockGeneratorConfig.screenWidth * _blockGeneratorConfig.startX;
            _camera = Camera.main;
        }

        private void ShowBlocks(IEvent ievent)
        {

            if (!initialized)
            {
                EventBusManager.GetInstance.Invoke<OnNextLevelLoadedEvent>(new OnNextLevelLoadedEvent());
                EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ShowBlocks);
                initialized = true;
            }
            Level.Level level = LevelManager.GetInstance.GetCurrentLevel();
            int[] blocksData = level.blocksData;
            InitializeData(blocksData);
            for (int i = 0; i < blocksData.Length; i++)
            {
                switch (blocksData[i])
                {
                    case EMPTY_BLOCK:
                        {
                            horizontal += horizontalDistance;
                            continue;
                        }
                    case NEW_ROW:
                        {
                            CalculateDataForNewRow(blocksData, i);
                            continue;
                        }
                    default:
                        {
                            BaseBlock block = BlocksManager.GetInstance.GetBlock(_blockConfigs[blocksData[i]]);
                            var scale = block.gameObject.transform.localScale;
                            block.gameObject.transform.localScale = new Vector3(scaler, scale.y, scale.z);
                            if (_camera != null)
                            block.transform.position = _camera.ScreenToWorldPoint(new Vector3(horizontal, vertical, _camera.nearClipPlane));
                            horizontal += horizontalDistance;
                        }
                        break;
                }
            }
            startX -= horizontalDistance / 2;
            horizontalDistance /= scaler;
        }

        private void InitializeData(int[] blocksData)
        {
            horizontal = _blockGeneratorConfig.startX;
            vertical = Screen.height - _blockGeneratorConfig.startY;
            countInRow = blocksData.TakeWhile(x => x != NEW_ROW).Count();
            scaler = (Screen.width - 2 * startX) / (horizontalDistance * countInRow);
            horizontalDistance *= scaler;
            startX += horizontalDistance / 2;
            horizontal += horizontalDistance / 2;
        }

        private void CalculateDataForNewRow(int[] blocksData, int skipIndex)
        {
            startX -= horizontalDistance / 2;
            horizontal -= horizontalDistance / 2;
            horizontalDistance /= scaler;
            countInRow = blocksData.Skip(skipIndex + 1).TakeWhile(x => x != NEW_ROW).Count();
            scaler = (Screen.width - 2 * startX) / (horizontalDistance * countInRow);
            horizontalDistance *= scaler;
            startX += horizontalDistance / 2;
            vertical -= verticalDistance;
            horizontal = startX;
        }

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnBlocksRepositoryInitializedEvent>(ShowBlocks);
        }

        private void OnDestroy()
        {
            EventBusManager.GetInstance.Unsubscribe<OnBlocksRepositoryInitializedEvent>(ShowBlocks);
            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>(ShowBlocks);
        }
    }
}
