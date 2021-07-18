using System;
using UnityEngine;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
using UnityEngine.UI;
using Assets.Scripts.UI.PopupSystem;

namespace Assets.Scripts.Health
{
    public class HealthManager : Singleton<HealthManager>
    {
        [SerializeField] private Heart _heartPrefab;
        public bool IsInitialized { get; private set; }
        private HealthController _healthController;

        private new void Awake()
        {
            IsDestroy = true;
            base.Awake();
        }

        public Heart HeartPrefaab
        {
            get
            {
                CheckHeartsInitialized();
                return _heartPrefab;
            }
        }

        public int Health
        {
            get
            {
                CheckHeartsInitialized();
                return _healthController.Health;
            }
        }

        public void SpendHeart(int value)
        {
            CheckHeartsInitialized();
            if (_healthController.IsEnoughLifes(0))
            {
                EventBusManager.GetInstance.Invoke(new OnHeartSpendEvent());
                _healthController.SpendLife(value);
            }
            else
            {
                PopupManager.GetInstance.SpawnPopup<HeartEndsPopup>();
            }
        }

        public void InitializeHealthController(HealthController healthController)
        {
            _healthController = healthController;
            IsInitialized = true;
            _healthController.SetHeartPrefab(_heartPrefab);
            _healthController.InitializeHearts();
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>((OnNextLevelLoaded) =>
            {
                _healthController.InitializeHearts();
            });
            EventBusManager.GetInstance.Invoke(new OnHeathInitizliedEvent());
        }

        private void CheckHeartsInitialized()
        {
            if (!IsInitialized)
            {
                throw new ArgumentNullException("Hearts are not initialized yet");
            }
        }
    }
}
