namespace Assets.Scripts.Block
{
    public class GraniteBlock : BaseBlock
    {
        public override void ApplyDamage(int value)
        {
        }

        public override void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
 