using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class AddEnergy : IButtonMethod
    {
        IButtonMethod restart = new RestartLevel();
        public void Call()
        {
            EnergyManager.GetInstance.AddEnergy(1);
            restart.Call();
        }
    }
}
