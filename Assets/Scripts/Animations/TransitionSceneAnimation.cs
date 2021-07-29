using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Assets.Scripts.Abstracts.Scene;
using Assets.Scripts.Abstracts.Game;

namespace Assets.Scripts.Animations
{
    public class TransitionSceneAnimation : MonoBehaviour
    {
        [SerializeField] private Image _fader;
        [SerializeField] private float _duration;
        private void Awake()
        {
            Time.timeScale = 0;
            _fader.enabled = true;
            _fader.DOColor(Color.black, 0);
            _fader.DOColor(Color.clear, _duration).OnComplete(() =>
            {
                _fader.enabled = false;
                Time.timeScale = 1;
            });
        }

        public void LoadNextScene(string nextSceneName)
        {
            _fader.DOColor(Color.clear, _duration);
            _fader.DOColor(Color.black, _duration).OnComplete(() =>
            {
                Game.sceneManagerBase.LoadNewSceneAsync(nextSceneName);
            });
        }

        private void OnDestroy()
        {
            DOTween.Kill(_fader);
        }
    }
}
