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
 