using Assets.Scripts.BlockSystem.FactoryPattern;
using Assets.Scripts.Localisation;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Buttons.Factories
{
    public class LevelButtonFactory : IFactory<Button>
    {
        public Button Prefab { get; set; }

        private GameObject _scrollView;

        public LevelButtonFactory(Button prefab, GameObject scrollView)
        {
            Prefab = prefab;
            _scrollView = scrollView;
        }

        public Button GetNewInstance()
        {
            return null;
        }

        public Button GetNewInstance(Level.Level currentLevel, Level.Level nextLevel, Action btnAction)
        {
            Button button = GameObject.Instantiate(Prefab, _scrollView.transform);
            button.gameObject.AddComponent<ButtonAnimation>();
            TextMeshProUGUI text = button.GetComponentInChildren<TextMeshProUGUI>();
            text.text = currentLevel.levelName;
            text.gameObject.AddComponent<LocalisationObject>();
            button.onClick.AddListener(() => btnAction.Invoke());
            if (nextLevel != null && !nextLevel.isCompleted)
            {
                button.interactable = false;
            }
            return button;
        }
    }
}
