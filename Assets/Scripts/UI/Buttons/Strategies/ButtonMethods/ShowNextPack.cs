using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;
using Assets.Scripts.UI.PopupSystem.ConcretePopups;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class ShowNextPack : IButtonMethod
    {
        public void Call()
        {
            PopupManager.GetInstance.DeletePopUp();
            PopupManager.GetInstance.SpawnPopup<ChoosePackagePopup>();
        }
    }
}
