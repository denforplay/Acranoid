using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Abstracts.Singeton;
using System;

namespace Assets.Scripts.UI.PopupSystem
{
    public class PopupSystem : Singleton<PopupSystem>
    {
        private const string CANVAS_NAME = "Canvas";
        [SerializeField] private PopupConfig _popupConfig;
        [SerializeField] private GameObject _canvas;
        private new void Awake()
        {
            IsDestroy = true;
            base.Awake();
        }
        public GameObject GetCanvas 
        {
            get
            {
                if (_canvas == null)
                {
                    _canvas = GameObject.Find(CANVAS_NAME);
                }

                return _canvas;
            }
        }

        private Stack<Popup> _popupsOnCanvas = new Stack<Popup>();
        public Popup SpawnPopup(Type type)
        {
            Time.timeScale = 0;
            Popup popUpPrefab = _popupConfig.Popups.Find(a => a.GetType() == type);
            Popup popup = CreatePopup(popUpPrefab);
            _popupsOnCanvas.Push(popup);
            return popup;
        }

        public Popup CreatePopup(Popup popupPrefab)
        {
            Popup popUp = Instantiate(popupPrefab, GetCanvas.transform);
            return popUp;
        }

        public void DeletePopUp()
        {
            Time.timeScale = 1;
            Popup popup = _popupsOnCanvas.Pop();
            Destroy(popup.gameObject);
        }

        private void OnDestroy()
        {
            while(_popupsOnCanvas.Count != 0)
            {
                DeletePopUp();
            }
        }
    }
}
