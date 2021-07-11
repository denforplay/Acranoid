using System.Collections.Generic;
using Assets.Scripts.Abstracts.Repository;
using System.Xml;

namespace Assets.Scripts.Localisation
{
    public class LocalisationRepository : Repository
    {
        public int SelectedLanguage { get; set; } = 0;
        public Dictionary<string, List<string>> localization { get; set; }

        public override void Initialize()
        {
            localization = new Dictionary<string, List<string>>();

        }
    }
}
