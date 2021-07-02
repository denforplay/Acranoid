using System.Collections.Generic;
using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.Localisation
{
    public class LocalisationController : Controller
    {
        private LocalisationRepository _localizationRepository;
        public string SelectedLanguage => this._localizationRepository.SelectedLanguage;

        public void AddLocalisation(string key, List<KeyValuePair<string, string>> list)
        {
            _localizationRepository.localization[key] = list;
        }

        public void SetLanguage(string selectedLanguage)
        {
            _localizationRepository.SelectedLanguage = selectedLanguage;
        }

        public string GetTranslate(string key)
        {
            if (_localizationRepository.localization is null)
            {
                _localizationRepository.Initialize();
            }

            if (_localizationRepository.localization.ContainsKey(key))
            {
                var keyList = _localizationRepository.localization[key];
                string keyTranslate = string.Empty;
                foreach (var pair in keyList)
                {
                    if (pair.Key == SelectedLanguage)
                    {
                        keyTranslate = pair.Value;
                        break;
                    }
                }

                return keyTranslate;
            }

            return null;
        }

        public override void OnCreate()
        {
            this._localizationRepository = Game.GetRepository<LocalisationRepository>();
        }

        public override void Initialize()
        {
            LocalisationManager.instance.Initialize(this);
        }
    }
}
