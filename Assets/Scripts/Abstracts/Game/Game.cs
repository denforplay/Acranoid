using Assets.Scripts.Abstracts.Scene;
using System.Collections;

namespace Assets.Scripts.Abstracts.Game
{
    public static class Game
    {
        public static SceneManagerBase sceneManagerBase { get; private set; }
        public static void Run()
        {
            Coroutines.Coroutines.StartRoutine(InitializeGameRoutine());
        }

        private static IEnumerator InitializeGameRoutine()
        {
            sceneManagerBase.InitScenesMap();
            yield return sceneManagerBase.LoadCurrentSceneAsync();
        }

        public static T GetController<T>() where T : Controller.Controller
        {
            return sceneManagerBase.GetController<T>();
        }

        public static T GetRepository<T>() where T: Repository.Repository
        {
            return sceneManagerBase.GetRepository<T>();
        }
    }
}
