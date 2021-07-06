using UnityEngine;
using Assets.Scripts.Localisation;

namespace Assets.Scripts.UI.Buttons
{
    public class RusLanguageButton : MonoBehaviour
    {
        public void SetRusLanguage()
        {
            LocalisationManager.GetInstance.SetLanguage("ru");
        }
    }
}
