﻿using Assets.Scripts.Block;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public enum BombType
    {
        Vertical,
        Horizontal,
        Round,
        ChainBomb
    };
    public class Bomb : BaseBonus
    {
        [SerializeField] private BombType _bombType;
        [SerializeField] private float _timeBetweenDestroy = 0.05f;
        private int _damage = 1;
        public override void Apply()
        {
            var allBlocks = BlocksManager.GetInstance.allBlocks;
            int row, col = 0;
            bool isFinded = false;
            for (row = 0; row < allBlocks.Count; row++)
            {
                for (col = 0; col < allBlocks[row].Count; col++)
                {
                    if (allBlocks[row][col] == BonusManager.GetInstance.CurrentDestroyedBlock)
                    {
                        isFinded = true;
                        break;
                    }
                }
                if (isFinded) break;
            }

            Coroutine routine = null;
            switch (_bombType)
            {
                case BombType.Vertical:
                    {
                        routine = Coroutines.Coroutines.StartRoutine(DestroyColumn(row, col));
                        break;
                    }
                case BombType.Horizontal:
                    {
                        routine = Coroutines.Coroutines.StartRoutine(DestroyLine(row));
                        break;
                    }
                case BombType.Round:
                    {
                        routine = Coroutines.Coroutines.StartRoutine(DestroyBlocksAround(--row, --col));
                        break;
                    }
                case BombType.ChainBomb:
                    {
                        routine = Coroutines.Coroutines.StartRoutine(DestroyChain(row, col));
                        break;
                    }
            }

            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>((OnNextLevelLoadedEvent) => Coroutines.Coroutines.StopRoutine(routine));
        }

        private IEnumerator DestroyBlocksAround(int row, int col)
        {
            for (int i = row; i < row + 3; i++)
            {
                for (int j = col; j < col + 3; j++)
                {
                    try
                    {
                        BlocksManager.GetInstance.ReturnBlock(BlocksManager.GetInstance.allBlocks[i][j]);
                    }
                    catch
                    {
                        continue;
                    }
                    yield return new WaitForSeconds(_timeBetweenDestroy);
                }
            }
        }

        public IEnumerator DestroyLine(int row)
        {
            for (int i = 0; BlocksManager.GetInstance.allBlocks.Count != 0 && i < BlocksManager.GetInstance.allBlocks[row].Count; i++)
            {
                if (BlocksManager.GetInstance.allBlocks[row][i] is ColorBlock && BlocksManager.GetInstance.allBlocks[row][i].gameObject.activeInHierarchy)
                    BlocksManager.GetInstance.ReturnBlock(BlocksManager.GetInstance.allBlocks[row][i]);
                yield return new WaitForSeconds(_timeBetweenDestroy);
            }
        }

        private IEnumerator DestroyColumn(int row, int col)
        {
            for (int i = 0; BlocksManager.GetInstance.allBlocks.Count != 0 && i < BlocksManager.GetInstance.allBlocks.Count; i++)
            {
                int index = BlocksManager.GetInstance.allBlocks[i].Count - BlocksManager.GetInstance.allBlocks[row].Count + col;
                if (index < 0) continue;
                try
                {
                    if (BlocksManager.GetInstance.allBlocks[i][index] is ColorBlock && BlocksManager.GetInstance.allBlocks[i][index].gameObject.activeInHierarchy)
                    {
                        BlocksManager.GetInstance.ReturnBlock(BlocksManager.GetInstance.allBlocks[i][index]);
                    }
                }
                catch
                {
                }

                yield return new WaitForSeconds(_timeBetweenDestroy);
            }
        }

        private IEnumerator DestroyChain(int row, int col)
        {
            Vector2 startPoint = new Vector2(row, col);
            Queue<Vector2> nextPointQueue = new Queue<Vector2>();
            Vector2[] moveDirections =
{
                new Vector2(0, -1),
                new Vector2(0, 1),
                new Vector2(-1, 0),
                new Vector2(1, 0)
            };
            nextPointQueue.Enqueue(startPoint);

            while (nextPointQueue.Count != 0)
            {
                Vector2 currentPoint = nextPointQueue.Dequeue();
                for (int i = 0; i < moveDirections.Length; i++)
                {
                    int nextX = (int)(moveDirections[i].x + currentPoint.x);
                    int nextY = (int)(moveDirections[i].y + currentPoint.y);
                    Vector2 nextPoint = new Vector2(nextX, nextY);
                    if (nextX >= 0 && nextX < BlocksManager.GetInstance.allBlocks.Count && nextY >= 0 && nextY < BlocksManager.GetInstance.allBlocks[nextX].Count)
                        if (BlocksManager.GetInstance.allBlocks[nextX][nextY] != null 
                            && BlocksManager.GetInstance.allBlocks[nextX][nextY].color == BlocksManager.GetInstance.allBlocks[(int)currentPoint.x][(int)currentPoint.y].color
                            && BlocksManager.GetInstance.allBlocks[nextX][nextY].gameObject.activeInHierarchy)
                        {
                            BlocksManager.GetInstance.ReturnBlock(BlocksManager.GetInstance.allBlocks[nextX][nextY]);
                            yield return new WaitForSeconds(_timeBetweenDestroy);
                            nextPointQueue.Enqueue(new Vector2(nextX, nextY));
                        }
                }
            }
        }

        public override void Remove()
        {
        }
    }
}