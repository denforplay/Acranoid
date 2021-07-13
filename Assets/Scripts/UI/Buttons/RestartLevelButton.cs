﻿using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Buttons
{
    public class RestartLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public void Awake()
        {
            _button.onClick.AddListener(RestartCurrentLevel);
        }

        public void RestartCurrentLevel()
        {
            LevelManager.GetInstance.LoadCurrentLevel();
            PopupSystem.PopupSystem.GetInstance.DeletePopUp();
        }
    }
}