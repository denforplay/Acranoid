using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.Localisation;
using Assets.Scripts.Abstracts.Scene;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Scenes.SceneConfigs
{
    class GameSceneConfig : SceneConfig
    {
        public const string SCENE_NAME = "GameScene";
        public override string SceneName => SCENE_NAME;

        public override Dictionary<Type, Controller> CreateAllControllers()
        {
            var controllersMap = new Dictionary<Type, Controller>();
            return controllersMap;
        }

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type, Repository>();
            return repositoriesMap;
        }
    }
}
