using System.Collections.Generic;
using Assets.Scripts.Abstracts.Repository;
using System.Xml;

namespace Assets.Scripts.Localisation
{
    public class LocalisationRepository : Repository
    {
        public string SelectedLanguage { get; set; } = "en";
        public Dictionary<string, List<KeyValuePair<string, string>>> localization { get; private set; }

        public override void Initialize()
        {
            localization = new Dictionary<string, List<KeyValuePair<string, string>>>();

        }

       

        public override void Save()
        {
        }
    }
}
