using Assets.Scripts.Abstracts.Singeton;

namespace Assets.Scripts.Energy
{
    public class EnergyManager : Singleton<EnergyManager>
    {
        public bool IsInitialized { get; private set; }
        private EnergyController _energyController;

        public void Initialize(EnergyController energyController)
        {
            _energyController = energyController;
        }
    }
}
