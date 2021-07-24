
using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Abstracts.Pool;

namespace Assets.Scripts.GameObjects.ParticleSystem
{
    public class ParticleController : Controller
    {
        private ParticleRepository _particleRepository;

        public override void OnCreate()
        {
            _particleRepository = Game.GetRepository<ParticleRepository>();
        }

        public override void Initialize()
        {
            ParticleManager.GetInstance.Initialize(this);
        }

        public ObjectPool<ParticleBase> FindPool<T>() where T : ParticleBase
        {
            if (_particleRepository.blocksPools == null)
            {
                _particleRepository.InitializePools();
            }
            return _particleRepository.blocksPools.Find(x => x.GetPrefab.GetType() == typeof(T));
        }

        public void ReturnParticle(ParticleBase particleBase)
        {
            if (particleBase.gameObject.activeInHierarchy)
            {
                particleBase.ReturnToPool();
            }

        }

        public ParticleBase GetParticle<T>() where T : ParticleBase
        {
            return FindPool<T>().GetPrefabInstance();
        }
    }
}
