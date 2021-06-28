using Assets.Scripts.Abstracts.Scene;
using Assets.Scripts.SceneConfigs;

namespace Assets.Scripts.SceneManagers
{
    public class TestSceneManager : SceneManagerBase
    {
        public override void InitScenesMap()
        {
            this.sceneConfigMap[TestSceneConfig.SCENE_NAME] = new TestSceneConfig();
        }
    }
}
