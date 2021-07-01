using Assets.Scripts.Abstracts.Scene;
using System.Collections;
using Assets.Scripts.Scenes.SceneManagers;

namespace Assets.Scripts.Abstracts.Game
{
    public enum State
    {
        isLoading,
        isLoaded
    }

    public static class Game
    {
        public static State state { get;  set; }
        public static SceneManagerBase sceneManagerBase { get; private set; }
        public static void Run()
        {
            sceneManagerBase = new StartSceneManager();
            state = State.isLoading;
            Coroutines.Coroutines.StartRoutine(InitializeGameRoutine());
            state = State.isLoaded;
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
