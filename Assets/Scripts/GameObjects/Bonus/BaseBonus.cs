using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.PlatformMovement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GameObjects.Bonus
{
    public abstract class BaseBonus : MonoBehaviour, IPoolable
    {
        [SerializeField] private float _time = 2f;
        private const float TIME_STEP = 2f;
        public bool isInstantlyActivated;
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
            yield return new WaitForSeconds(TIME_STEP);
            Remove();
        }

        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}
