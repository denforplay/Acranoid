using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Abstracts.Singeton;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameObjects.ParticleSystem
{
    public class ParticleManager : Singleton<ParticleManager>
    {
        [SerializeField] private GameObject _particleGO;
        [SerializeField] private ParticleConfig _particleConfig;
        private List<ParticleBase> allParticles;
        private ParticleController _particleController;
        private bool _isInitialized;
        public ParticleConfig ParticleConfig => _particleConfig;
        private new void Awake()
        {
            IsDestroy = true;
            allParticles = new List<ParticleBase>();
            base.Awake();
        }

        public void Initialize(ParticleController particleController)
        {
            _particleController = particleController;
            _isInitialized = true;
        }

        public ParticleBase GetParticle<T>() where T : ParticleBase
        {
            CheckInitialize();

            ParticleBase particle = _particleController.GetParticle<T>();
            if (particle.transform.parent == null)
            particle.transform.SetParent(_particleGO.transform);
            allParticles.Add(particle);
            return particle;
        }

        public void ReturnParticle(ParticleBase particleBase)
        {
            CheckInitialize();
            allParticles.Remove(particleBase);
            _particleController.ReturnParticle(particleBase);
        }

        public void ReturnAllParticles(IEvent ievent)
        {
            if (allParticles != null)
                foreach (var particle in allParticles)
                {
                    if (particle != null && particle.gameObject.activeInHierarchy)
                    {
                        particle.ReturnToPool();
                    }
                }

            allParticles.Clear();
        }

        private void CheckInitialize()
        {
            if (!_isInitialized)
            {
                throw new ArgumentNullException("Particle manager not initialized yet");
            }
        }
    }
}
