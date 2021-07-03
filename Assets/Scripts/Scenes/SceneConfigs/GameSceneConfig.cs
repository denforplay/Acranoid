﻿using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.Localisation;
using Assets.Scripts.Level;
using Assets.Scripts.Health;
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
            this.CreateController<HealthController>(controllersMap);
            this.CreateController<LevelsController>(controllersMap);
            this.CreateController<LocalisationController>(controllersMap);
            return controllersMap;
        }

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type, Repository>();
            this.CreateRepository<HealthRepository>(repositoriesMap);
            this.CreateRepository<LevelRepository>(repositoriesMap);
            this.CreateRepository<LocalisationRepository>(repositoriesMap);
            return repositoriesMap;
        }
    }
}
