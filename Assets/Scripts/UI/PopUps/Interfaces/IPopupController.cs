using System;
using UnityEngine.Events;

namespace Assets.Scripts.UI.PopUps.Interfaces
{
    public interface IPopupController
    {
        void ShowPopup(IPopupConfig popupConfig, Action rightButtonAction, Action leftButtonAction);
        void HidePopup();
        bool IsActive();
    }
}
