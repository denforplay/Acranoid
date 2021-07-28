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
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.GameObjects.Bonus;
using Assets.Scripts.Abstracts.Singeton;

namespace Assets.Scripts.Block
{
    public class BlockGenerator : Singleton<BlockGenerator>
    {
        private const int EMPTY_BLOCK = -2;
        private const int NEW_ROW = -1;
        private const int GRANITE_BLOCK = 0;
        private const int COLOR_BLOCK = 1;
        [SerializeField] List<BaseBlock> _blocksPrefabs;
        [SerializeField] List<BlockConfig> _blockConfigs;
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
        BoxCollider2D boxCollider2D;
        private new void Awake()
        {
            boxCollider2D = _blocksPrefabs[0].GetComponent<BoxCollider2D>();
            IsDestroy = true;
            base.Awake();
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ShowBlocks);
        }

        private void Start()
        {
            _camera = Camera.main;
                screen = new Vector2(Screen.width, Screen.height);
            screen = _camera.ScreenToWorldPoint(screen);
            distance.x = boxCollider2D.size.x;
            distance.y = boxCollider2D.size.y;
            startPosition = new Vector2(-screen.x, screen.y - _topBorderRender.size.y - _blockRender.size.y / 2);
        }

        public void ShowBlocks(IEvent ievent)
        {
            if (!initialized)
            {
                initialized = true;
                EventBusManager.GetInstance.Invoke<OnNextLevelLoadedEvent>(new OnNextLevelLoadedEvent());
                return;
            }
            Level.Level level = LevelManager.GetInstance.GetCurrentLevel();
            int[] blocksData = level.blocksData;
            InitializeData(blocksData);
            for (int i = 0; i < blocksData.Length; i++)
            {
                int blockIndex = blocksData[i];
                if (blockIndex >= 10)
                    blockIndex /= 10;
                switch (blockIndex)
                {
                    case EMPTY_BLOCK:
                        {
                            position.x += distance.x;
                            continue;
                        }
                    case NEW_ROW:
                        {
                            CalculateDataForNewRow(blocksData, i);
                            BlocksManager.GetInstance.SetNewRow();
                            continue;
                        }
                    default:
                        {
                            BaseBlock block;
                            if (blocksData[i] != GRANITE_BLOCK)
                            {
                                block = BlocksManager.GetInstance.GetBlock(_blocksPrefabs[COLOR_BLOCK]);
                                ConfigureBlock(block as ColorBlock, blocksData[i]);
                            }
                            else
                            {
                                block = BlocksManager.GetInstance.GetBlock(_blocksPrefabs[GRANITE_BLOCK]);
                            }

                            var scale = block.transform.localScale;
                            block.transform.localScale = new Vector3(scaler, scale.y, scale.z);

                            if (_camera != null)
                                block.transform.position = new Vector3(position.x, position.y, _camera.nearClipPlane);

                            position.x += distance.x;
                        }
                        break;
                }
            }

            startPosition.x -= distance.x / 2;
            distance.x /= scaler;
        }

        private void ConfigureBlock(ColorBlock block, int blockData)
        {
            int count = 1;
            int index = -1;
            while (blockData >= 10)
            {
                if (index == -1) index = 0;
                index += blockData % 10 * count;
                count *= 10;
                blockData /= 10;
            }
            if (index != -1)
            {
                block._baseBonus = BonusManager.GetInstance.GetBonus(index);
                block._spriteOnBlock.sprite = block._baseBonus.BonusOnBlockImage;
            }
            block.SetData(_blockConfigs[blockData - 1]);
            (block as ColorBlock).color = _blockConfigs[blockData - 1].baseColor;
        }

        private void InitializeData(int[] blocksData)
        {
            position.x = startPosition.x;
            position.y = startPosition.y;
            countInRow = blocksData.TakeWhile(x => x != NEW_ROW).Count();
            var blockSizeX = (screen.x * 2) / countInRow;
            scaler = blockSizeX / boxCollider2D.size.x;
            distance.x *= scaler;
            startPosition.x += distance.x / 2;
            position.x = startPosition.x;
        }

        private void CalculateDataForNewRow(int[] blocksData, int skipIndex)
        {
            startPosition.x -= distance.x / 2;
            position.x -= distance.x / 2;
            distance.x /= scaler;
            countInRow = blocksData.Skip(skipIndex + 1).TakeWhile(x => x != NEW_ROW).Count();
            if (countInRow != 0)
            {
                var blockSizeX = (screen.x * 2) / countInRow;
                scaler = blockSizeX / boxCollider2D.size.x;
            }
            
            distance.x *= scaler;
            startPosition.x += distance.x / 2;
            position.y -= distance.y;
            position.x = startPosition.x;
        }

        private void OnDestroy()
        {
            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>(ShowBlocks);
        }
    }
}
