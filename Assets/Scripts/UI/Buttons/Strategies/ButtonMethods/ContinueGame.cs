using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;
using UnityEngine;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class ContinueGame : IButtonMethod
    {
        public void Call()
        {
            Time.timeScale = 1;
            PopupManager.GetInstance.DeletePopUp();
        }
    }
}
