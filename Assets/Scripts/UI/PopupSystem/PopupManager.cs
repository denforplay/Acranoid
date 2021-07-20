using Assets.Scripts.Abstracts.Singeton;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.PopupSystem
{
    public class PopupManager : Singleton<PopupManager>
    {
        [SerializeField] private PopupConfig _popupConfig;
        [SerializeField] private GameObject _canvas;

        public GameObject Canvas => _canvas;

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
                return _canvas;
            }
        }

        public Popup SpawnPopup<T>() where T : Popup
        {
            Time.timeScale = 0;
            Popup popUpPrefab = _popupConfig.Popups.Find(a => a.GetType() == typeof(T));
            Popup popup = CreatePopup(popUpPrefab);
            _popupsOnCanvas.Push(popup);
            return popup;
        }

        private Popup CreatePopup(Popup popupPrefab)
        {
            Popup popUp = Instantiate(popupPrefab, GetCanvas.transform);
            popUp.Show();
            return popUp;
        }

        public void DeletePopUp()
        {
            Time.timeScale = 1;
            Popup popup = _popupsOnCanvas.Pop();
            popup.Hide();
            Destroy(popup.gameObject);
        }

        public void DeleteAllPopups()
        {
            while (_popupsOnCanvas.Count != 0)
            {
                DeletePopUp();
            }
        }

        private void OnDestroy()
        {
            DeleteAllPopups();
        }
    }
}
