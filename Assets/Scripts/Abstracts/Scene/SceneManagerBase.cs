using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Abstracts.Scene
{
    public abstract class SceneManagerBase
    {
        private const float LOAD_PROGRESS = 0.9f;
        public Scene scene { get; private set; }

        protected Dictionary<string, SceneConfig> sceneConfigMap;

        public SceneManagerBase()
        {
            this.sceneConfigMap = new Dictionary<string, SceneConfig>();
        }

        public abstract void InitScenesMap();

        public Coroutine LoadCurrentSceneAsync()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            SceneConfig config = this.sceneConfigMap[sceneName];
            return Coroutines.Coroutines.StartRoutine(this.LoadCurrentSceneRoutine(config));
        }

        public Coroutine LoadNewSceneAsync(string sceneName)
        {
            SceneConfig config = this.sceneConfigMap[sceneName];
            return Coroutines.Coroutines.StartRoutine(this.LoadNewSceneRoutine(config));
        }

        private IEnumerator LoadNewSceneRoutine(SceneConfig sceneConfig)
        {
            yield return Coroutines.Coroutines.StartRoutine(this.LoadSceneRoutine(sceneConfig));
            yield return Coroutines.Coroutines.StartRoutine(this.InitializeSceneRoutine(sceneConfig));
        }

        private IEnumerator LoadCurrentSceneRoutine(SceneConfig sceneConfig)
        {
            yield return Coroutines.Coroutines.StartRoutine(this.InitializeSceneRoutine(sceneConfig));
        }

        private IEnumerator LoadSceneRoutine(SceneConfig sceneConfig)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneConfig.SceneName);
            asyncOperation.allowSceneActivation = false;
            while (asyncOperation.progress < LOAD_PROGRESS)
            {
                yield return null;
            }

            yield return null;
            yield return null;
            yield return null;
            asyncOperation.allowSceneActivation = true;
        }

        private IEnumerator InitializeSceneRoutine(SceneConfig sceneConfig)
        {
            this.scene = new Scene(sceneConfig);
            yield return this.scene.InitializeAsync();
        }

        public T GetRepository<T>() where T : Repository.Repository
        {
            return this.scene.GetRepository<T>();
        }

        public T GetController<T>() where T : Controller.Controller
        {
            return this.scene.GetController<T>();
        }
    }
}
