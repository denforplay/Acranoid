using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Energy;
using System;
using System.Collections;

namespace Assets.Scripts.EnergySystem
{
    public class EnergyController : Controller
    {
        public EnergyRepository _energyRepository;
        public bool isAdding = false;
        public bool restoring = false;
        public int TotalEnergy => _energyRepository._totalEnergy;
        public DateTime NextEnergyTime => _energyRepository._nextEnergyTime;
        public override void OnCreate()
        {
            _energyRepository = new EnergyRepository();
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
                DateTime counter = _energyRepository._nextEnergyTime;
                isAdding = false;
                while (currentTime > counter)
                {
                    if (_energyRepository._totalEnergy < EnergyManager.GetInstance.MaxEnergy)
                    {
                        isAdding = true;
                        _energyRepository._totalEnergy++;
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
                    _energyRepository._nextEnergyTime = counter;
                }

                EventBusManager.GetInstance.Invoke<OnRestoringEnergyEvent>(new OnRestoringEnergyEvent());
                _energyRepository.Save();
                yield return null;
            }
            restoring = false;
        }

        public void SpendEnergy()
        {
            if (_energyRepository._totalEnergy == 0)
            {
                return;
            }

            _energyRepository._totalEnergy--;
            EventBusManager.GetInstance.Invoke<OnEnergySpendEvent>(new OnEnergySpendEvent());
            if (!restoring)
            {
                if (_energyRepository._totalEnergy + 1 == EnergyManager.GetInstance.MaxEnergy)
                {
                    _energyRepository._nextEnergyTime = EnergyManager.GetInstance.AddDuration(_energyRepository._nextEnergyTime, EnergyManager.GetInstance.RestoreDuration);
                }
                Coroutines.Coroutines.StartRoutine(RestoreRoutine());
            }

        }
    }
}
