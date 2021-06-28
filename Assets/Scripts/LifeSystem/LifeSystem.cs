using System;

namespace Assets.Scripts.LifeSystem
{
    public static class LifeSystem
    {
        public static event Action OnLifesInitialized;
        public static bool IsInitialized { get; private set; }
        public static int Lifes
        {
            get
            {
                CheckLifeSystem();
                return _lifeController.Lifes;
            }
        }
        private static LifeController _lifeController;
        public static void Initialize(LifeController lifeController)
        {
            _lifeController = lifeController;
            IsInitialized = true;
            OnLifesInitialized?.Invoke();
        }

        public static bool IsEnoughLifes(int value)
        {
            CheckLifeSystem();
            return _lifeController.IsEnoughLifes(value);
        } 

        public static void AddLife(object sender, int value)
        {
            CheckLifeSystem();
            _lifeController.AddLife(sender, value);
        }

        public static void SpendLife(object sender, int value)
        {
            CheckLifeSystem();
            _lifeController.SpendLife(sender, value);
        }

        private static void CheckLifeSystem()
        {
            if (!IsInitialized)
            {
                throw new ArgumentNullException("Lifes is not initialized yet");
            }
        }
    }
}
