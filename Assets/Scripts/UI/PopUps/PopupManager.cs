﻿using Assets.Scripts.UI.PopUps.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Assets.Scripts.UI.PopUps
{
    public class PopupManager : MonoBehaviour, IPopupButtons, IPopupController
    {

        private Action buttonLeft;
        private Action buttonRight;
        public static PopupManager instance;
        private PopupController _popupController;
        [SerializeField] private GameObject _popup;
        [SerializeField] private TextMeshProUGUI _tittle;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;

        public bool IsInitialized { get; private set; }
        public GameObject Popup => _popup;
        public Button LeftButton => _leftButton;
        public Button RightButton => _rightButton;
        public TextMeshProUGUI Tittle => _tittle;
        public TextMeshProUGUI Text => _text;
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

        public void Initialize(PopupController popupController)
        {
            _popupController = popupController;
        }

        public void HealthEndedPopUp()
        {
            IPopupConfig popupConfig = new Popup("GAME OVER", "Choose button", null, null);
            ShowPopup(popupConfig, () => HidePopup(), () => HidePopup());
        }

        public void ShowPopup(IPopupConfig popupConfig, Action right = null, Action left = null)
        {
            CheckInitialization();
            _popupController.ShowPopup(popupConfig, right, left);
        }

        public void HidePopup()
        {
            CheckInitialization();
            _popupController.HidePopup();
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
    }
}