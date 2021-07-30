using UnityEngine;

namespace Assets.Scripts.GameObjects
{
    public class ResizePhysicalObject : MonoBehaviour
    {
        private Vector2 _screenResolution = new Vector2(1080f, 1920f);
        private void Awake()
        {
            if (TryGetComponent(out SpriteRenderer sprite))
            {
                sprite.size = new Vector2(sprite.size.x *  _screenResolution.x / Screen.width, sprite.size.y * _screenResolution.y / Screen.height);
            }

            if (TryGetComponent(out BoxCollider2D boxCollider))
            {
                boxCollider.size = new Vector2(boxCollider.size.x * _screenResolution.x / Screen.width, boxCollider.size.y * _screenResolution.y / Screen.height);
            }
        }
    }
}
