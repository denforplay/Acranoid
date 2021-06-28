using System;
using Assets.Scripts.LifeSystem;
using System.Collections.Generic;
using Assets.Scripts.Abstracts.Scene;

namespace Assets.Scripts.Abstracts.Repository
{
    public class RepositoriesBase
    {
        private Dictionary<Type, Repository> _repositoriesMap;
        private SceneConfig _sceneConfig;
        public RepositoriesBase(SceneConfig sceneConfig)
        {
            this._sceneConfig = sceneConfig;
            this._repositoriesMap = new Dictionary<Type, Repository>();
        }

        public void CreateAllRepositories()
        {
            this._repositoriesMap = this._sceneConfig.CreateAllRepositories();
        }

        public void InitializeAllRepositories()
        {
            var allRepositories = this._repositoriesMap.Values;

            foreach (var repository in allRepositories)
            {
                repository.Initialize();
            }
        }

        public void SendOnCreateToAllRepositories()
        {
            var allRepositories = this._repositoriesMap.Values;
            foreach (var repository in allRepositories)
            {
                repository.OnCreate();
            }
        }

        public void SendOnStartToAllRepositories()
        {
            var allRepositories = this._repositoriesMap.Values;
            foreach (var repository in allRepositories)
            {
                repository.OnStart();
            }
        }

        public T GetRepository<T>() where T : Repository
        {
            Type type = typeof(T);
            return (T)this._repositoriesMap[type];
        }
    }
}
