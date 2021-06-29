using Assets.Scripts.Abstracts.Repository;
using UnityEngine.Pool;
using UnityEngine;
namespace Assets.Scripts.View
{
    public class ViewRepository : Repository
    {
        public string PathToCanvas { get; set; }
        ObjectPool<GameObject> _viewPool;
        public override void Initialize()
        {
        }

        public override void Save()
        {
        }
    }
}
