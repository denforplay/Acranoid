
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
            LevelManager.OnLevelCompeted += base.ReturnToPool;
        }
    }
}
 