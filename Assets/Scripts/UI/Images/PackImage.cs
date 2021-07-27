using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.LevelEvents;
using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Images
{
    [RequireComponent(typeof(Image))]
    public class PackImage : MonoBehaviour
    {
        private Image _packImage;

        private void Awake()
        {
            _packImage = GetComponent<Image>();
            ChangePackImage(null);
            EventBusManager.GetInstance.Subscribe<OnPackChangedEvent>(ChangePackImage);
        }

        public void ChangePackImage(IEvent ievent)
        {
            if (_packImage != null)
            _packImage.sprite = LevelManager.GetInstance.CurrentPackSprite;
        }
    }
}
