using System;
using System.Collections.Generic;
namespace Assets.Scripts.Abstracts.Scene
{
    public abstract class SceneConfig
    {
        public abstract Dictionary<Type, Repository.Repository> CreateAllRepositories();
        public abstract Dictionary<Type, Controller.Controller> CreateAllControllers();
        public abstract string SceneName { get; }
        public void CreateController<T>(Dictionary<Type, Controller.Controller>  controllersMap) where T : Controller.Controller, new()
        {
            T controller = new T();
            Type type = typeof(T);
            controllersMap[type] = controller;
        }

        public void CreateRepository<T>(Dictionary<Type, Repository.Repository> repositoriesMap) where T : Repository.Repository, new()
        {
            T repository = new T();
            Type type = typeof(T);
            repositoriesMap[type] = repository;
        }
    }
}