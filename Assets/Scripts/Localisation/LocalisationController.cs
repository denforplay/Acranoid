using System.Collections.Generic;
using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.Localisation
{
    public class LocalisationController : Controller
    {
        private LocalisationRepository _localizationRepository;
        public string SelectedLanguage => this._localizationRepository.SelectedLanguage;
        public override void OnCreate()
        {
            base.OnCreate();
            this._localizationRepository = Game.GetRepository<LocalisationRepository>();
        }

        public void SetLanguage(string selectedLanguage)
        {
            _localizationRepository.SelectedLanguage = selectedLanguage;
        }

        public string GetTranslate(string key)
        {
            if (_localizationRepository.localization.ContainsKey(key))
            {
                int languageId = _localizationRepository.localization[key].IndexOf(SelectedLanguage);
                return _localizationRepository.localization[key][languageId];
            }

            return null;
        }
         
        public override void Initialize()
        {
            _localizationRepository.Initialize();

        }
    }
}
