using Assets.Scripts.Block;
using UnityEngine;

namespace Assets.Scripts.BlockSystem.FactoryPattern
{
    public class BlockFactory<T> : IFactory<T> where T : BaseBlock
    {
        private BlockConfig _blockConfig;

        public T Prefab { get ; set; }

        public BlockFactory(T prefab, BlockConfig blockConfig)
        {
            Prefab = prefab;
            _blockConfig = blockConfig;
        }
        public T GetNewInstance()
        {
            T instance = GameObject.Instantiate(Prefab);
            instance.SetData(_blockConfig);
            return instance;
        }
    }
}
