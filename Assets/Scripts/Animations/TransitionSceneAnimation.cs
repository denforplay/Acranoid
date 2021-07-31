using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.Abstracts.Scene;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.EventBus;
using Assets.Scripts.EventBus.Events.SceneEvents;
using Assets.Scripts.Abstracts.EventBus.Interfaces;
using Assets.Scripts.Scenes.SceneConfigs;
using UnityEngine.SceneManagement;
using Assets.Scripts.UI.PopupSystem;

namespace Assets.Scripts.Animations
{
    public class TransitionSceneAnimation : MonoBehaviour
    {
        [SerializeField] private Image _fader;
        [SerializeField] private float _duration;
        private void Awake()
        {
            EventBusManager.GetInstance.Subscribe<OnLoadGameScene>(LoadGameScene);
            Time.timeScale = 0;
            _fader.enabled = true;
            _fader.DOColor(Color.black, 0);
            _fader.DOColor(Color.clear, _duration).OnComplete(() =>
            {
                _fader.enabled = false;
                Time.timeScale = 1;
            });
        }

        public void LoadGameScene(IEvent ievent)
        {
            _fader.enabled = true;
            _fader.DOColor(Color.clear, 0);
            _fader.DOColor(Color.black, _duration).OnComplete(() =>
            {
                Game.sceneManagerBase.LoadNewSceneAsync(GameSceneConfig.SCENE_NAME);
            });
        }

        private void OnDestroy()
        {
            EventBusManager.GetInstance.Unsubscribe<OnLoadGameScene>(LoadGameScene);
            DOTween.Kill(_fader);
        }
    }
}
