using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus.Events.BlockEvents;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ProgressBar
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private float _fillSpeed;
        private Slider _slider;
        private float _targetProgress = 0;

        private void Awake()
        {
            _slider = gameObject.GetComponent<Slider>();
        }

        private void Update()
        {
            if (_slider.value < _targetProgress)
            {
                _slider.value += _fillSpeed * Time.deltaTime;
            }
        }

        public void IncrementProgress(IEvent ievnt)
        {
           _targetProgress = _slider.value + 1;
        }

        private void ResetProgress(IEvent ievent)
        {
            var level = LevelManager.GetInstance.GetCurrentLevel();
            _slider.value = 0;
            _targetProgress = 0;
        }


        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnLevelsInitialized>(ResetProgress);
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ResetProgress);
            EventBusManager.GetInstance.Subscribe<OnBlockDestroyEvent>(IncrementProgress);
        }

        private void OnDisable()
        {
            EventBusManager.GetInstance.Unsubscribe<OnLevelsInitialized>(ResetProgress);
            EventBusManager.GetInstance.Unsubscribe<OnLevelCompletedEvent>(ResetProgress);
            EventBusManager.GetInstance.Unsubscribe<OnBlockDestroyEvent>(IncrementProgress);
        }
    }
}
