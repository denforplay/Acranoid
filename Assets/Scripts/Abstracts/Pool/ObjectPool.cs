using Assets.Scripts.Abstracts.Pool.Interfaces;
using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace Assets.Scripts.Abstracts.Pool
{
    public class ObjectPool<T> : IObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        private T _prefab;
        private readonly ConcurrentBag<T> _container = new ConcurrentBag<T>();

        public ObjectPool(T prefab)
        {
            _prefab = prefab;
        }

        public T GetPrefabInstance()
        {
            T instance = null;
            if (_container.Count > 0 && _container.TryTake(out instance))
            {
                instance.transform.SetParent(null);
                instance.gameObject.SetActive(true);
            }
            else
            {
                instance = GameObject.Instantiate(_prefab);

            }
            instance.Origin = this;
            instance.Prepare();

            return instance;
        }

        public void ReturnToPool(T instance)
        {
            instance.gameObject.SetActive(false);

            _container.Add(instance);
        }

        public void ReturnToPool(object instance)
        {
            if (instance is T)
            {
                ReturnToPool(instance as T);
            }
        }
    }
}
