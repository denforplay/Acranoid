using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private Button _button;
    [SerializeField] private float _downScale = 0.7f;
    [SerializeField] private float _upScale = 1.0f;
    [SerializeField] private float _duration = 0.1f;
    [SerializeField] private Color _endColor = new Color(100,100,100,255);

    private Color _startColor;

    private void Start()
    {
        _button = GetComponent<Button>();
        _startColor = _button.image.color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_button.enabled)
        {
            _button.transform.DOScale(_downScale, _duration);
            _button.image.DOColor(_endColor, _duration);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_button.enabled)
        {
            _button.transform.DOScale(_upScale, _duration);
            _button.image.DOColor(_startColor, _duration);
        }
    }
}