using UnityEngine;
using TMPro;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Level;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;

namespace Assets.Scripts.UI.Text
{
    public class LevelText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;

        private void Awake()
        {
            ChangeLevelText(null);
        }

        public void ChangeLevelText(IEvent ievent)
        {
            _levelText.text = LevelManager.GetInstance.CurrentLevel.levelName;
        }
        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnNextLevelLoadedEvent>(ChangeLevelText);
        }

        private void OnDisable()
        {
            EventBusManager.GetInstance.Unsubscribe<OnNextLevelLoadedEvent>(ChangeLevelText);
        }
    }
}