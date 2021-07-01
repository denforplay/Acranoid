namespace Assets.Scripts.Abstracts.Controller
{
    public abstract class Controller
    {
        public virtual void OnCreate() { }

        public abstract void Initialize();

        public virtual void OnStart() { }
    }
}
