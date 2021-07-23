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
        public Ball ball;

        private new void Awake()
        {
            base.Awake();
            _ballsPool = new ObjectPool<Ball>(new BallFactory(_ballPrefab));
        }

        public Ball SpawnBall()
        {
            if (ball == null)
            {
                ball = _ballsPool.GetPrefabInstance();
                ball.Initialize();
            }
            return ball;
        }

    }
}
