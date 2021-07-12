using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.EventBus;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.Scenes.SceneManagers;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Assets.Scripts.UI.Buttons
{
    public class SceneChangerButton : MonoBehaviour
    {
        public void OpenMainMenu()
        {
            PopupSystem.PopupSystem.GetInstance.DeletePopUp();
            SceneManager.LoadScene(StartSceneConfig.SCENE_NAME);
        }
    }
}
