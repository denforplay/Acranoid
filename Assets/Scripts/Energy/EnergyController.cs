using Assets.Scripts.Abstracts.Controller;

namespace Assets.Scripts.Energy
{
    public class EnergyController : Controller
    {
        private EnergyRepository _energyRepository;

        public override void OnCreate()
        {
            base.OnCreate();

        }
        public override void Initialize()
        {
            EnergyManager.GetInstance.Initialize(this);
        }

        public void SpendEnergy(int value)
        {
            _energyRepository.Energy -= value;
        }

        public void AddEnergy(int value)
        {
            _energyRepository.Energy += value;
        }

        public bool IsEnoughEnergy(int value)
        {
            return _energyRepository.Energy > value;
        }
    }
}
