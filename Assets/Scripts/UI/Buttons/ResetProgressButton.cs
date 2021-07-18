using Assets.Scripts.EnergySystem.Energy;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Buttons
{
    public class ResetProgressButton : MonoBehaviour
    {
        [SerializeField] private Button _resetProgressButton;

        private void Awake()
        {
            _resetProgressButton.onClick.AddListener(() => EnergyManager.GetInstance.SpendEnergy(1));
        }
    }
}
