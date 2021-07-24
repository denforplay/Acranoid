using Assets.Scripts.Level;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.ProgressBar
{
    public class PackProgressBar : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.5f;
        private Slider _slider;
        private void Awake()
        {
            _slider = gameObject.GetComponent<Slider>();
            _slider.maxValue = LevelManager.GetInstance.GetPackCount(LevelManager.GetInstance._levelPackObject);
            _slider.DOValue(LevelManager.GetInstance.GetPackProgress(LevelManager.GetInstance._levelPackObject), _duration);
        }
    }
}
