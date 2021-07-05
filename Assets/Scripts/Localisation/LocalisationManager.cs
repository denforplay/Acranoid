using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
namespace Assets.Scripts.Localisation
{
    public class LocalisationManager : MonoBehaviour
    {
        [SerializeField] TextAsset _localizationFile;
        public static LocalisationManager instance;
        public static event Action OnLocalisationLoaded;
        public static event Action OnLanguageChanged;
        public bool IsInitialized { get; private set; }
        private LocalisationController _localisationController;
        public LocalisationController LocalisationController => _localisationController;

        private void Awake()
        {
            if (instance is null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

        }

        public string Language
        {
            get
            {
                CheckLocalisation();
                return _localisationController.SelectedLanguage;
            }
        }

        public void ReadXmlData()
        {
            XmlDocument localizationDoc = new XmlDocument();
            localizationDoc.LoadXml(_localizationFile.text);
            foreach (XmlNode key in localizationDoc["Keys"].ChildNodes)
            {
                string keyName = key.Attributes["Name"].Value;
                List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                foreach (XmlNode translate in key["Translates"])
                {
                    var pair = new KeyValuePair<string, string>(translate.Name, translate.InnerText);
                    values.Add(pair);
                }

                _localisationController.AddLocalisation(keyName, values);
            }
        }

        public void Initialize(LocalisationController localisationController)
        {
            if (!IsInitialized)
            {
                _localisationController = localisationController;
                ReadXmlData();
                IsInitialized = true;
                OnLocalisationLoaded?.Invoke();
            }
            else
            {
                OnLocalisationLoaded?.Invoke();
            }
        }

        public string GetTranslate(string key)
        {
            CheckLocalisation();
            return _localisationController.GetTranslate(key);
        }

        public void SetLanguage(string language)
        {
            CheckLocalisation();
            _localisationController.SetLanguage(language);
            OnLanguageChanged?.Invoke();
        }

        private void CheckLocalisation()
        {
            if (!IsInitialized)
            {
                throw new ArgumentNullException("Localisation is not initialized yet");
            }
        }
    }
}
