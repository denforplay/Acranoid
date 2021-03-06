using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Popup;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.Buttons.Strategies.Interfaces;
using Assets.Scripts.UI.PopupSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Buttons.Strategies.ButtonMethods
{
    public class OpenMainMenu : IButtonMethod
    {
        public void Call()
        {
            PopupManager.GetInstance.DeletePopUp();
            Time.timeScale = 1;
            SceneManager.LoadScene(StartSceneConfig.SCENE_NAME);
        }
    }
}
