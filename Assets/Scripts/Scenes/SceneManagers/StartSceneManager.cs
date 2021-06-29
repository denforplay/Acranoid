using Assets.Scripts.Abstracts.Scene;
using Assets.Scripts.Scenes.SceneConfigs;

namespace Assets.Scripts.Scenes.SceneManagers
{
    public class StartSceneManager : SceneManagerBase
    {
        public override void InitScenesMap()
        {
            this.sceneConfigMap[StartSceneConfig.SCENE_NAME] = new StartSceneConfig();
            this.sceneConfigMap[GameSceneConfig.SCENE_NAME] = new GameSceneConfig();
        }
    }
}
