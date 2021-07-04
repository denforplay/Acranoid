
namespace Assets.Scripts.Block.BlockFactory.BlocksFacility
{
    public abstract class BlockFacility
    {
        public BaseBlock GetBlock(BlockConfig blockConfig)
        {
            BaseBlock block = CreateBlock(blockConfig);
            if (block != null)
            {
                block.SetData(blockConfig);
            }
            return block;
        }

        public abstract BaseBlock CreateBlock(BlockConfig blockConfig);
    }
}
