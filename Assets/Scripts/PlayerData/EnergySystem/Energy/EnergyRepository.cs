using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.PlayerData;
using System;

namespace Assets.Scripts.EnergySystem.Energy
{
    public class EnergyRepository : Repository
    {
        private const string TOTAL_ENERGY = "totalEnergy";

        public int _totalEnergy = 0;

        public override void Initialize()
        {
            _totalEnergy = PlayerDataManager.GetInstance.GetIntDataForKey(TOTAL_ENERGY);
        }

        public override void Save()
        {
            PlayerDataManager.GetInstance.SetIntDataForKey(TOTAL_ENERGY, _totalEnergy);
        }
    }
}
