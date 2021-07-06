using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.UI.PopUps.Interfaces;
using System;

namespace Assets.Scripts.UI.PopUps
{
    public class PopupController : Controller, IPopupController
    {
        private Action buttonLeft;
        private Action buttonRight;

        public void HealthEndedPopUp()
        {
            IPopupConfig popupConfig = new Popup("GAME OVER", "Choose button", null, null);
            ShowPopup(popupConfig, () => HidePopup(), () => HidePopup());
        }

        public void ShowPopup(IPopupConfig popupConfig, Action right = null, Action left = null)
        {
            buttonLeft = left;
            buttonRight = right;
            PopupManager.instance.LeftButton.onClick.AddListener(left.Invoke);
            PopupManager.instance.RightButton.onClick.AddListener(right.Invoke);
            PopupManager.instance.Popup.SetActive(true);
            SetPopupData(popupConfig);
        }

        public void HidePopup()
        {
            buttonLeft = null;
            buttonRight = null;

            if (PopupManager.instance.Popup.activeSelf)
            {
                PopupManager.instance.Popup.SetActive(false);
            }
        }

        public bool IsActive()
        {
            return PopupManager.instance.Popup.activeSelf;
        }

        private void SetPopupData(IPopupConfig popupConfig)
        {
            PopupManager.instance.Popup.gameObject.SetActive(true);
            PopupManager.instance.Popup.gameObject.SetActive(true);

            PopupManager.instance.Tittle.text = popupConfig.Title;
            PopupManager.instance.Text.text = popupConfig.Text;
        }

        public override void Initialize()
        {
            PopupManager.instance.Initialize(this);
        }
    }
}