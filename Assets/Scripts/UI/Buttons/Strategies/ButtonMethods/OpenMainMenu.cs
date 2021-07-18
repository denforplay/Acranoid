using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class OpenMainMenu : IButtonMethod
    {
        public void Call()
        {
            PopupManager.GetInstance.DeletePopUp();
            SceneManager.LoadScene(StartSceneConfig.SCENE_NAME);
        }
    }
}
