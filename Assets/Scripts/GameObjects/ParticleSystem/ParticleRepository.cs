
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus.Events.ParticleEvent;
using Assets.Scripts.GameObjects.ParticleSystem.ConcreteParticles;
using System.Collections.Generic;

namespace Assets.Scripts.GameObjects.ParticleSystem
{
    public class ParticleRepository : Repository
    {
        public List<ObjectPool<ParticleBase>> blocksPools;
        private bool isInitialized;
        public override void Initialize()
        {
            EventBusManager.GetInstance.Subscribe<OnParticleManagerInitializedEvent>(InitializePool);
        }

        public void InitializePool(IEvent ievent)
        {
            if (!isInitialized)
            {
                isInitialized = true;
                InitializePools();
            }

        }

        public void InitializePools()
        {
            blocksPools = new List<ObjectPool<ParticleBase>>();
            foreach (var particle in ParticleManager.GetInstance.ParticleConfig.particles)
            {
                blocksPools.Add(new ObjectPool<ParticleBase>(new ParticleFactory(particle)));
            }
        }
    }
}
