using Assets.Scripts.BlockSystem.FactoryPattern;
using Assets.Scripts.UI.PopupSystem;
using UnityEngine;
using System.Linq;
using System;

namespace Assets.Scripts.GameObjects.BlockSystem.FactoryPattern
{
    public class PopupFactory<T> : IFactory<T> where T : Popup
    {
        public Type PopupType { get; set; }
        public T Prefab { get; set; }

        private PopupConfig _popupConfig;

        public PopupFactory (PopupConfig popupConfig)
        {
            _popupConfig = popupConfig;
        }

        public T GetNewInstance()
        {
            Prefab = (T)_popupConfig.Popups.Find(x => x.GetType() == PopupType);
            T instance = GameObject.Instantiate(Prefab);
            return instance;
        }
    }
}
