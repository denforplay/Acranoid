using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.BlockEvents;
using Assets.Scripts.GameObjects.Bonus;
using Assets.Scripts.GameObjects.ParticleSystem;
using Assets.Scripts.GameObjects.ParticleSystem.ConcreteParticles;
using Assets.Scripts.Health;
using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class ColorBlock : BaseBlock
    {
        public BaseBonus _baseBonus;
        public Color color;
        [SerializeField] ParticleSystem _destroyParticle;

        private int bonusUsage = 0;
        public override void ApplyDamage(int value)
        {
            _life -= value;
            if (_life < 1)
            {
                EventBusManager.GetInstance.Invoke<OnBlockDestroyEvent>(new OnBlockDestroyEvent());
                var particle = ParticleManager.GetInstance.GetParticle<BlockDestroyParticle>();
                particle.transform.position = this.transform.position;
                particle.ParticlePrefab.startColor = color;
                if (_baseBonus != null)
                {
                    BonusManager.GetInstance.GenerateBonus(this, _baseBonus);
                }
                BlocksManager.GetInstance.ReturnBlock(this);
            }
            else
            {
                _spriteRenderer.sprite = _sprites[_life - 1];
            }
        }

        public override void ReturnToPool()
        {
            _baseBonus = null;
            base.ReturnToPool();
        }
    }
}
