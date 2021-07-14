using UnityEngine;

namespace Assets.Scripts.Borders
{
    public class BorderPositionChanger : MonoBehaviour
    {
        [SerializeField] private GameObject _leftBorder;
        [SerializeField] private GameObject _rightBorder;
        [SerializeField] private GameObject _topBorder;
        [SerializeField] private Camera _camera;

        private void Start()
        {
            Vector2 screen = new Vector2(Screen.width, Screen.height);
            var leftRightSprite = _leftBorder.gameObject.GetComponent<SpriteRenderer>();
            var topSprite = _topBorder.gameObject.GetComponent<SpriteRenderer>();
            screen = _camera.ScreenToWorldPoint(screen);
            var leftBorderPos = new Vector2(-screen.x + leftRightSprite.size.x / 2, 0);
            var rightBorderPos = new Vector2(screen.x - leftRightSprite.size.x / 2, 0);
            var topBorderPos = new Vector2(0, screen.y - topSprite.size.y / 2);
            _leftBorder.transform.position = leftBorderPos;
            _rightBorder.transform.position = rightBorderPos;
            _topBorder.transform.position = topBorderPos;
        }
    }
}
