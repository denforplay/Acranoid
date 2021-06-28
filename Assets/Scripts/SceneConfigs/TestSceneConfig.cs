using System;
using System.Collections.Generic;
using Assets.Scripts.LifeSystem;
using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.Abstracts.Scene;

namespace Assets.Scripts.SceneConfigs
{
    public class TestSceneConfig : SceneConfig
    {
        public const string SCENE_NAME = "SampleScene";
        public override string SceneName => SCENE_NAME;

        public override Dictionary<Type, Controller> CreateAllControllers()
        {
            var interactorsMap = new Dictionary<Type, Controller>();
            this.CreateController<LifeController>(interactorsMap);
            return interactorsMap;
        }

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type, Repository>();
            this.CreateRepository<LifeRepository>(repositoriesMap);
            return repositoriesMap;
        }
    }
}
