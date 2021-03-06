using Assets.Scripts.Block;
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.EnergySystem.Timer;
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
            LevelManager.GetInstance.IsLevelRestarted = true;
            PopupManager.GetInstance.DeletePopUp();
            BlocksManager.GetInstance.ReturnAllBlocks(null);
            if (!LevelManager.GetInstance.IsLevelCompleted)
            {
                EnergyManager.GetInstance.SpendEnergy(1);
            }
            LevelManager.GetInstance.LoadCurrentLevel();
            LevelManager.GetInstance.IsLevelRestarted = false;
        }
    }
}
