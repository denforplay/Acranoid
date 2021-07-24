using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.EventBus.Events.HealthEvents;
using Assets.Scripts.Abstracts.Singeton;

namespace Assets.Scripts.Health
{
    public class HealthViewManager : Singleton<HealthViewManager>
    {
        [SerializeField] private GameObject _healthContent;
        private HealthRepository _healthRepository;
        private List<Heart> _heartsView;
        private int _health;

        private new void Awake()
        {
            IsDestroy = true;
            base.Awake();
            _heartsView = new List<Heart>();
            EventBusManager.GetInstance.Subscribe<OnHeartSpendEvent>(DeleteHealthView);
            EventBusManager.GetInstance.Subscribe<OnHeartAddEvent>((OnHeartAddEvent) => AddHealthView(this, OnHeartAddEvent));
            EventBusManager.GetInstance.Subscribe<OnHeathInitizliedEvent>((OnHeathInitizliedEvent) =>
            {
                EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>((OnNextLevelLoaded) =>
                {
                    _health = HealthManager.GetInstance.Health;
                    DeleteAllHearts();
                    ViewHearts();
                });
            });

            _healthRepository = Game.GetRepository<HealthRepository>();
        }

        public void DeleteHealthView(IEvent ievent)
        {
            var heart = _heartsView.Last(x => x != null && x.gameObject.activeInHierarchy);
            if (heart != null)
            heart.ReturnToPool();
        }

        public void DeleteAllHearts()
        {
            foreach (var heart in _heartsView)
            {
                heart.ReturnToPool();
            }
            _heartsView.Clear();
        }

        public void AddHealthView(object sender, IEvent ievent)
        {
            if (_healthContent != null)
            {
                Heart heart = _healthRepository._heartPool.GetPrefabInstance();
                heart.transform.SetParent(_healthContent.transform);
                heart.gameObject.transform.position = _healthContent.transform.position;
                _heartsView.Add(heart);
            }
        }

        public void ViewHearts()
        {
            for (int i = 0; i < _health; i++)
            {
                if (_healthContent != null)
                AddHealthView(this, null);
            }
        }

        private void OnDestroy()
        {
            _heartsView.Clear();
            _healthRepository.Initialize();
            EventBusManager.GetInstance.Unsubscribe<OnHeartSpendEvent>(DeleteHealthView);
            EventBusManager.GetInstance.Unsubscribe<OnHeartAddEvent>((OnHeartAddEvent) => AddHealthView(this, OnHeartAddEvent));
            EventBusManager.GetInstance.Unsubscribe<OnHeathInitizliedEvent>((OnHeathInitizliedEvent) =>
            {
                EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>((OnNextLevelLoaded) =>
                {
                    _health = HealthManager.GetInstance.Health;
                    DeleteAllHearts();
                    ViewHearts();
                });
            });

            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>((OnNextLevelLoaded) =>
            {
                _health = HealthManager.GetInstance.Health;
                ViewHearts();
            });
        }
    }
}
