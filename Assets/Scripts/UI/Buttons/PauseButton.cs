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

        private GameObject _stopGameGO;
        private void Start()
        {
            _stopGame.onClick.AddListener(PauseGame);
            _stopGameGO = _stopGame.gameObject;
        }

        public void PauseGame()
        {
            _stopGameGO.SetActive(false);
            PopupManager.GetInstance.SpawnPopup<PausePopup>();
            Time.timeScale = 0;
        }

        private void ReturnButton(IEvent ievent)
        {
            _stopGameGO.SetActive(true);
        }

        public void OnDisable()
        {
            EventBusManager.GetInstance.Subscribe<OnPausePopupClosedEvent>(ReturnButton);
        }

        public void OnEnable()
        {
            EventBusManager.GetInstance.Unsubscribe<OnPausePopupClosedEvent>(ReturnButton);
        }
    }
}
