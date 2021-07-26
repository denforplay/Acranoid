using Assets.Scripts.BallMovement;
using UnityEngine;
using Assets.Scripts.Abstracts.Pool;
using System.Collections.Generic;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Assets.Scripts.Health;
using Assets.Scripts.EventBus.Events;
using DG.Tweening;
using Assets.Scripts.PlatformMovement;

namespace Assets.Scripts.GameObjects.BallMovement
{
    public class BallManager : Singleton<BallManager>
    {
        [SerializeField] private Ball _ballPrefab;
        private ObjectPool<Ball> _ballsPool;
        private Ball _activeBall;
        public Ball activeBall
        {
            get
            {
                if (_activeBall == null)
                {
                    _activeBall = SpawnBall(this);
                }

                if (!allBalls.Contains(_activeBall))
                {
                    allBalls.Add(_activeBall);
                }
                return _activeBall;
            }
        }
        private List<Ball> allBalls;
        public List<Ball> AllBalls => allBalls;
        private new void Awake()
        {
            IsDestroy = true;
            allBalls = new List<Ball>();
            base.Awake();
            _ballsPool = new ObjectPool<Ball>(new BallFactory(_ballPrefab));
        }

        public Ball SpawnBall(object sender)
        {
            Ball ball = _ballsPool.GetPrefabInstance();
            ball.Initialize();
            allBalls.Add(ball);
            return ball;
        }

        public void ReturnAllBalls(IEvent ievent)
        {
            var balls = new List<Ball>(allBalls);
            foreach (var ball in balls)
            {
                ball.ReturnToPool();
                allBalls.Remove(ball);
            }
        }

        public void ReturnBall(Ball ball)
        {
            ball.ReturnToPool();
            allBalls.Remove(ball);
            if (allBalls.Count == 0)
            {
                HealthManager.GetInstance.SpendHeart(1);
                return;
            }
        }

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ReturnAllBalls);
        }

        private void OnDestroy()
        {
            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>(ReturnAllBalls);
        }
    }
}
