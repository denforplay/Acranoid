using Assets.Scripts.BlockSystem.FactoryPattern;
using Assets.Scripts.Level;
using Assets.Scripts.Localisation;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Buttons.Factories
{
    public class PackageButtonFactory : IFactory<Button>
    {
        public Button Prefab { get; set; }

        private GameObject _scrollView;

        public PackageButtonFactory(Button prefab, GameObject scrollView)
        {
            Prefab = prefab;
            _scrollView = scrollView;
        }

        public Button GetNewInstance()
        {
            return null;
        }

        public Button GetNewInstance(LevelPackObject levelPackObject, Action btnAction)
        {
            Button button = GameObject.Instantiate(Prefab, _scrollView.transform);
            Image btnImage = button.GetComponentsInChildren<Image>()[1];
            btnImage.sprite = levelPackObject._packImage;
            TextMeshProUGUI[] btnTexts = button.GetComponentsInChildren<TextMeshProUGUI>();
            btnTexts[0].text = levelPackObject.packName;
            btnTexts[0].gameObject.AddComponent<LocalisationObject>();
            button.onClick.AddListener(() => btnAction.Invoke());
            return button;
        }
    }
}
