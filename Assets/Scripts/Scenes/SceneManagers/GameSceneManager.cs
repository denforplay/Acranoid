using Assets.Scripts.Abstracts.Scene;
using Assets.Scripts.Scenes.SceneConfigs;

namespace Assets.Scripts.Scenes.SceneManagers
{
    public class GameSceneManager : SceneManagerBase
    {
        public override void InitScenesMap()
        {
            this.sceneConfigMap[GameSceneConfig.SCENE_NAME] = new GameSceneConfig();
            this.sceneConfigMap[StartSceneConfig.SCENE_NAME] = new StartSceneConfig();
        }
    }
}
