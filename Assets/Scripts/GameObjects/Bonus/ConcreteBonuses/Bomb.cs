using Assets.Scripts.Block;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.GameObjects.ParticleSystem;
using Assets.Scripts.GameObjects.ParticleSystem.ConcreteParticles;
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
                        routine = Coroutines.Coroutines.StartRoutine(DestroyLine(row, col));
                        break;
                    }
                case BombType.Round:
                    {
                        routine = Coroutines.Coroutines.StartRoutine(DestroyBlocksAround(row, col));
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
            Vector2[] moveDirections =
            {
                new Vector2(-1, 1),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(0, 1),
                new Vector2(1, -1),
                new Vector2(0, -1),
                new Vector2(-1, -1),
                new Vector2(-1, 0),
            };
            for (int i = 0; i < moveDirections.Length; i++)
            {
                int nextX = (int)(moveDirections[i].x + row);
                int nextY = (int)(moveDirections[i].y + col);
                Vector2 nextPoint = new Vector2(nextX, nextY);
                if (nextX >= 0 && nextX < BlocksManager.GetInstance.allBlocks.Count && nextY >= 0 && nextY < BlocksManager.GetInstance.allBlocks[nextX].Count)
                    if (BlocksManager.GetInstance.allBlocks[nextX][nextY] != null
                        && BlocksManager.GetInstance.allBlocks[nextX][nextY].gameObject.activeInHierarchy)
                    {
                        SpawnParticle(nextX, nextY);
                        BlocksManager.GetInstance.allBlocks[nextX][nextY].ApplyDamage(_damage);
                        yield return new WaitForSeconds(_timeBetweenDestroy);
                    }
            }
        }

        public IEnumerator DestroyLine(int row, int col)
        {
            Vector2 startPoint = new Vector2(row, col);
            Queue<Vector2> nextPointQueue = new Queue<Vector2>();
            Vector2[] moveDirections =
            {
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
                            && BlocksManager.GetInstance.allBlocks[nextX][nextY].gameObject.activeInHierarchy)
                        {
                            SpawnParticle(nextX, nextY);
                            BlocksManager.GetInstance.allBlocks[nextX][nextY].ApplyDamage(_damage);
                            yield return new WaitForSeconds(_timeBetweenDestroy);
                            nextPointQueue.Enqueue(nextPoint);
                        }
                }
            }
        }

        private IEnumerator DestroyColumn(int row, int col)
        {
            Vector2 startPoint = new Vector2(row, col);
            Queue<Vector2> nextPointQueue = new Queue<Vector2>();
            Vector2[] moveDirections =
{
                new Vector2(0, -1),
                new Vector2(0, 1),
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
                            && BlocksManager.GetInstance.allBlocks[nextX][nextY].gameObject.activeInHierarchy)
                        {
                            SpawnParticle(nextX, nextY);
                            BlocksManager.GetInstance.allBlocks[nextX][nextY].ApplyDamage(_damage);
                            yield return new WaitForSeconds(_timeBetweenDestroy);
                            nextPointQueue.Enqueue(new Vector2(nextX, nextY));
                        }
                }
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
                            SpawnParticle(nextX, nextY);
                            BlocksManager.GetInstance.allBlocks[nextX][nextY].ApplyDamage(_damage);
                            yield return new WaitForSeconds(_timeBetweenDestroy);
                            nextPointQueue.Enqueue(new Vector2(nextX, nextY));
                        }
                }
            }
        }

        private void SpawnParticle(int nextX, int nextY)
        {
            var bombEffect = ParticleManager.GetInstance.GetParticle<BombParticle>();
            bombEffect.transform.position = BlocksManager.GetInstance.allBlocks[nextX][nextY].transform.position;
        }
        public override void Remove()
        {
        }
    }
}
