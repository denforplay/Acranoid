using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Assets.Scripts.Level;
using Assets.Scripts.PlayerData;
using Assets.Scripts.UI.PopupSystem;

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


        public ObjectPool<BaseBlock> FindPool(BaseBlock baseBlock)
        {
           if (_blocksRepository.blocksPools == null)
            {
                _blocksRepository.InitializePools();
            }
           return _blocksRepository.blocksPools.Find(x => x.GetPrefab.GetType() == baseBlock.GetType());
        }

        public void ReturnBlock(BaseBlock block)
        {
            _blocksRepository.Count--;
            var pool = FindPool(block);
            pool.ReturnToPool(block);
            if (_blocksRepository.Count == 0)
            {
                JsonParser jsonParser = new JsonParser();
                jsonParser.SetJsonData(LevelManager.GetInstance.CurrentLevel);
                EventBusManager.GetInstance.Invoke(new OnLevelCompletedEvent());
                PopupSystem.GetInstance.SpawnPopup(typeof(LevelSuccessPopup));
            }
        }

        public BaseBlock GetBlock(BaseBlock block)
        {
            if (block is ColorBlock)
            {
                _blocksRepository.Count++;
            }

            return FindPool(block).GetPrefabInstance();
        }
        
        public override void Initialize()
        {
            BlocksManager.GetInstance.Initialize(this);
        }
    }
}
