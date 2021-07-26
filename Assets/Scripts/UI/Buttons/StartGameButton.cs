using UnityEngine;
using Assets.Scripts.UI.PopupSystem;
using Assets.Scripts.UI.PopupSystem.ConcretePopups;

namespace Assets.Scripts.UI.Buttons
{
    public class StartGameButton : MonoBehaviour
    {
        public void LoadGameScene()
        {
            PopupManager.GetInstance.SpawnPopup<ChoosePackagePopup>();
        }
    }
}
