using Assets.Scripts.Level;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class ClosePopupMethod : IButtonMethod
    {
        public void Call()
        {
            PopupManager.GetInstance.DeletePopUp();
        }
    }
}
