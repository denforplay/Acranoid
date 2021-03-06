using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.EventBus.Events.BlockEvents;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Assets.Scripts.ProgressBar
{
    public class LevelProgressBar : MonoBehaviour
    {
        private const int COLOR_BLOCK = 1;
        private Slider _slider;
        private float _targetProgress = 0;

        private void Awake()
        {
            _slider = gameObject.GetComponent<Slider>();
            var level = LevelManager.GetInstance.GetCurrentLevel();
            _slider.maxValue = level.blocksData.Count(x => x >= COLOR_BLOCK);
        }

        public void IncrementProgress(IEvent ievnt)
        {
           _targetProgress = _slider.value + 1;
            _slider.value = _targetProgress;
        }

        private void ResetProgress(IEvent ievent)
        {
            var level = LevelManager.GetInstance.GetCurrentLevel();
            _slider.value = 0;
            _slider.maxValue = level.blocksData.Count(x => x >= COLOR_BLOCK);
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
            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>(ResetProgress);
            EventBusManager.GetInstance.Unsubscribe<OnBlockDestroyEvent>(IncrementProgress);
        }
    }
}
