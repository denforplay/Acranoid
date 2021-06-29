using System;
using System.Collections.Generic;
using Assets.Scripts.Abstracts.Scene;

namespace Assets.Scripts.Abstracts.Controller
{
    public class ControllersBase
    {
        private Dictionary<Type, Controller> _controllersMap;
        private SceneConfig _sceneConfig;
        public ControllersBase(SceneConfig sceneConfig)
        {
            _sceneConfig = sceneConfig;
        }

        public void CreateAllControllers()
        {
            this._controllersMap = this._sceneConfig.CreateAllControllers();
        }

        public void InitializeAllControllers()
        {
            var allControllers = this._controllersMap.Values;
            foreach (var controller in allControllers)
            {
                controller.Initialize();
            }
        }

        public void SendOnCreateToAllControllers()
        {
            var allControllers = this._controllersMap.Values;
            foreach (var controller in allControllers)
            {
                controller.OnCreate();
            }
        }

        public void SendOnStartToAllControllers()
        {
            var allControllers = this._controllersMap.Values;
            foreach (var controller in allControllers)
            {
                controller.OnStart();
            }
        }

        public T GetController<T>() where T : Controller
        {
            Type type = typeof(T);
            return (T)this._controllersMap[type];
        }
    }
}
