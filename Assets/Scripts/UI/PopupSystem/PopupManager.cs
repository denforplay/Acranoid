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

        public Popup SpawnPopup<T>() where T : Popup
        {
            Time.timeScale = 0;
            Popup popUpPrefab = _popupConfig.Popups.Find(a => a.GetType() == typeof(T));
            Popup popup = CreatePopup(popUpPrefab);
            popup.Show();
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
            popup.Hide();
            Destroy(popup.gameObject);
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
