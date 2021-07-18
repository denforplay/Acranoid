using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.Level;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class LoadNextLevel : IButtonMethod
    {
        public void Call()
        {
            EnergyManager.GetInstance.SpendEnergy(1);
            PopupManager.GetInstance.DeletePopUp();
            LevelManager.GetInstance.LoadNextLevel();
        }
    }
}
