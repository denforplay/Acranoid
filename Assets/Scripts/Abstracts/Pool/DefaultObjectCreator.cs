using Assets.Scripts.Abstracts.Pool.Interfaces;

namespace Assets.Scripts.Abstracts.Pool
{
    public class DefaultObjectCreator<T> : IPoolObjectCreator<T> where T : class, new()
    {
        T IPoolObjectCreator<T>.CreateObject()
        {
            return new T();
        }
    }
}
