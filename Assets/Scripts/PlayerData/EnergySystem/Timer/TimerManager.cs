
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
        private const string TIMER_FORMAT = "{0}:{1:D2}:{2:D2}";
        private const string TIMER_FULL = "Full";
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private int _restoreDuration = 15;

        public TimerController _timerController;
        private bool isInitialized = false;
        public int RestoreDuration => _restoreDuration;
        public DateTime NextEnergyTime =>  _timerController == null ? DateTime.Now.AddSeconds(_restoreDuration) : _timerController.NextEnergyTime;
        public DateTime LastAddedEnergyTime => _timerController == null ? DateTime.Now : _timerController.LastAddedTime;
        public void SetNextEnergyTime(DateTime value)
        {
            _timerController.SetNextEnergyTime(value);
        }

        public void SetLastAddedEnergyTime(DateTime value)
        {
            _timerController.SetLastAddedTime(value);
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

        public DateTime AddDuration(DateTime time, int duration)
        {
            return time.AddSeconds(duration);
        }

        public DateTime AddDurationInSeconds(DateTime time, int duration)
        {
            return time.AddSeconds(duration);
        }

        public void UpdateTimer(IEvent ievent)
        {
            if (EnergyManager.GetInstance.TotalEnergy >= EnergyManager.GetInstance.MaxEnergy)
            {
                _timerText.text = TIMER_FULL;
                return;
            }

            TimeSpan timeSpan = _timerController.NextEnergyTime - DateTime.Now;
            string value = String.Format(TIMER_FORMAT, (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
            if (_timerText != null)
                _timerText.text = value;
        }

        public void UpdateTime()
        {
            SetLastAddedEnergyTime(DateTime.Now);
            SetNextEnergyTime(DateTime.Now.AddSeconds(_restoreDuration));
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
