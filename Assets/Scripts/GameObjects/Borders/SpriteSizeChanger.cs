using UnityEngine;

namespace Assets.Scripts.GameObjects.Borders
{

    public class SpriteSizeChanger : MonoBehaviour
    {
        private Vector2 defaultResolution;
        private void Start()
        {
            defaultResolution = new Vector2(1080f, 1920f);
            var scale = new Vector3(defaultResolution.x / Screen.width, defaultResolution.y / Screen.height, 1f);
            gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite);
            sprite.transform.localScale = Vector3.Scale(sprite.transform.localScale, scale);
        }
    }
}
