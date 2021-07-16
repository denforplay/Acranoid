using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.PlayerData;
using System;
using UnityEngine;

namespace Assets.Scripts.EnergySystem
{
    public class EnergyRepository
    {
        private const string TOTAL_ENERGY = "totalEnergy";
        private const string NEXT_ENERGY_TIME = "nextEnergyTime";
        private const string LAST_ADDED_TIME = "lastAddedTime";

        public int _totalEnergy = 0;
        public DateTime _nextEnergyTime;
        public DateTime _lastAddedTime;

        public void Initialize()
        {
            _totalEnergy = PlayerDataManager.GetInstance.GetIntDataForKey(TOTAL_ENERGY);
            DateTime.TryParse(PlayerDataManager.GetInstance.GetStringDataForKey(NEXT_ENERGY_TIME), out _nextEnergyTime);
            DateTime.TryParse(PlayerDataManager.GetInstance.GetStringDataForKey(LAST_ADDED_TIME), out _lastAddedTime);
        }

        public void Save()
        {
            PlayerDataManager.GetInstance.SetIntDataForKey(TOTAL_ENERGY, _totalEnergy);
            PlayerDataManager.GetInstance.SetStringDataForKey(NEXT_ENERGY_TIME, _nextEnergyTime.ToString());
            PlayerDataManager.GetInstance.SetStringDataForKey(LAST_ADDED_TIME, _lastAddedTime.ToString());
        }
    }
}
