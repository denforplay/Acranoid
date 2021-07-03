using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.Abstracts.Repository;
using System.Collections.Generic;

namespace Assets.Scripts.Block
{
    public class BlocksRepository : Repository
    {
        public List<Block> gameBlocks;
        public ObjectPool<Block> blocksPool;
        public override void Initialize()
        {
            gameBlocks = new List<Block>();
            blocksPool = new ObjectPool<Block>(new DefaultObjectCreator<Block>());
        }

        public override void Save()
        {

        }
    }
}
