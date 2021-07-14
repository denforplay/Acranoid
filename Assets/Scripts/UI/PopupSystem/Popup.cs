using Assets.Scripts.Abstracts.Pool.Interfaces;
using UnityEngine;

namespace Assets.Scripts.UI.PopupSystem
{
    public abstract class Popup : MonoBehaviour, IPoolable
    {
        public IObjectPool Origin { get; set; }

        public virtual void Show()
        {
        }

        public virtual void Hide()
        {
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
