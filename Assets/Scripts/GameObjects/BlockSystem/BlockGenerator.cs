using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.BlockSystem;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Level;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using Assets.Scripts.GameObjects.Borders;

namespace Assets.Scripts.Block
{
    public class BlockGenerator : MonoBehaviour
    {
        private const int EMPTY_BLOCK = -2;
        private const int NEW_ROW = -1;
        private const int GRANITE_BLOCK = 0;
        private const int COLOR_BLOCK = 1;
        [SerializeField] List<BaseBlock> _blocksPrefabs;
        [SerializeField] List<BlockConfig> _blockConfigs;
        [SerializeField] private BlockGeneratorConfig _blockGeneratorConfig;
        [SerializeField] private SpriteRenderer _leftBorderRender;
        [SerializeField] private SpriteRenderer _topBorderRender;
        [SerializeField] private SpriteRenderer _blockRender;
        private Vector2 distance = new Vector2();
        private Vector2 position = new Vector2();
        private Vector2 startPosition = new Vector2();
        private Vector2 screen;
        private float countInRow;
        private float scaler;
        private bool initialized;
        private Camera _camera;
        private void Start()
        {
            _camera = Camera.main;
            screen = new Vector2(Screen.width, Screen.height);
            var screenInWorld = _camera.ScreenToWorldPoint(screen);
            distance.x = screen.x / _blockGeneratorConfig.screenWidth * _blockGeneratorConfig.blockWidth;
            distance.y = screen.y / _blockGeneratorConfig.screenHeight * _blockGeneratorConfig.blockHeight;
            startPosition = new Vector2(_leftBorderRender.size.x - screenInWorld.x, screenInWorld.y - _topBorderRender.size.y -_blockRender.size.x/2);
            startPosition = _camera.WorldToScreenPoint(startPosition);
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
                            position.x += distance.x;
                            continue;
                        }
                    case NEW_ROW:
                        {
                            CalculateDataForNewRow(blocksData, i);
                            continue;
                        }
                    default:
                        {
                            BaseBlock block;
                            if (blocksData[i] != GRANITE_BLOCK)
                            {
                                block = BlocksManager.GetInstance.GetBlock(_blocksPrefabs[COLOR_BLOCK]);
                                block.SetData(_blockConfigs[blocksData[i] - 1]);
                            }
                            else
                            {
                                block = BlocksManager.GetInstance.GetBlock(_blocksPrefabs[GRANITE_BLOCK]);
                            }
                            var scale = block.gameObject.transform.localScale;
                            block.gameObject.transform.localScale = new Vector3(scaler, scale.y, scale.z);
                            if (_camera != null)
                                block.transform.position = _camera.ScreenToWorldPoint(new Vector3(position.x, position.y, _camera.nearClipPlane));

                            position.x += distance.x;
                        }
                        break;
                }
            }
            startPosition.x -= distance.x / 2;
            distance /= scaler;
        }

        private void InitializeData(int[] blocksData)
        {
            position.x = startPosition.x;
            position.y = startPosition.y;
            countInRow = blocksData.TakeWhile(x => x != NEW_ROW).Count();
            scaler = (screen.x - 2 * startPosition.x) / (distance.x * countInRow);
            distance *= scaler;
            startPosition.x += distance.x / 2;
            position.x = startPosition.x;
        }

        private void CalculateDataForNewRow(int[] blocksData, int skipIndex)
        {
            startPosition.x -= distance.x / 2;
            position.x -= distance.x / 2;
            distance /= scaler;
            countInRow = blocksData.Skip(skipIndex + 1).TakeWhile(x => x != NEW_ROW).Count();
            if (countInRow != 0)
            scaler = (Screen.width - 2 * startPosition.x) / (distance.x * countInRow);
            distance *= scaler;
            startPosition.x += distance.x / 2;
            position.y -= distance.y;
            position.x = startPosition.x;
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
