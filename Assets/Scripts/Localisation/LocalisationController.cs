using System.Collections.Generic;
using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.Localisation
{
    public class LocalisationController : Controller
    {
        private LocalisationRepository _localizationRepository;
        public int LanguageId => this._localizationRepository.SelectedLanguage;

        public void SetLocalisation(Dictionary<string, List<string>> localisation)
        {
            _localizationRepository.localization = localisation;
        }

        public void SetLanguage(int selectedLanguage)
        {
            _localizationRepository.SelectedLanguage = selectedLanguage;
        }

        public string GetTranslate(string key, int languageId)
        {
            if (_localizationRepository.localization.ContainsKey(key))
            {
                return _localizationRepository.localization[key][languageId];
            }

            return key;
        }

        public override void OnCreate()
        {
            this._localizationRepository = Game.GetRepository<LocalisationRepository>();
        }

        public override void Initialize()
        {
            LocalisationManager.GetInstance.Initialize(this);
        }
    }
}
