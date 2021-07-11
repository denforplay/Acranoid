using UnityEngine;
using Assets.Scripts.Localisation;

namespace Assets.Scripts.UI.Buttons
{
    public class LocalisationButton : MonoBehaviour
    {
        public void SetLanguage(int language)
        {
            LocalisationManager.GetInstance.SetLanguage(language);
        }
    }
}
