using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.Popup;
using Assets.Scripts.GameObjects.BlockSystem.FactoryPattern;

namespace Assets.Scripts.UI.PopupSystem
{
    public class PopupRepository : Repository
    {
        public PopupPool<Popup> popups;
        public override void Initialize()
        {
            EventBusManager.GetInstance.Subscribe<OnPopupManagerInitializedEvent>((OnPopupManagerInitializedEvent) =>
            {
                popups = new PopupPool<Popup>(new PopupFactory<Popup>(PopupManager.GetInstance.PopupConfig));
            });
        }
    }
}
