using UnityEngine;

namespace Assets.Scripts.Resize
{
    public class ResizableObject : MonoBehaviour
    {
        private const float BORDER_WIDTH = 0.2f;
        private Vector2 _setupScreen = new Vector2(1080, 1920);
        private UnityEngine.Camera _camera;

        private void Start()
        {
            _camera = UnityEngine.Camera.main;
            ResizeObjectsOnScreen();
        }

        private void ResizeObjectsOnScreen()
        {
            float horizontal = Screen.width / _setupScreen.x;
            float vertical = Screen.height / _setupScreen.y;
            Vector3 localScale = this.transform.localScale;
            if (horizontal != vertical)
            this.transform.localScale = new Vector2(horizontal - BORDER_WIDTH, localScale.y);
        }
    }
}
