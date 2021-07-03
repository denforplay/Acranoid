
namespace Assets.Scripts.Abstracts.Singleton
{
    interface ISingleton<T> where T : ISingleton<T>
    {
        T GetInstance();
    }
}
