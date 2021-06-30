using Assets.Scripts.Abstracts.Game;
using UnityEngine;
using TMPro;

namespace Assets.Scripts.Localisation
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalisationObject : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private string _key;

        private void Initialize()
        {
            _text = GetComponent<TextMeshProUGUI>();
            _key = _text.text;
        }

        public void Localise()
        {
            if (_text == null)
            {
                Initialize();
            }

            if (Game.state == State.isLoaded)
            {
                Game.GetController<LocalisationController>().SetLanguage("ru");
                _text.text = Game.GetController<LocalisationController>().GetTranslate(_key);
            }
            else
            {
                Localise();
            }
        }
    }
}
