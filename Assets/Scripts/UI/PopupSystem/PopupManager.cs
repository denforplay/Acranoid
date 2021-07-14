using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Popup;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.PopupSystem
{
    public class PopupManager : Singleton<PopupManager>
    {
        private const string CANVAS_NAME = "Canvas";

        [SerializeField] private PopupConfig _popupConfig;
        [SerializeField] private GameObject _canvas;
        public PopupConfig PopupConfig => _popupConfig;
        private bool isInitialized { get; set; }
        private PopupController _popupController;
        private Stack<Popup> _popupsOnCanvas = new Stack<Popup>();
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

        public void Initialize(PopupController popupController)
        {
            _popupController = popupController;
            isInitialized = true;
            EventBusManager.GetInstance.Invoke(new OnPopupManagerInitializedEvent());
        }

        public void SpawnPopup<T>() where T : Popup
        {
            Time.timeScale = 0;
            Popup popUpPrefab = _popupController.GetPopup(typeof(T));
            _popupsOnCanvas.Push(popUpPrefab);
        }

        public void DeletePopUp()
        {
            Time.timeScale = 1;
            Popup popup = _popupsOnCanvas.Pop();
            popup.ReturnToPool();
        }

        private void OnDestroy()
        {
            while (_popupsOnCanvas.Count != 0)
            {
                DeletePopUp();
            }
        }
    }
}
