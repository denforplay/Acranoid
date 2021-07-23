using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.EventBus.Events.HealthEvents;

namespace Assets.Scripts.Health
{
    public class HealthViewManager : MonoBehaviour
    {
        [SerializeField] private GameObject _healthContent;
        private HealthRepository _healthRepository;
        private List<Heart> _heartsView;
        private int _health;

        private void Awake()
        {
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

        public void Initialize()
        {
            _heartsView = new List<Heart>();
        }

        public void DeleteAllHearts()
        {
            foreach (var heart in _heartsView)
            {
                _healthRepository._heartPool.ReturnToPool(heart);
            }
            _heartsView.Clear();
        }

        public void DeleteHealthView(IEvent ievent)
        {
            try
            {
                _heartsView.Last(x => x != null && x.gameObject.activeInHierarchy).ReturnToPool();
            }
            catch
            {
            }
        }

        public void AddHealthView(object sender, IEvent ievent)
        {
            Heart heart = _healthRepository._heartPool.GetPrefabInstance();
            try
            {
                heart.transform.SetParent(_healthContent.transform);
            }
            catch
            {
                _healthContent = GameObject.Find("HealthContent");
                heart.transform.SetParent(_healthContent.transform);
            }
            heart.gameObject.transform.position = _healthContent.transform.position;
            _heartsView.Add(heart);
        }

        public void ViewHearts()
        {
            if (_healthContent != null)
                for (int i = 0; i < HealthManager.GetInstance.Health; i++)
                {
                    Heart heart = _healthRepository._heartPool.GetPrefabInstance();
                    if (heart != null)
                    {
                        heart.transform.SetParent(_healthContent.transform);
                        heart.gameObject.transform.position = _healthContent.transform.position;
                        _heartsView.Add(heart);
                    }
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
                EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>((OnNextLevelLoaded) =>
                {
                    _health = HealthManager.GetInstance.Health;
                    DeleteAllHearts();
                    ViewHearts();
                });
            });

        }
    }
}
