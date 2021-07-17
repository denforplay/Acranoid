
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Energy;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.EnergySystem.Timer
{
    public class TimerManager : Singleton<TimerManager>
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        public TimerController _timerController;
        private bool isInitialized = false;

        public DateTime NextEnergyTime => _timerController != null ? _timerController.NextEnergyTime : DateTime.Now;

        public void SetNextEnergyTime(DateTime value)
        {
            _timerController.SetNextEnergyTime(value);
        }

        private new void Awake()
        {
            IsDestroy = true;
            base.Awake();
        }

        public void Initialize(TimerController timerController)
        {
            _timerController = timerController;
            EventBusManager.GetInstance.Subscribe<OnRestoringEnergyEvent>(UpdateTimer);
            isInitialized = true;
        }

        public DateTime AddDurationInSeconds(DateTime time, int duration)
        {
            return time.AddSeconds(duration);
        }

        private void UpdateTimer(IEvent ievent)
        {
            if (EnergyManager.GetInstance.TotalEnergy >= EnergyManager.GetInstance.MaxEnergy)
            {
                _timerText.text = "Full";
                return;
            }

            TimeSpan timeSpan = _timerController.NextEnergyTime - DateTime.Now;
            string value = String.Format("{0}:{1:D2}:{2:D2}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
            if (_timerText != null)
                _timerText.text = value;
        }

        public void CheckInitialization()
        {
            if (!isInitialized)
            {
                throw new ArgumentException();
            }
        }

        private void OnDestroy()
        {
            EventBusManager.GetInstance.Unsubscribe<OnRestoringEnergyEvent>(UpdateTimer);
        }
    }
}
