using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.BlockSystem.FactoryPattern;
using Assets.Scripts.GameObjects.BlockSystem.FactoryPattern;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.UI.PopupSystem
{
    public class PopupPool<T> : IObjectPool<T> where T : Popup, IPoolable
    {
        private readonly ConcurrentBag<T> _container = new ConcurrentBag<T>();
        private PopupFactory<T> _factory;
        public PopupPool(PopupFactory<T> factory)
        {
            _factory = factory;
        }

        public T GetPrefabInstance(Type type)
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
                _factory.PopupType = type;
                instance = _factory.GetNewInstance();
            }
            instance.Origin = this;

            return instance;
        }

        public void ReturnToPool(T instance)
        {
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

        public T GetPrefabInstance()
        {
            throw new Exception();
        }
    }
}
