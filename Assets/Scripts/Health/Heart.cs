﻿using Assets.Scripts.Abstracts.Pool.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Health
{
    public class Heart : MonoBehaviour, IPoolable
    {
        public IObjectPool Origin { get; set; }

        public void ReturnToPool()
        {
            Origin.ReturnToPool(this);
        }
    }
}