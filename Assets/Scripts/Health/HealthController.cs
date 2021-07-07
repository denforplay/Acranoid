﻿using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.Block;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using System;

namespace Assets.Scripts.Health
{
    public class HealthController : Controller
    {
        private HealthRepository _healthRepository;

        public int Health => this._healthRepository.Health;


        public override void OnCreate()
        {
            base.OnCreate();
            this._healthRepository = Game.GetRepository<HealthRepository>();
        }

        public void InitializeHearts()
        {
            _healthRepository.InitializeHearts();
        }

        public override void Initialize()
        {
            HealthManager.GetInstance.InitializeHealthController(this);
        }

        public bool IsEnoughLifes(int value) => Health > value;

        public void AddLife(int value)
        {
            this._healthRepository.Health += value;
            this._healthRepository.Save();
        }

        public void SpendLife(int value)
        {
            this._healthRepository.Health -= value;
            if (IsEnoughLifes(0))
            {
                EventBusManager.GetInstance.Invoke<OnHeartSpendEvent>(new OnHeartSpendEvent());
                return;
            }

            this._healthRepository.Save();
        }
    }
}
