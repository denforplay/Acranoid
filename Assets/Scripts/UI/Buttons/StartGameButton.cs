using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Scenes.SceneConfigs;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Assets.Scripts.UI.Buttons
{
    public class StartGameButton : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Game.sceneManagerBase.LoadNewSceneAsync(GameSceneConfig.SCENE_NAME);
        }
    }
}
