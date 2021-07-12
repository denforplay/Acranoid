using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI.PopupSystem;
namespace Assets.Scripts.UI.Buttons
{
    public class PauseButton : MonoBehaviour
    {
        [SerializeField] private Button _continueGame;
        [SerializeField] private Button _stopGame;

        private void Awake()
        {
            _stopGame.onClick.AddListener(PauseGame);
            _continueGame.onClick.AddListener(ContinueGame);
        }

        public void PauseGame()
        {
            _stopGame.gameObject.SetActive(false);
            _continueGame.gameObject.SetActive(true);
            PopupSystem.PopupSystem.GetInstance.SpawnPopup(typeof(PausePopup));
            Time.timeScale = 0;
        }

        public void ContinueGame()
        {
            PopupSystem.PopupSystem.GetInstance.DeletePopUp();
            _stopGame.gameObject.SetActive(true);
            _continueGame.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
