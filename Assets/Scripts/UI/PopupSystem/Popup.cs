using Assets.Scripts.Abstracts.Pool.Interfaces;
using UnityEngine;
using DG.Tweening;

namespace Assets.Scripts.UI.PopupSystem
{
    public abstract class Popup : MonoBehaviour, IPoolable
    {
        public IObjectPool Origin { get; set; }
        public abstract void DisableInput();
        public abstract void EnableInput();
        public virtual void Show()
        {
            DisableInput();
            Time.timeScale = 0;
            transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, 0f);
            transform.DOMoveY(PopupManager.GetInstance.Canvas.transform.position.y, 2f).OnComplete(() =>
            {
                EnableInput();
            });
        }

        public virtual void Hide()
        {
            DisableInput();
            transform.DOMoveY(-PopupManager.GetInstance.Canvas.transform.position.y, 2f).OnComplete(() =>
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            });
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
