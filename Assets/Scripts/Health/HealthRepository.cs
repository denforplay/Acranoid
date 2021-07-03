﻿using Assets.Scripts.Abstracts.Repository;
using Assets.Scripts.Abstracts.Game;
using Assets.Scripts.Level;

namespace Assets.Scripts.Health
{
    public class HealthRepository : Repository
    {
        public int Health { get; set; }

        public override void Initialize()
        {
            this.Health = 3;
        }

        public override void Save()
        {
        }
    }
}
