using Assets.Scripts.Level;
using System;
using UnityEngine;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
using UnityEngine.UI;

namespace Assets.Scripts.Health
{
    public class HealthManager : Singleton<HealthManager>
    {
        [SerializeField] private Heart _heartPrefab;
        [SerializeField] private GameObject _healthPanel;
        public bool IsInitialized { get; private set; }
        private HealthController _healthController;
        private HealthViewController _healthViewController;

        public Heart HeartPrefaab
        {
            get
            {
                CheckHeartsInitialized();
                return _heartPrefab;
            }
        }

        public GameObject HealthPanel
        {
            get
            {
                CheckHeartsInitialized();
                return _healthPanel;
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


        private void ShowHealth()
        {
            CheckHeartsInitialized();
            _healthViewController.ViewHearts();
        }

        public void SpendHeart(int value)
        {
            CheckHeartsInitialized();
            if (_healthController.IsEnoughLifes(0))
            {
                DeleteHealthView();
                EventBusManager.GetInstance.Invoke(new OnHeartSpendEvent());
                _healthController.SpendLife(value);
            }
            else
            {
            }
        }

        public void InitializeHealthController(HealthController healthController)
        {
            _healthController = healthController;
            IsInitialized = true;
            _healthController.SetHeartPrefab(_heartPrefab);
            EventBusManager.GetInstance.Subscribe<OnHeathInitizliedEvent>((OnHeathInitizliedEvent) => 
            {
                EventBusManager.GetInstance.Subscribe<OnLevelsInitialized>((OnLevelsInitialized) =>
                {
                    _healthController.InitializeHearts();
                    ShowHealth();
                });

                EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>((OnNextLevelLoaded) =>
                {
                    _healthViewController.DeleteAllHearts();
                    _healthController.InitializeHearts();
                    ShowHealth();
                });
            });

            EventBusManager.GetInstance.Invoke(new OnHeathInitizliedEvent());
        }

        public void InitializeViewController(HealthViewController healthViewController)
        {
            _healthViewController = healthViewController;
        }

        public void DeleteHealthView()
        {
            CheckHeartsInitialized();
            _healthViewController.DeleteHealthView();
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
