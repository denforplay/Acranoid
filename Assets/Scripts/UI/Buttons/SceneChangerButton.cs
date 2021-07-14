using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.UI.PopupSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Buttons
{
    public class SceneChangerButton : MonoBehaviour
    {
        public void OpenMainMenu()
        {
            PopupManager.GetInstance.DeletePopUp();
            SceneManager.LoadScene(StartSceneConfig.SCENE_NAME);
        }
    }
}
