using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.BlockEvents;
using Assets.Scripts.GameObjects.Bonus;
using Assets.Scripts.Health;
using Assets.Scripts.Level;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class ColorBlock : BaseBlock
    {
        [SerializeField] public BaseBonus _baseBonus;
        public Color color;
        [SerializeField] ParticleSystem _destroyParticle;

        private int bonusUsage = 0;
        public override void ApplyDamage(int value)
        {
            _life -= value;
            if (_life < 1)
            {
                var particle = Instantiate(_destroyParticle);
                particle.transform.position = this.transform.position;
                particle.startColor = color;
                Destroy(particle.gameObject, particle.main.duration);

                if (_baseBonus != null)
                {
                    BonusManager.GetInstance.GenerateBonus(this, _baseBonus);
                }
                EventBusManager.GetInstance.Invoke<OnBlockDestroyEvent>(new OnBlockDestroyEvent());
                BlocksManager.GetInstance.ReturnBlock(this);
            }
            else
            {
                _spriteRenderer.sprite = _sprites[_life - 1];
            }
        }

        public override void ReturnToPool()
        {
            if (_life > 0)
            {
                _life = 0;
                if (_baseBonus != null && bonusUsage == 0)
                {
                    bonusUsage++;
                    if (HealthManager.GetInstance.Health > 0 && !LevelManager.GetInstance.IsLevelCompleted)
                    {
                        BonusManager.GetInstance.GenerateBonus(this, _baseBonus);
                    }
                }
            }
            base.ReturnToPool();
        }
    }
}
