using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.Scenes.SceneManagers;
using UnityEngine;
namespace Assets.Scripts.UI.Buttons
{
    public class StartGameButton : MonoBehaviour
    {
        public void LoadGameScene()
        {
            Game.sceneManagerBase.LoadNewSceneAsync(GameSceneConfig.SCENE_NAME);
        }
    }
}
