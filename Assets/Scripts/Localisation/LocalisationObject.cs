using UnityEngine;
using TMPro;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Abstracts.EventBus.Interfaces;

namespace Assets.Scripts.Localisation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalisationObject : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private string _key;
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _key = _text.text;
        }

        private void Translate(IEvent ievent)
        {
            _text.text = LocalisationManager.GetInstance.GetTranslate(_key, LocalisationManager.GetInstance.LanguageId);
        }

        private void OnEnable()
        {
            EventBusManager.GetInstance.Subscribe<OnLocalisationLoadedEvent>(Translate);
            EventBusManager.GetInstance.Subscribe<OnLanguageChanged>(Translate);
        }

        private void OnBecameInvisible()
        {
            EventBusManager.GetInstance.Subscribe<OnLocalisationLoadedEvent>(Translate);
            EventBusManager.GetInstance.Subscribe<OnLanguageChanged>(Translate);
        }
    }
}
