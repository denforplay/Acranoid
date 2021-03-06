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

        public override void ApplyDamage(int value)
        {
            _life -= value;
            if (_life < 1)
            {
                EventBusManager.GetInstance.Invoke<OnBlockDestroyEvent>(new OnBlockDestroyEvent());
                SpawnParticle();
                GenerateBonus();
            }
            else
            {
                _spriteRenderer.sprite = _sprites[_life - 1];
            }
        }

        public override void ReturnToPool()
        {
            if (_life >= 1 && HealthManager.GetInstance.Health > 0 && !LevelManager.GetInstance.IsLevelRestarted)
            {
                _life = 0;
                EventBusManager.GetInstance.Invoke<OnBlockDestroyEvent>(new OnBlockDestroyEvent());
                SpawnParticle();
                GenerateBonus();
            }
                _baseBonus = null;
            base.ReturnToPool();
        }

        private void SpawnParticle()
        {
            var particle = ParticleManager.GetInstance.GetParticle<BlockDestroyParticle>();
            particle.transform.position = this.transform.position;
            particle.ParticlePrefab.startColor = color;
        }

        private void GenerateBonus()
        {
            if (_baseBonus != null)
            {
                BonusManager.GetInstance.GenerateBonus(this, _baseBonus);
            }
            BlocksManager.GetInstance.ReturnBlock(this);
        }
    }
}
