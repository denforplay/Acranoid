using UnityEngine;
using Assets.Scripts.Localisation;

namespace Assets.Scripts.UI.Buttons
{
    public class EngLanguageButton : MonoBehaviour
    {
        public void SetEngLanguage()
        {
            LocalisationManager.instance.SetLanguage("en");
        }
    }
}
