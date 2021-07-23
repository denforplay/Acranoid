using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI.PopupSystem;
using Assets.Scripts.EventBus;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.EventBus.Events.Popup;

namespace Assets.Scripts.UI.Buttons
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _stopGame;
        private void Start()
        {
            EventBusManager.GetInstance.Subscribe<OnPausePopupClosedEvent>(ReturnButton);
            _stopGame.onClick.AddListener(PauseGame);
        }

        public void PauseGame()
        {
            _stopGame.interactable = false;
            PopupManager.GetInstance.SpawnPopup<PausePopup>();
        }

        private void ReturnButton(IEvent ievent)
        {
            _stopGame.interactable = true;
        }

        private void OnDestroy()
        {
            EventBusManager.GetInstance.Unsubscribe<OnPausePopupClosedEvent>(ReturnButton);
        }
    }
}
