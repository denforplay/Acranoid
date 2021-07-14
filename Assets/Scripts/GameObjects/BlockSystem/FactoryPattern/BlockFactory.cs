using Assets.Scripts.Block;
using UnityEngine;

namespace Assets.Scripts.BlockSystem.FactoryPattern
{
    public class BlockFactory<T> : IFactory<T> where T : BaseBlock
    {
        public T Prefab { get ; set; }

        public BlockFactory(T prefab)
        {
            Prefab = prefab;
        }
        public T GetNewInstance()
        {
            T instance = GameObject.Instantiate(Prefab);
            return instance;
        }
    }
}
