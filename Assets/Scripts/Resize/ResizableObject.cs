using UnityEngine;
using Assets.Scripts.PlatformMovement;

namespace Assets.Scripts.Resize
{
    public class ResizableObject : MonoBehaviour
    {
        public const float SCREEN_HEIGHT_CONFIG = 1920f;
        public const float SCREEN_WIDTH_CONFIG = 1080f;

        [SerializeField] PlatformMoveConfig _platformMoveConfig;
        [SerializeField] float _defaultBorderPosition = 3.4f;

        private void Start()
        {
            Resize();
        }

        private void Resize()
        {
            _platformMoveConfig.borderPosition = _defaultBorderPosition;
            if (Screen.height > SCREEN_HEIGHT_CONFIG)
            {
                var localScale = this.gameObject.transform.localScale;
                float horizontalScale = SCREEN_HEIGHT_CONFIG / Screen.height;
                _platformMoveConfig.borderPosition *= horizontalScale;
                this.gameObject.transform.localScale = new Vector2(horizontalScale, localScale.y);
            }
        }
    }
}
