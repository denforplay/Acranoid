using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Scenes.SceneConfigs;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI.Buttons
{

    public class StartGameButton : MonoBehaviour
    {
        [SerializeField] private GameObject _scrollView;

        public void LoadGameScene()
        {
            _scrollView.SetActive(true);
        }
    }
}
