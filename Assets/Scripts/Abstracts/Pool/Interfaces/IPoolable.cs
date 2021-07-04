
namespace Assets.Scripts.Abstracts.Pool.Interfaces
{
    public interface IPoolable
    {
        IObjectPool Origin { get; set; }
        void Prepare();
        void ReturnToPool();
    }
}
