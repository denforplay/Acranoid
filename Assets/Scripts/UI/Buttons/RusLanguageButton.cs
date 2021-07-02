using UnityEngine;
using Assets.Scripts.Localisation;

namespace Assets.Scripts.UI.Buttons
{
    public class RusLanguageButton : MonoBehaviour
    {
        public void SetRusLanguage()
        {
            LocalisationManager.instance.SetLanguage("ru");
        }
    }
}
