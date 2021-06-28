using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Repository;
using System.Collections;
using UnityEngine;

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
            _controllerBase.CreateAllControllers();
            _repositoriesBase.CreateAllRepositories();
            yield return null;

            _controllerBase.SendOnCreateToAllControllers();
            _repositoriesBase.SendOnCreateToAllRepositories();
            yield return null;

            _controllerBase.InitializeAllControllers();
            _repositoriesBase.InitializeAllRepositories();
            yield return null;

            _controllerBase.SendOnStartToAllControllers();
            _repositoriesBase.SendOnStartToAllRepositories();
            yield return null;
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