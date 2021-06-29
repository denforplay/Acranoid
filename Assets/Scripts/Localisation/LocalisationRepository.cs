using System.Collections.Generic;
using Assets.Scripts.Abstracts.Repository;
using System.Xml;

namespace Assets.Scripts.Localisation
{
    public class LocalisationRepository : Repository
    {
        private const string LOCALISATION_FILE_PATH = @"C:\Users\KompAZ\Documents\GitHub\Acranoid\Assets\Resources\Localisation\Localisation.xml";

        public string SelectedLanguage { get; set; }
        public Dictionary<string, List<KeyValuePair<string, string>>> localization { get; private set; }

        public override void Initialize()
        {
            localization = new Dictionary<string, List<KeyValuePair<string, string>>>();
            XmlDocument localizationDoc = new XmlDocument();
            localizationDoc.Load(LOCALISATION_FILE_PATH);
            foreach (XmlNode key in localizationDoc["Keys"].ChildNodes)
            {
                string keyName = key.Attributes["Name"].Value;
                List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                foreach (XmlNode translate in key["Translates"])
                {
                    var pair = new KeyValuePair<string, string>(translate.Name, translate.InnerText);
                    values.Add(pair);
                }

                localization[keyName] = values;
            }
        }

        public override void Save()
        {
        }
    }
}
