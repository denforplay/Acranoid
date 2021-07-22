using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.EnergySystem.Timer;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Energy;
using Assets.Scripts.Health;
using Assets.Scripts.UI.PopupSystem;
using Assets.Scripts.UI.PopupSystem.ConcretePopups;
using System;
using System.Collections;

namespace Assets.Scripts.EnergySystem.Energy
{
    public class EnergyController : Controller
    {
        public EnergyRepository _energyRepository;
        public bool isAdding = false;
        public bool restoring = false;
        public int TotalEnergy => _energyRepository._totalEnergy;
        public override void OnCreate()
        {
            _energyRepository = new EnergyRepository();
            _energyRepository.Initialize();
        }
        public override void Initialize()
        {
            EnergyManager.GetInstance.Initialize(this);
            Coroutines.Coroutines.StartRoutine(RestoreRoutine());
        }

        private IEnumerator RestoreRoutine()
        {
            EventBusManager.GetInstance.Invoke<OnRestoringEnergyEvent>(new OnRestoringEnergyEvent());
            restoring = true;
            while (_energyRepository._totalEnergy < EnergyManager.GetInstance.MaxEnergy)
            {
                DateTime currentTime = DateTime.Now;
                DateTime counter = TimerManager.GetInstance.NextEnergyTime;
                isAdding = false;
                while (currentTime > counter)
                {
                    if (_energyRepository._totalEnergy < EnergyManager.GetInstance.MaxEnergy)
                    {
                        isAdding = true;
                        _energyRepository._totalEnergy++;
                        EventBusManager.GetInstance.Invoke<OnRestoringEnergyEvent>(new OnRestoringEnergyEvent());
                        _energyRepository.Save();
                        DateTime timeToAdd = _energyRepository._lastAddedTime > counter ? _energyRepository._lastAddedTime : counter;
                        counter = EnergyManager.GetInstance.AddDuration(timeToAdd, EnergyManager.GetInstance.RestoreDuration);
                    }
                    else
                    {
                        break;
                    }
                }

                if (isAdding)
                {
                    _energyRepository._lastAddedTime = DateTime.Now;
                    TimerManager.GetInstance.SetNextEnergyTime(counter);
                }

                EventBusManager.GetInstance.Invoke<OnRestoringEnergyEvent>(new OnRestoringEnergyEvent());
                _energyRepository.Save();
                yield return null;
            }
            restoring = false;
        }

        public void SpendEnergy(int value)
        {
            if (_energyRepository._totalEnergy == 0)
            {
                PopupManager.GetInstance.SpawnPopup<EnergyEndedPopup>();
                return;
            }

            _energyRepository._totalEnergy--;
            EventBusManager.GetInstance.Invoke<OnEnergySpendEvent>(new OnEnergySpendEvent());
            if (!restoring)
            {
                if (_energyRepository._totalEnergy + 1 <= EnergyManager.GetInstance.MaxEnergy)
                {
                    TimerManager.GetInstance.SetNextEnergyTime(EnergyManager.GetInstance.AddDuration(TimerManager.GetInstance.NextEnergyTime, EnergyManager.GetInstance.RestoreDuration));
                }
                Coroutines.Coroutines.StartRoutine(RestoreRoutine());
            }
        }

        public void AddEnergy(int value)
        {
            _energyRepository._totalEnergy++;
            EventBusManager.GetInstance.Invoke<OnEnergySpendEvent>(new OnEnergySpendEvent());
            if (!restoring)
            {
                if (_energyRepository._totalEnergy < EnergyManager.GetInstance.MaxEnergy)
                {
                    TimerManager.GetInstance.SetNextEnergyTime(EnergyManager.GetInstance.AddDuration(TimerManager.GetInstance.NextEnergyTime, EnergyManager.GetInstance.RestoreDuration));
                }
                Coroutines.Coroutines.StartRoutine(RestoreRoutine());
            }
        }

        public void Save()
        {
            _energyRepository.Save();
        }
    }
}
