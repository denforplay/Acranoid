using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI.PopupSystem
{
    public class PopupConfig : ScriptableObject
    {
        [SerializeField] private List<Popup> _popups;
        public List<Popup> Popups => _popups;
    }
}
