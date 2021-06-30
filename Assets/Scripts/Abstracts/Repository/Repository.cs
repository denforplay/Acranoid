namespace Assets.Scripts.Abstracts.Repository
{
    public abstract class Repository
    {
        public abstract void OnCreate();

        public abstract void Initialize();

        public abstract void OnStart();

        public abstract void Save();
    }
}

