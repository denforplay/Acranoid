using Assets.Scripts.Abstracts.Controller;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Abstracts.Game;
using System.Linq;

namespace Assets.Scripts.Health
{
    public class HealthViewController : Controller
    {
        private List<Heart> _heartsView;
        private HealthRepository _healthRepository;
        private int _health => _healthRepository.Health;
        private GameObject _healthPanel;

        public override void OnCreate()
        {
            base.OnCreate();
            _healthRepository = Game.GetRepository<HealthRepository>();
        }

        public override void Initialize()
        {
            _heartsView = new List<Heart>();
            _healthPanel = HealthManager.GetInstance.HealthPanel;
            HealthManager.GetInstance.InitializeViewController(this);
        }

        public void DeleteAllHearts()
        {
            foreach (var heart in _heartsView)
            {
                _healthRepository._heartPool.ReturnToPool(heart);
            }
            _heartsView.Clear();
        }

        public void DeleteHealthView()
        {
            _heartsView.Last(x => x.gameObject.activeInHierarchy).ReturnToPool();
        }

        public void ViewHearts()
        {
            for (int i = 0; i < _healthRepository.Health; i++)
            {
                Heart heart = _healthRepository._heartPool.GetPrefabInstance();
                heart.transform.SetParent(_healthPanel.transform);
                heart.gameObject.transform.position = _healthPanel.transform.position;
                _heartsView.Add(heart);
            }
        }
    }
}
