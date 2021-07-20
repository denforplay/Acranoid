using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.BlockEvents;
using Assets.Scripts.GameObjects.Bonus;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class ColorBlock : BaseBlock, IPoolable
    {
        public Color color;
        [SerializeField] ParticleSystem _destroyParticle;
        public override void ApplyDamage()
        {
            _life--;
            if (_life < 1)
            {
                BonusManager.GetInstance.GenerateBonus(this);
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
            base.ReturnToPool();
            var particle = Instantiate(_destroyParticle);
            particle.transform.position = this.transform.position;
            particle.startColor = color;
            Destroy(particle, particle.main.duration);
        }
    }
}
