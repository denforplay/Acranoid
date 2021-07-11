using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Abstracts.Singeton;
using System;

namespace Assets.Scripts.UI.PopupSystem
{
    public class PopupSystem : Singleton<PopupSystem>
    {
        [SerializeField] private PopupConfig _popupConfig;
        [SerializeField] private Canvas _canvas;
        private Stack<Popup> _popupsOnCanvas = new Stack<Popup>();
        public Popup SpawnPopup(Type type)
        {
            Popup popUpPrefab = _popupConfig.Popups.Find(a => a.GetType() == type);
            Popup popup = CreatePopup(popUpPrefab);
            _popupsOnCanvas.Push(popup);
            return popup;
        }

        public Popup CreatePopup(Popup popupPrefab)
        {
            Popup popUp = Instantiate(popupPrefab, _canvas.transform);
            return popUp;
        }

        public void DeletePopUp()
        {
            Popup popup = _popupsOnCanvas.Pop();
            Destroy(popup.gameObject);
        }
    }
}
