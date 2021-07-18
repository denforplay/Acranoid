using UnityEngine;
using Assets.Scripts.UI.PopupSystem;

namespace Assets.Scripts.UI.Buttons
{
    public class StartGameButton : MonoBehaviour
    {
        public void LoadGameScene()
        {
            PopupManager.GetInstance.SpawnPopup<ChooseLevelPopup>();
        }
    }
}
