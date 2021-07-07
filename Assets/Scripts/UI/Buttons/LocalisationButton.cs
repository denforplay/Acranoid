using UnityEngine;
using Assets.Scripts.Localisation;

namespace Assets.Scripts.UI.Buttons
{
    public class LocalisationButton : MonoBehaviour
    {
        public void SetLanguage(string language)
        {
            LocalisationManager.GetInstance.SetLanguage(language);
        }
    }
}
