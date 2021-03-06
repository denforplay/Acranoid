using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.Localisation;
using Assets.Scripts.Health;
using Assets.Scripts.Abstracts.Scene;
using System;
using System.Collections.Generic;
using Assets.Scripts.Block;
using Assets.Scripts.EnergySystem.Timer;
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.GameObjects.ParticleSystem;

namespace Assets.Scripts.Scenes.SceneConfigs
{
    class GameSceneConfig : SceneConfig
    {
        public const string SCENE_NAME = "GameScene";
        public override string SceneName => SCENE_NAME;

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type, Repository>();
            this.CreateRepository<BlocksRepository>(repositoriesMap);
            this.CreateRepository<TimerRepository>(repositoriesMap);
            this.CreateRepository<EnergyRepository>(repositoriesMap);
            this.CreateRepository<HealthRepository>(repositoriesMap);
            this.CreateRepository<LocalisationRepository>(repositoriesMap);
            this.CreateRepository<ParticleRepository>(repositoriesMap);
            return repositoriesMap;
        }

        public override Dictionary<Type, Controller> CreateAllControllers()
        {
            var controllersMap = new Dictionary<Type, Controller>();
            this.CreateController<BlocksController>(controllersMap);
            this.CreateController<TimerController>(controllersMap);
            this.CreateController<EnergyController>(controllersMap);
            this.CreateController<HealthController>(controllersMap);
            this.CreateController<LocalisationController>(controllersMap);
            this.CreateController<ParticleController>(controllersMap);
            return controllersMap;
        }
    }
}
