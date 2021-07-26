using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.Abstracts.Game;
using System.Collections;
using UnityEngine;
using Assets.Scripts.Scenes.SceneConfigs;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events;
using Assets.Scripts.Block;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Abstracts.Scene
{
    public class Scene
    {
        private ControllersBase _controllerBase;
        private RepositoriesBase _repositoriesBase;
        private SceneConfig _sceneConfig;

        public Scene(SceneConfig sceneConfig)
        {
            _sceneConfig = sceneConfig;
            this._controllerBase = new ControllersBase(sceneConfig);
            this._repositoriesBase = new RepositoriesBase(sceneConfig);
        }

        public Coroutine InitializeAsync()
        {
            return Coroutines.Coroutines.StartRoutine(this.InitializeRoutine());
        }

        private IEnumerator InitializeRoutine()
        {
            _repositoriesBase.CreateAllRepositories();
            _controllerBase.CreateAllControllers();
            yield return null;
            _repositoriesBase.SendOnCreateToAllRepositories();
            _controllerBase.SendOnCreateToAllControllers();
            yield return null;

            _repositoriesBase.InitializeAllRepositories();
            _controllerBase.InitializeAllControllers();
            yield return null;

            _repositoriesBase.SendOnStartToAllRepositories();
            _controllerBase.SendOnStartToAllControllers();
            yield return null;

            if (_sceneConfig is GameSceneConfig && BlocksManager._isInitialized)
            {
                BlockGenerator.GetInstance.ShowBlocks(null);
            }
            else if (_sceneConfig is GameSceneConfig)
            {
                EventBusManager.GetInstance.Subscribe<OnBlocksManagerInitializedEvent>((OnBlocksManagerInitializedEvent) => BlockGenerator.GetInstance.ShowBlocks(null));
            }
        }

        public T GetRepository<T>() where T : Repository.Repository
        {
            return this._repositoriesBase.GetRepository<T>();
        }

        public T GetController<T>() where T : Controller.Controller
        {
            return this._controllerBase.GetController<T>();
        }
    }
}