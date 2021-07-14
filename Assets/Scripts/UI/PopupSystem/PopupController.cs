using Assets.Scripts.Abstracts.Controller;
using Assets.Scripts.Abstracts.Game;
using System;

namespace Assets.Scripts.UI.PopupSystem
{
    public class PopupController : Controller
    {
        private PopupRepository _popupRepository;
        public override void OnCreate()
        {
            _popupRepository = Game.GetRepository<PopupRepository>();
        }
        public override void Initialize()
        {
            PopupManager.GetInstance.Initialize(this);
        }

        public Popup GetPopup(Type popupType)
        {
            return _popupRepository.popups.GetPrefabInstance(popupType);
        }
    }
}
