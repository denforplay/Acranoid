using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.Localisation;
using Assets.Scripts.Abstracts.Scene;
using System;
using System.Collections.Generic;
using Assets.Scripts.Level;
using Assets.Scripts.Health;
using Assets.Scripts.EnergySystem.Energy;
using Assets.Scripts.EnergySystem.Timer;

namespace Assets.Scripts.Scenes.SceneConfigs
{
    public class StartSceneConfig : SceneConfig
    {
        public const string SCENE_NAME = "StartScene";
        public override string SceneName => SCENE_NAME;

        private bool isLocalisationInit1 = false;
        private bool isLocalisationInit2 = false;
        public override Dictionary<Type, Controller> CreateAllControllers()
        {
            var controllersMap = new Dictionary<Type, Controller>();
            this.CreateController<TimerController>(controllersMap);
            this.CreateController<EnergyController>(controllersMap);
            if (!isLocalisationInit1)
            {
                this.CreateController<LocalisationController>(controllersMap);
                isLocalisationInit1 = true;
            }
            this.CreateController<LevelsController>(controllersMap);
            return controllersMap;
        }

        public override Dictionary<Type, Repository> CreateAllRepositories()
        {
            var repositoriesMap = new Dictionary<Type, Repository>();
            if (!isLocalisationInit2)
            {
                this.CreateRepository<LocalisationRepository>(repositoriesMap);
                isLocalisationInit2 = true;
            }
            this.CreateRepository<LevelRepository>(repositoriesMap);
            return repositoriesMap;
        }
    }
}
