using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Buttons
{
    public class NextLevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(LoadNextLevel);
        }

        private void LoadNextLevel()
        {
            LevelManager.GetInstance.LoadNextLevel();
            PopupSystem.PopupSystem.GetInstance.DeletePopUp();
        }
    }
}
