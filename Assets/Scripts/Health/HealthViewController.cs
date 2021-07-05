using Assets.Scripts.Abstracts.Controller;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Abstracts.Pool;

namespace Assets.Scripts.Health
{
    public class HealthViewController : Controller
    {
        private List<GameObject> _heartsView;
        private HeartConfig _heartConfig;
        private HealthRepository _healthRepository;
        private int _health => _healthRepository.Health;

        public override void OnCreate()
        {
            base.OnCreate();
            _healthRepository = Game.GetRepository<HealthRepository>();
        }

        public override void Initialize()
        {
            _heartsView = new List<GameObject>();
            _heartConfig = HealthManager.instance.HeartConfig;
            HealthManager.instance.InitializeViewController(this);
        }

        public void DeleteAllHearts()
        {
            foreach (var item in _heartsView)
            {
                GameObject.Destroy(item.gameObject);
            }
            _heartsView.Clear();
        }

        public void DeleteHealthView()
        {
            for (int i = 0; i < _heartsView.Count; i++)
            {
                _heartsView[i].TryGetComponent(out SpriteRenderer spriteRender);
                if (spriteRender.sprite == _heartConfig.activeHeart)
                {
                    spriteRender.sprite = _heartConfig.inActiveHeart;
                    break;
                }
            }
        }

        public void ViewHearts()
        {
            float posX = Screen.width - _heartConfig.borderPosition;
            float posY = Screen.height - _heartConfig.borderPosition;
            for (int i = 0; i < _healthRepository.Health; i++)
            {
                GameObject heart = HealthManager.instance.CreateHeart();
                Vector2 heartPosition = Camera.main.ScreenToWorldPoint(new Vector3(posX - i * _heartConfig.heartSize, posY));
                heart.transform.position = heartPosition;
                heart.TryGetComponent(out SpriteRenderer spriteRenderer);
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = _heartConfig.activeHeart;
                }

                _heartsView.Add(heart);
            }
        }
    }
}
