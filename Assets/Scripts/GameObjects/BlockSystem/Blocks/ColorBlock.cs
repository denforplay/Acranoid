using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.BlockEvents;
using Assets.Scripts.GameObjects.Bonus;

namespace Assets.Scripts.Block
{
    public class ColorBlock : BaseBlock, IPoolable
    {
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
    }
}
