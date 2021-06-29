using System.Collections.Generic;
using Assets.Scripts.Abstracts.Repository;
using System.Xml;

namespace Assets.Scripts.Localisation
{
    public class LocalisationRepository : Repository
    {
        private const string LOCALISATION_FILE_PATH = @"..\..\Assets\Localisation.xml";

        public string SelectedLanguage { get; set; }
        public Dictionary<string, List<string>> localization { get; private set; }

        public override void Initialize()
        {
            localization = new Dictionary<string, List<string>>();
            XmlDocument localizationDoc = new XmlDocument();
            localizationDoc.LoadXml(LOCALISATION_FILE_PATH);
            foreach (XmlNode key in localizationDoc["Keys"].ChildNodes)
            {
                string keyName = key.Attributes["Name"].Value;
                List<string> values = new List<string>();
                foreach (XmlNode translate in key["Translates"])
                {
                    values.Add(translate.Value);
                }

                localization[keyName] = values;
            }
        }

        public override void Save()
        {

        }
    }
}
