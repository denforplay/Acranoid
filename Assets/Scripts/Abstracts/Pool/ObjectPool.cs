using Assets.Scripts.Abstracts.Pool.Interfaces;
using System;
using System.Collections.Concurrent;

namespace Assets.Scripts.Abstracts.Pool
{
    public abstract class ObjectPool<T> where T : class, IPoolable
    {
        private readonly ConcurrentBag<T> _container = new ConcurrentBag<T>();
        private readonly IPoolObjectCreator<T> _objectCreator;
        public int Count
        {
            get
            {
                return this._container.Count;
            }
        }

        public ObjectPool(IPoolObjectCreator<T> creator)
        {
            if (creator == null)
            {
                throw new ArgumentNullException("creator can't be null");
            }

            this._objectCreator = creator;
        }

        public T GetObject()
        {
            T obj;
            if (this._container.TryTake(out obj))
            {
                return obj;
            }

            return this._objectCreator.CreateObject();
        }

        public void ReturnObject(ref T obj)
        {
            obj.ResetState();
            this._container.Add(obj);
            obj = null;
        }
    }
}
