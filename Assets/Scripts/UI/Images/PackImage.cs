using Assets.Scripts.Level;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Images
{
    [RequireComponent(typeof(Image))]
    public class PackImage : MonoBehaviour
    {
        private Image _packImage;

        private void Awake()
        {
            _packImage = GetComponent<Image>();
            _packImage.sprite = LevelManager.GetInstance.CurrentPackSprite;
        }
    }
}
