using UnityEngine;
namespace Assets.Scripts.BlockSystem.FactoryPattern
{
    public interface IFactory<T> where T : MonoBehaviour
    {
        T Prefab { get; set; }
        T GetNewInstance();
    }
}
