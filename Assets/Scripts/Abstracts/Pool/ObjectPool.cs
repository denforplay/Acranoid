using Assets.Scripts.Abstracts.Pool.Interfaces;
using System;
using System.Collections.Concurrent;
using UnityEngine;
using System.Linq;
using Assets.Scripts.BlockSystem.FactoryPattern;

namespace Assets.Scripts.Abstracts.Pool
{
    public class ObjectPool<T> : IObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        private readonly ConcurrentBag<T> _container = new ConcurrentBag<T>();
        private IFactory<T> _factory;
        public int ActiveCount => _container.Count(x => x.gameObject.activeSelf == true);
        public int Count => _container.Count;
        public T GetPrefab => _factory.Prefab;
        public ObjectPool(IFactory<T> factory)
        {
            _factory = factory;
        }

        public T GetPrefabInstance()
        {
            T instance = null;
            if (_container.Count > 0 && _container.TryTake(out instance))
            {
                if (instance != null)
                {
                    instance.transform.SetParent(null);
                    instance.gameObject.SetActive(true);    
                }
            }
            else
            {
                instance = _factory.GetNewInstance();
            }
            instance.Origin = this;

            return instance;
        }

        public void ReturnToPool(T instance)
        {
            instance.gameObject.SetActive(false);
            instance.ReturnToPool();
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
