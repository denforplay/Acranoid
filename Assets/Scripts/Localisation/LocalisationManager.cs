using Assets.Scripts.Abstracts.Singeton;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Localisation
{
    public class LocalisationManager : Singleton <LocalisationManager>
    {
        [SerializeField] TextAsset _localizationFile;
        public bool IsInitialized { get; private set; }
        private LocalisationController _localisationController;
        public LocalisationController LocalisationController => _localisationController;

        public int LanguageId
        {
            get
            {
                CheckLocalisation();
                return _localisationController.LanguageId;
            }
        }

        public void ReadXmlData()
        {
            XmlParser xmlParser = new XmlParser();
            _localisationController.SetLocalisation(xmlParser.ParseXml(_localizationFile));
        }

        public void Initialize(LocalisationController localisationController)
        {
            if (!IsInitialized)
            {
                _localisationController = localisationController;
                ReadXmlData();
                IsInitialized = true;
                EventBusManager.GetInstance.Invoke<OnLocalisationLoadedEvent>(new OnLocalisationLoadedEvent());
            }
            else
            {
                EventBusManager.GetInstance.Invoke<OnLocalisationLoadedEvent>(new OnLocalisationLoadedEvent());
            }
        }

        public string GetTranslate(string key, int languageId)
        {
            CheckLocalisation();
            return _localisationController.GetTranslate(key, languageId);
        }

        public void SetLanguage(int languageId)
        {
            CheckLocalisation();
            _localisationController.SetLanguage(languageId);
            EventBusManager.GetInstance.Invoke<OnLanguageChanged>(new OnLanguageChanged());
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
