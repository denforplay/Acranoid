using UnityEngine;

namespace Assets.Scripts.GameObjects.Borders
{
    public class BorderSizeChanger : MonoBehaviour
    {
        private Vector2 defaultResolution;
        private void Start()
        {
            defaultResolution = new Vector2(1080f, 1920f);
            gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite);
        }
    }
}
