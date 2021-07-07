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
        [SerializeField] private string _key;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _key = _text.text;
        }

        private void Translate()
        {
            _text.text = LocalisationManager.GetInstance.GetTranslate(_key);
        }

        private void OnEnable()
        {
            LocalisationManager.OnLocalisationLoaded += Translate;
            LocalisationManager.OnLanguageChanged += Translate;
        }

        private void OnDisable()
        {
            LocalisationManager.OnLocalisationLoaded -= Translate;
            LocalisationManager.OnLanguageChanged -= Translate;
        }
    }
}
