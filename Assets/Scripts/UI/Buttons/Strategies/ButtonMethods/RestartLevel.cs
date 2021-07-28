using Assets.Scripts.Block;
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.Health;
using Assets.Scripts.Level;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class RestartLevel : IButtonMethod
    {
        public void Call()
        {
            PopupManager.GetInstance.DeletePopUp();
            BlocksManager.GetInstance.ReturnAllBlocks(null);
            if (!LevelManager.GetInstance.IsLevelCompleted)
            EnergyManager.GetInstance.SpendEnergy(1);
            LevelManager.GetInstance.LoadCurrentLevel();
        }
    }
}
