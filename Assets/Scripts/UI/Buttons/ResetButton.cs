using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Buttons
{
    public class ResetButton : MonoBehaviour
    {
        [SerializeField] private Button _resetButton;
        private void Awake()
        {
            _resetButton.onClick.AddListener(() => PlayerPrefs.DeleteAll());
        }
    }
}
