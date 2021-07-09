using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Scenes.SceneConfigs;

namespace Assets.Scripts.UI.Buttons
{
    public class PackageButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _buttonText;
        [SerializeField] private LevelPackObject _levelPackObject;
        private void Awake()
        {
            _buttonText.text = _levelPackObject.packName;
        }

        public void SetPackagesViewData()
        {
            LevelManager.GetInstance.SetLevelPackObject(_levelPackObject);
        }
    }
}
