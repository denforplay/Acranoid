using Assets.Scripts.PlayerData;
using System;

namespace Assets.Scripts.EnergySystem.Timer
{

    public class TimerRepository
    {
        private const string NEXT_ENERGY_TIME = "nextEnergyTime";
        private const string LAST_ADDED_TIME = "lastAddedTime";

        public DateTime nextEnergyTime;
        public DateTime lastAddedTime;
        public void Initialize()
        {
            DateTime.TryParse(PlayerDataManager.GetInstance.GetStringDataForKey(NEXT_ENERGY_TIME), out nextEnergyTime);
            DateTime.TryParse(PlayerDataManager.GetInstance.GetStringDataForKey(LAST_ADDED_TIME), out lastAddedTime);
        }

        public void Save()
        {
            PlayerDataManager.GetInstance.SetStringDataForKey(NEXT_ENERGY_TIME, nextEnergyTime.ToString());
            PlayerDataManager.GetInstance.SetStringDataForKey(LAST_ADDED_TIME, lastAddedTime.ToString());
        }
    }
}
