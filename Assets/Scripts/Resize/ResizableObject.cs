using UnityEngine;
using Assets.Scripts.PlatformMovement;

namespace Assets.Scripts.Resize
{
    public class ResizableObject : MonoBehaviour
    {
        [SerializeField] PlatformMoveConfig _platformMoveConfig;
        [SerializeField] float _defaultBorderPosition = 3.5f;

        private void Start()
        {
            Resize();
        }

        private void Resize()
        {
            _platformMoveConfig.borderPosition = _defaultBorderPosition;
            if (Screen.height > 1920)
            {
                var localScale = this.gameObject.transform.localScale;
                float horizontalScale = 1920f / Screen.height;
                _platformMoveConfig.borderPosition *= horizontalScale;
                this.gameObject.transform.localScale = new Vector2(horizontalScale, localScale.y);
            }
        }
    }
}
