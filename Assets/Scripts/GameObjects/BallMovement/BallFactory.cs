using Assets.Scripts.BallMovement;
using Assets.Scripts.BlockSystem.FactoryPattern;
using UnityEngine;

namespace Assets.Scripts.GameObjects.BallMovement
{
    public class BallFactory : IFactory<Ball>
    {
        public Ball Prefab { get; set; }

        public BallFactory (Ball prefab)
        {
            Prefab = prefab;
        }

        public Ball GetNewInstance()
        {
            return GameObject.Instantiate(Prefab);
        }
    }
}
