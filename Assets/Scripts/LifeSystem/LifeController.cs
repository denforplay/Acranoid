using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.LifeSystem
{
    public class LifeController : Controller
    {
        private LifeRepository _lifeRepository;

        public int Lifes => this._lifeRepository.Lifes;

        public override void OnCreate()
        {
            base.OnCreate();
            this._lifeRepository = Game.GetRepository<LifeRepository>();
        }

        public override void Initialize()
        {
            LifeSystem.Initialize(this);
        }

        public bool IsEnoughLifes(int value) => Lifes >= value;

        public void AddLife(object sender, int value)
        {
            this._lifeRepository.Lifes += value;
            this._lifeRepository.Save();
        }

        public void SpendLife(object sender, int value)
        {
            this._lifeRepository.Lifes -= value;
            this._lifeRepository.Save();
        }
    }
}
