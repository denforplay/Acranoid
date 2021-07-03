using Assets.Scripts.Level;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Health
{
    public class HealthManager : MonoBehaviour
    {
        public static event Action OnHealthInitializedEvent;
        public event Action OnHeartSpendEvent;
        public static HealthManager instance;
        [SerializeField] HeartConfig _heartConfig;
        List<GameObject> hearts = new List<GameObject>();
        public bool IsInitialized { get; private set; }
        private HealthController _healthController;

        public int Health
        {
            get
            {
                CheckHeartsInitialized();
                return _healthController.Health;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            OnHealthInitializedEvent += () =>
            {
                EventAdder();
            };
            DontDestroyOnLoad(gameObject);
        }

        private void ShowHealth()
        {
            CheckHeartsInitialized();
            float posX = Screen.width - _heartConfig.borderPosition;
            float posY = Screen.height - _heartConfig.borderPosition;
            for (int i = 0; i < Health; i++)
            {
                GameObject heart = Instantiate(_heartConfig.heartPrefab);
                Vector2 heartPosition = Camera.main.ScreenToWorldPoint(new Vector3(posX - i * _heartConfig.heartSize, posY));
                heart.transform.position = heartPosition;
                heart.TryGetComponent(out SpriteRenderer spriteRenderer);
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = _heartConfig.activeHeart;
                }

                hearts.Add(heart);
            }
        }

        public void SpendHeart(int value)
        {
            CheckHeartsInitialized();
            if (_healthController.IsEnoughLifes(0))
            {
                DeleteHealthView();
                OnHeartSpendEvent?.Invoke();
            }
            _healthController.SpendLife(value);
        }

        public void InitializeHealthController(HealthController healthController)
        {
            _healthController = healthController;
            IsInitialized = true;
            OnHealthInitializedEvent?.Invoke();
        }

        public void DeleteHealthView()
        {
            CheckHeartsInitialized();
            for (int i = 0; i < hearts.Count; i++)
            {
                hearts[i].TryGetComponent(out SpriteRenderer spriteRender);
                if (spriteRender.sprite == _heartConfig.activeHeart)
                {
                    spriteRender.sprite = _heartConfig.inActiveHeart;
                    break;
                }
            }
        }

        private void EventAdder()
        {
            LevelManager.OnLevelsInitialized += ShowHealth;
        }

        private void CheckHeartsInitialized()
        {
            if (!IsInitialized)
            {
                throw new ArgumentNullException("Hearts are not initialized yet");
            }
        }
    }
}
