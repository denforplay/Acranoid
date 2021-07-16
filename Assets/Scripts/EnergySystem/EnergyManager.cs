using Assets.Scripts.PlayerData;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Energy;
using Assets.Scripts.Abstracts.EventBus.Interfaces;

namespace Assets.Scripts.EnergySystem
{
    public class EnergyManager : Singleton<EnergyManager>
    {
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private int _maxEnergy;


        private EnergyController _energyController;
      
        private int _restoreDuration = 30;

        private void NewStart()
        {
            _energyController._energyRepository.Initialize();
            EventBusManager.GetInstance.Invoke<OnEnergyManagerStartEvent>(new OnEnergyManagerStartEvent());
        }

        public int RestoreDuration => _restoreDuration;
        public int MaxEnergy => _maxEnergy;

        public void Initialize(EnergyController energyController)
        {
            _energyController = energyController;
            EventBusManager.GetInstance.Subscribe<OnEnergySpendEvent>(UpdateEnergy);
            EventBusManager.GetInstance.Subscribe<OnRestoringEnergyEvent>((OnRestoringEnergyEvent) =>
            {
                UpdateEnergy(OnRestoringEnergyEvent);
                UpdateTimer(OnRestoringEnergyEvent);
            });
            NewStart();
        }

        public DateTime AddDuration(DateTime time, int duration)
        {
            return time.AddSeconds(duration);
        }

        private void UpdateTimer(IEvent ievent)
        {
            if (_energyController.TotalEnergy >= _maxEnergy)
            {
                _timerText.text = "Full";
                return;
            }

            TimeSpan timeSpan = _energyController.NextEnergyTime - DateTime.Now;
            string value = String.Format("{0}:{1:D2}:{2:D2}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
            if (_timerText.text != null)
            _timerText.text = value;
        }

        private void UpdateEnergy(IEvent ievent)
        {
            if (_energyText.text != null)
            _energyText.text = _energyController.TotalEnergy.ToString();
        }
    }
}
