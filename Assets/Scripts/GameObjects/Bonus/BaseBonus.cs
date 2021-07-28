using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.PlatformMovement;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameObjects.Bonus
{
    public abstract class BaseBonus : MonoBehaviour, IPoolable
    {
        [SerializeField] private Sprite _bonusOnBlockImage;
        [SerializeField] private float _duration = 2.0f;
        public bool isInstantlyActivated;
        public Sprite BonusOnBlockImage => _bonusOnBlockImage;
        public IObjectPool Origin { get; set; }

        public abstract void Apply();
        public abstract void Remove();

        public void Stop()
        {
            Remove();
        }

        protected void StartTimer()
        {
            Coroutines.Coroutines.StartRoutine(Timer());
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_duration);
            Remove();
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
