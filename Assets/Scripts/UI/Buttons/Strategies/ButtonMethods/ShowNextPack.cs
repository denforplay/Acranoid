using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class ShowNextPack : IButtonMethod
    {
        public void Call()
        {
            PopupManager.GetInstance.DeletePopUp();
            PopupManager.GetInstance.SpawnPopup<ChooseLevelPopup>();
        }
    }
}
