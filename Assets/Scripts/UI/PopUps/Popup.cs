using Assets.Scripts.UI.PopUps.Interfaces;

namespace Assets.Scripts.UI.PopUps
{
    public class Popup : IPopupConfig
    {
        private string _title;
        private string _text;
        private string _buttonRightText;
        private string _buttonLeftText;

        public string Title => _title;
        public string Text => _text;
        public string ButtonRightText => _buttonRightText;
        public string ButtonLeftText => _buttonLeftText;

        public Popup(string title, string text, string buttonRightText, string buttonLeftText)
        {
            _title = title;
            _text = text;
            _buttonRightText = buttonRightText;
            _buttonLeftText = buttonLeftText;
        }
    }
}
