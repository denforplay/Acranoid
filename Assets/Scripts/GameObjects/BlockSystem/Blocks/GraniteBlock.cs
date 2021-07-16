
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Assets.Scripts.Level;

namespace Assets.Scripts.Block
{
    public class GraniteBlock : BaseBlock
    {
        public override void ApplyDamage()
        {
        }

        public override void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
 