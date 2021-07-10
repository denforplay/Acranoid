using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.BlockSystem.FactoryPattern;
using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Health
{
    public class HealthRepository : Repository
    {
        public ObjectPool<Heart> _heartPool;
        public Heart heartPrefab;
        public int Health { get; set; }
        public override void Initialize()
        {
            _heartPool = new ObjectPool<Heart>(new HeartFactory(heartPrefab));
            this.Health = 0;
        }

        public void InitializeHearts()
        {
            this.Health = LevelManager.GetInstance.GetCurrentLevelLifes();
        }
    }
}