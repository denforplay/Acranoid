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

        public Button GetNewInstance(LevelPackObject levelPackObject, LevelPackObject nextLevelPackObject, Action btnAction)
        {
            Button button = GameObject.Instantiate(Prefab, _scrollView.transform);
            button.gameObject.AddComponent<ButtonAnimation>();
            Image btnImage = button.GetComponentsInChildren<Image>()[1];
            btnImage.sprite = levelPackObject._packImage;
            TextMeshProUGUI[] btnTexts = button.GetComponentsInChildren<TextMeshProUGUI>();
            btnTexts[0].text = levelPackObject.packName;
            btnTexts[0].gameObject.AddComponent<LocalisationObject>();
            btnTexts[1].text = $"{LevelManager.GetInstance.GetPackProgress(levelPackObject)}/{LevelManager.GetInstance.GetPackCount(levelPackObject)}";
            button.onClick.AddListener(() => btnAction.Invoke());
            
            if (nextLevelPackObject != null)
            {
                int nextPackProgress = LevelManager.GetInstance.GetPackProgress(nextLevelPackObject);
                int nextPackCount = LevelManager.GetInstance.GetPackCount(nextLevelPackObject);
                if (nextPackProgress < nextPackCount)
                {

                    button.interactable = false;
                }
            }
            return button;
        }
    }
}
