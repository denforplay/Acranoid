using Assets.Scripts.Abstracts.Game;
using UnityEngine;
using TMPro;

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
            Localisation.OnLocalisationLoaded += Translate;
        }

        private void Initialize()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _key = _text.text;
        }

        private void Translate()
        {
            Localisation.SetLanguage("ru");
            _text.text = Localisation.GetTranslate(_key);
        }
    }
}
