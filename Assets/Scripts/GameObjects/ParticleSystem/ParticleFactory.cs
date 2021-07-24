using Assets.Scripts.BlockSystem.FactoryPattern;
using UnityEngine;

namespace Assets.Scripts.GameObjects.ParticleSystem
{
    public class ParticleFactory : IFactory<ParticleBase>
    {
        public ParticleBase Prefab { get; set; }
        public ParticleFactory(ParticleBase particleBase)
        {
            Prefab = particleBase;
        }

        public ParticleBase GetNewInstance()
        {
            ParticleBase particle = GameObject.Instantiate(Prefab);
            return particle;
        }
    }
}
