﻿using Assets.Scripts.Abstracts.Pool.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public abstract class BaseBlock : MonoBehaviour, IPoolable
    {
        protected List<Sprite> _sprites = new List<Sprite>();
        protected int _score;
        protected SpriteRenderer _spriteRenderer;
        protected int _life;

        public IObjectPool Origin { get ; set; }

        public void SetData(BlockConfig blockConfig)
        {
            _sprites = new List<Sprite>(blockConfig._sprites);
            _score = blockConfig.score;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = blockConfig.baseColor;
            _life = blockConfig._sprites.Count;
        }

        public abstract void ApplyDamage();

        public void ReturnToPool()
        {
            this.gameObject.transform.localScale = Vector3.one;
            Origin.ReturnToPool(this);
        }
    }
}
