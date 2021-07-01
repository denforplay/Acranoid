using System;

namespace Assets.Scripts.Localisation
{
    public class Localisation
    {
        public static event Action OnLocalisationLoaded;
        public static bool IsInitialized { get; private set; }
        private static LocalisationController _localisationController;
        public static string Language
        {
            get
            {
                CheckLocalisation();
                return _localisationController.SelectedLanguage;
            }
        }

        public static void Initialize(LocalisationController localisationController)
        {
            _localisationController = localisationController;
            IsInitialized = true;
            OnLocalisationLoaded?.Invoke();
        }

        public static string GetTranslate(string key)
        {
            CheckLocalisation();
            return _localisationController.GetTranslate(key);
        }

        public static void SetLanguage(string language)
        {
            CheckLocalisation();
            _localisationController.SetLanguage(language);
        }

        private static void CheckLocalisation()
        {
            if (!IsInitialized)
            {
                throw new ArgumentNullException("Localisation is not initialized yet");
            }
        }
    }
}
