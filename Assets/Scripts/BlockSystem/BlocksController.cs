using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Level;

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

        public void ReturnBlock(BaseBlock block)
        {
            _blocksRepository.Count--;
            _blocksRepository.blocksPool.ReturnToPool(block);
            if (_blocksRepository.Count == 0)
            {
                LevelManager.instance.LoadNextLevel();
            }
        }

        public BaseBlock GetBlock()
        {
            _blocksRepository.Count++;
            return _blocksRepository.blocksPool.GetPrefabInstance();
        }

        public override void Initialize()
        {
            BlocksManager.instance.Initialize(this);
        }
    }
}
