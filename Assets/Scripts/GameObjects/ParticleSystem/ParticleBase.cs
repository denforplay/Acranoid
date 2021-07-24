using Assets.Scripts.Abstracts.Pool.Interfaces;
using UnityEngine;

namespace Assets.Scripts.GameObjects.ParticleSystem
{
    public abstract class ParticleBase : MonoBehaviour, IPoolable
    {
        [SerializeField] UnityEngine.ParticleSystem _particlePrefab;
        public UnityEngine.ParticleSystem ParticlePrefab => _particlePrefab;
        public IObjectPool Origin { get; set; }

        private void OnEnable()
        {
            _particlePrefab.Play();
        }

        private void OnBecameInvisible()
        {
            ParticleManager.GetInstance.ReturnParticle(this);
        }


        public void ReturnToPool()
        {
            _particlePrefab.Stop();
            gameObject.SetActive(false);
        }
    }
}
