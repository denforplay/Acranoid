using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Abstracts.Singeton;
using System;

namespace Assets.Scripts.EventBus
{
    public class EventBusManager : Singleton<EventBusManager>
    {
        private IEventBus _eventBus;
        private new void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Initialize()
        {
            _eventBus = new Abstracts.EventBus.EventBus();
        }
        public void Subscribe<T>(Action<T> subscriber) where T : IEvent
        {
            GetInstance._eventBus.Subscribe<T>(subscriber);
        }

        public void Unsubscribe<T>(Action<T> subscriber) where T : IEvent
        {
            GetInstance._eventBus.Unsubscibe<T>(subscriber);
        }

        public void Invoke<T>(T invokator) where T : IEvent
        {
            GetInstance._eventBus.Invoke(invokator);
        }
    }
}
