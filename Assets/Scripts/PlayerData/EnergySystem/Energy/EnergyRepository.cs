using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.PlayerData;
using System;

namespace Assets.Scripts.EnergySystem.Energy
{
    public class EnergyRepository
    {
        private const string TOTAL_ENERGY = "totalEnergy";
        private const string LAST_ADDED_TIME = "lastAddedTime";

        public DateTime _lastAddedTime;
        public int _totalEnergy = 0;

        public void Initialize()
        {
            _totalEnergy = PlayerDataManager.GetInstance.GetIntDataForKey(TOTAL_ENERGY);
            DateTime.TryParse(PlayerDataManager.GetInstance.GetStringDataForKey(LAST_ADDED_TIME), out _lastAddedTime);
        }

        public void Save()
        {
            PlayerDataManager.GetInstance.SetIntDataForKey(TOTAL_ENERGY, _totalEnergy);
            PlayerDataManager.GetInstance.SetStringDataForKey(LAST_ADDED_TIME, _lastAddedTime.ToString());
        }
    }
}
