using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.Level;

namespace Assets.Scripts.Energy
{
    public class EnergyRepository : Repository
    {
        public int Energy { get; set; }

        public override void Initialize()
        {
            this.Energy = 30;
        }
    }
}
