using UnityEngine;
using TMPro;
using Assets.Scripts.Level;

namespace Assets.Scripts.UI.Text
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PackNameText : MonoBehaviour
    {
        private TextMeshProUGUI _packText;

        private void Awake()
        {
            _packText = GetComponent<TextMeshProUGUI>();
            _packText.text = LevelManager.GetInstance.CurrentPackName;
        }
    }
}
