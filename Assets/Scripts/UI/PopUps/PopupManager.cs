using Assets.Scripts.UI.PopUps.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI.PopUps
{
    public class PopupManager : MonoBehaviour, IPopupButtons, IPopupController
    {
        [SerializeField] private GameObject _popup;
        [SerializeField] private TextMeshProUGUI _tittle;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        private Action buttonLeft;
        private Action buttonRight;
        public static PopupManager instance;
        public bool IsInitialized { get; private set; }
        private void Awake()
        {
            if (instance is null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            IsInitialized = true;

            DontDestroyOnLoad(gameObject);
        }

        public void HealthEndedPopUp()
        {
            IPopupConfig popupConfig = new Popup("GAME OVER", "Choose button", null, null);
            ShowPopup(popupConfig, () => HidePopup(), () => HidePopup());
        }

        public void ShowPopup(IPopupConfig popupConfig, Action right = null, Action left = null)
        {
            CheckInitialization();
            buttonLeft = left;
            buttonRight = right;
            _leftButton.onClick.AddListener(left.Invoke);
            _rightButton.onClick.AddListener(right.Invoke);
            _popup.SetActive(true);
            SetPopupData(popupConfig);
        }

        public void HidePopup()
        {
            CheckInitialization();
            buttonLeft = null;
            buttonRight = null;

            if (_popup.activeSelf)
            {
                _popup.SetActive(false);
            }
        }

        public void ButtonLeftAction()
        {
            CheckInitialization();
            if (buttonLeft is null)
            {
                buttonLeft();
            }
            else
            {
                HidePopup();
            }
        }

        public void ButtonRightAction()
        {
            CheckInitialization();
            if (buttonRight is null)
            {
                buttonRight();
            }
            else
            {
                HidePopup();
            }
        }

        public bool IsActive()
        {
            CheckInitialization();
            return _popup.activeSelf;
        }

        public void CheckInitialization()
        {
            if (!IsInitialized)
            {
                throw new ArgumentNullException("Popup manager no intialized yet");
            }
        }

        private void SetPopupData(IPopupConfig popupConfig)
        {
            CheckInitialization();
            _rightButton.gameObject.SetActive(true);
            _leftButton.gameObject.SetActive(true);

            _tittle.text = popupConfig.Title;
            _text.text = popupConfig.Text;
        }
    }
}