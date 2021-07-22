using Assets.Scripts.BallMovement;
using UnityEngine;
using Assets.Scripts.Abstracts.Pool;
using System.Collections.Generic;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Assets.Scripts.Health;

namespace Assets.Scripts.GameObjects.BallMovement
{
    public class BallManager : Singleton<BallManager>
    {
        [SerializeField] private Ball _ballPrefab;
        private ObjectPool<Ball> _ballsPool;
        public List<Ball> _allBalls;

        private new void Awake()
        {
            base.Awake();
            _ballsPool = new ObjectPool<Ball>(new BallFactory(_ballPrefab));
            _allBalls = new List<Ball>();
        }

        public Ball SpawnBall()
        {
            Ball ball = _ballsPool.GetPrefabInstance();
            ball.Initialize();
            _allBalls.Add(ball);
            return ball;
        }

        public void ReturnBall(Ball ball)
        {
            if (ball.gameObject.activeInHierarchy)
            {
                _allBalls.Remove(ball);
                _ballsPool.ReturnToPool(ball);
                if (_allBalls.Count == 0)
                {
                    HealthManager.GetInstance.SpendHeart(1);
                }
            }
        }
        
        public void ReturnAllBalls(IEvent ievent)
        {
            var allBals = new List<Ball>(_allBalls);
            foreach (var ball in allBals)
            {
                _ballsPool.ReturnToPool(ball);
            }

            _allBalls.Clear();
        }

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnLevelCompletedEvent>(ReturnAllBalls);
        }
    }
}
