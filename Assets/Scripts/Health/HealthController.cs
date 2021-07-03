using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using System;

namespace Assets.Scripts.Health
{
    public class HealthController : Controller
    {
        public event Action OnHeartSpendedEvent;
        private HealthRepository _healthRepository;

        public int Health => this._healthRepository.Health;

        public override void OnCreate()
        {
            base.OnCreate();
            this._healthRepository = Game.GetRepository<HealthRepository>();
        }

        public override void Initialize()
        {
            HealthManager.instance.InitializeHealthController(this);
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
                OnHeartSpendedEvent?.Invoke();
                return;
            }

            this._healthRepository.Save();
        }
    }
}
