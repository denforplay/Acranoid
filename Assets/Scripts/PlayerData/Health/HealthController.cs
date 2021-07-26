using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;

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
            _healthRepository.Initialize();
        }

        public void SetHeartPrefab(Heart prefab)
        {
            _healthRepository.heartPrefab = prefab;
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
            HealthViewManager.GetInstance.DeleteHealthView(null);
            if (IsEnoughLifes(0))
            {
                return;
            }
        }
    }
}
