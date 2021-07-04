﻿using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.Block
{
    public class BlocksController : Controller
    {
        private BlocksRepository _blocksRepository;
        public override void OnCreate()
        {
            base.OnCreate();
            _blocksRepository = Game.GetRepository<BlocksRepository>();
        }

        public void ReturnBlock(Block block)
        {
            _blocksRepository.blocksPool.ReturnToPool(block);
        }

        public Block GetBlock()
        {
            return _blocksRepository.blocksPool.GetPrefabInstance();
        }

        public override void Initialize()
        {
            BlocksManager.instance.Initialize(this);
        }
    }
}
