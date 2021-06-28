using Assets.Scripts.Abstracts.Repository;
using UnityEngine;

namespace Assets.Scripts.LifeSystem
{
    public class LifeRepository : Repository
    {
        private const string KEY = "LIFE_KEY";

        public int Lifes { get; set; }

        public override void Initialize()
        {
            this.Lifes = PlayerPrefs.GetInt(KEY, 0);
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(KEY, this.Lifes);
        }
    }
}
