﻿using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Health
{
    public class HealthManager : MonoBehaviour
    {
        public static event Action OnHealthInitializedEvent;
        public event Action OnHeartSpendEvent;
        public static HealthManager instance;
        [SerializeField] HeartConfig _heartConfig;
        public bool IsInitialized { get; private set; }
        private HealthController _healthController;
        private HealthViewController _healthViewController;

        public HeartConfig HeartConfig
        {
            get
            {
                CheckHeartsInitialized();
                return _heartConfig;
            }
        }

        public int Health
        {
            get
            {
                CheckHeartsInitialized();
                return _healthController.Health;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            OnHealthInitializedEvent += () =>
            {
                LevelManager.OnLevelsInitialized += ShowHealth;
            };
            DontDestroyOnLoad(gameObject);
        }

        private void ShowHealth()
        {
            CheckHeartsInitialized();
            _healthViewController.ViewHearts();
        }

        public void SpendHeart(int value)
        {
            CheckHeartsInitialized();
            if (_healthController.IsEnoughLifes(0))
            {
                DeleteHealthView();
                OnHeartSpendEvent?.Invoke();
            }
            _healthController.SpendLife(value);
        }

        public void InitializeHealthController(HealthController healthController)
        {
            _healthController = healthController;
            IsInitialized = true;
            OnHealthInitializedEvent?.Invoke();
        }

        public void InitializeViewController(HealthViewController healthViewController)
        {
            _healthViewController = healthViewController;
        }

        public GameObject CreateHeart()
        {
            return Instantiate(_heartConfig.heartPrefab);
        }

        public void DeleteHealthView()
        {
            CheckHeartsInitialized();
            _healthViewController.DeleteHealthView();
        }

        private void CheckHeartsInitialized()
        {
            if (!IsInitialized)
            {
                throw new ArgumentNullException("Hearts are not initialized yet");
            }
        }
    }
}