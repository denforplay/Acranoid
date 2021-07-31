using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using System;

namespace Assets.Scripts.EnergySystem.Timer
{
    public class TimerController : Controller
    {
        private TimerRepository _timerRepository;
        public DateTime NextEnergyTime => _timerRepository.nextEnergyTime;
        public DateTime LastAddedTime => _timerRepository.lastAddedTime;
        public override void OnCreate()
        {
            _timerRepository = Game.GetRepository<TimerRepository>();
            _timerRepository.Initialize();
        }
        public override void Initialize()
        {
            TimerManager.GetInstance.Initialize(this);
        }

        public void SetNextEnergyTime(DateTime value)
        {
            _timerRepository.nextEnergyTime = value;
            _timerRepository.Save();
        }
        public void SetLastAddedTime(DateTime value)
        {
            _timerRepository.lastAddedTime = value;
            _timerRepository.Save();
        }

        public void Save()
        {
            _timerRepository.Save();
        }
    }
}
