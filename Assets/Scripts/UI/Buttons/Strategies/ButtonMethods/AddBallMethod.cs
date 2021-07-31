using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.BallEvents;
using Assets.Scripts.Health;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;
using Assets.Scripts.UI.PopupSystem.ConcretePopups;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class AddBallMethod : IButtonMethod
    {
        public void Call()
        {
            if (EnergyManager.GetInstance.TotalEnergy >= 1)
            {
                EnergyManager.GetInstance.SpendEnergy(1);
                EventBusManager.GetInstance.Invoke<OnBallAddedEvent>(new OnBallAddedEvent());
                PopupManager.GetInstance.DeleteAllPopups();
            }
            else
            {
                PopupManager.GetInstance.SpawnPopup<EnergyEndedPopup>();
            }
        }
    }
}
