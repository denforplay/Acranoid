using Assets.Scripts.Health;
using UnityEngine;

namespace Assets.Scripts.BlockSystem.FactoryPattern
{
    class HeartFactory : IFactory<Heart>
    {
        public Heart Prefab { get; set; }

        public HeartFactory(Heart prefab)
        {
            Prefab = prefab;
        }

        public Heart GetNewInstance()
        {
            try
            {
                Heart instance = GameObject.Instantiate(Prefab);
                return instance;
            }
            catch
            { }
            return null;
        }
    }
}
