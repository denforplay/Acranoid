using UnityEngine;
using TMPro;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus.Events.LocalisationEvents;

namespace Assets.Scripts.Localisation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalisationObject : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private string _key;
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _key = _text.text;
            EventBusManager.GetInstance.Invoke<OnLocalisationObjectAwakeEvent>(new OnLocalisationObjectAwakeEvent());
        }

        private void Translate(IEvent ievent)
        {
            _text.text = LocalisationManager.GetInstance.GetTranslate(_key, LocalisationManager.GetInstance.LanguageId);
        }

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnLocalisationLoadedEvent>(Translate);
            EventBusManager.GetInstance.Subscribe<OnLocalisationLoadedEvent>((OnLocalisationLoadedEvent) => EventBusManager.GetInstance.Subscribe<OnLocalisationObjectAwakeEvent>(Translate));
            EventBusManager.GetInstance.Subscribe<OnLanguageChanged>(Translate);
        }

        private void OnBecameInvisible()
        {
            EventBusManager.GetInstance.Subscribe<OnLocalisationLoadedEvent>(Translate);
            EventBusManager.GetInstance.Subscribe<OnLanguageChanged>(Translate);
        }
    }
}
