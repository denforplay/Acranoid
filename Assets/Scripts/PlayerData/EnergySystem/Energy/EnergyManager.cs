using System;
using UnityEngine;
using TMPro;
using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Energy;
using Assets.Scripts.Abstracts.EventBus.Interfaces;

namespace Assets.Scripts.EnergySystem.Energy
{
    public class EnergyManager : Singleton<EnergyManager>
    {
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private int _maxEnergy;

        private EnergyController _energyController;

        public int TotalEnergy => _energyController.TotalEnergy;
        public int MaxEnergy => _maxEnergy;

        private new void Awake()
        {
            IsDestroy = true;
            base.Awake();
        }
        private void NewStart()
        {
            _energyController._energyRepository.Initialize();
            EventBusManager.GetInstance.Invoke<OnEnergyManagerStartEvent>(new OnEnergyManagerStartEvent());
        }

        public void Initialize(EnergyController energyController)
        {
            _energyController = energyController;
            EventBusManager.GetInstance.Subscribe<OnEnergySpendEvent>(UpdateEnergy);
            EventBusManager.GetInstance.Subscribe<OnRestoringEnergyEvent>(UpdateEnergy);
            NewStart();
        }

        public void SpendEnergy(int value)
        {
            _energyController.SpendEnergy(value);
        }

        public void AddEnergy(int value)
        {
            _energyController.AddEnergy(value);
        }


        private void UpdateEnergy(IEvent ievent)
        {
            _energyText.text = $"{_energyController.TotalEnergy}/{_maxEnergy}";
        }

        private void OnDestroy()
        {
            if (_energyController != null)
            _energyController.Save();
            EventBusManager.GetInstance.Unsubscribe<OnEnergySpendEvent>(UpdateEnergy);
            EventBusManager.GetInstance.Unsubscribe<OnRestoringEnergyEvent>(UpdateEnergy);
        }
    }
}
