using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.Localisation;
using Assets.Scripts.View;
using Assets.Scripts.Abstracts.Scene;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Scenes.SceneConfigs
{
    public class StartSceneConfig : SceneConfig
    {
        public const string SCENE_NAME = "StartScene";
        public override string SceneName => SCENE_NAME;

        public override Dictionary<Type, Controller> CreateAllControllers()
        {
            var controllersMap = new Dictionary<Type, Controller>();
            this.CreateController<LocalisationController>(controllersMap);
            return controllersMap;
        }

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type, Repository>();
            this.CreateRepository<LocalisationRepository>(repositoriesMap);
            return repositoriesMap;
        }
    }
}
