
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

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnLevelCompletedEvent>((OnLevelCompletedEvent) => base.ReturnToPool());
        }
    }
}
 