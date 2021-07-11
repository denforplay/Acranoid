using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Localisation
{
    public class XmlParser
    {
        private const string KEY_NAME = "Name";
        private const string KEYS = "Keys";
        private const string TRANSLATES = "Translates";

        public Dictionary<string, List<string>> ParseXml(TextAsset _parsedFile)
        {
            XmlDocument localizationDoc = new XmlDocument();
            localizationDoc.LoadXml(_parsedFile.text);
            var localisation = new Dictionary<string, List<string>>();
            foreach (XmlNode key in localizationDoc[KEYS].ChildNodes)
            {
                string keyName = key.Attributes[KEY_NAME].Value;
                List<string> values = new List<string>();
                foreach (XmlNode translate in key[TRANSLATES])
                {
                    values.Add(translate.InnerText);
                }

                localisation[keyName] = values;
            }

            return localisation;
        }
    }
}
