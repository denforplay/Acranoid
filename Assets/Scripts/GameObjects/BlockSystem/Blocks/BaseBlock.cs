using Assets.Scripts.Abstracts.Pool.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public abstract class BaseBlock : MonoBehaviour, IPoolable
    {
        public List<Sprite> _sprites = new List<Sprite>();
        protected int _score;
        public SpriteRenderer _spriteRenderer;
        protected int _life;
        public IObjectPool Origin { get ; set; }

        public void SetData(BlockConfig blockConfig)
        {
            _sprites = new List<Sprite>(blockConfig._sprites);
            _score = blockConfig.score;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _sprites.Last();
            _spriteRenderer.color = blockConfig.baseColor;
            _life = blockConfig._sprites.Count;
        }

        public abstract void ApplyDamage();

        public virtual void ReturnToPool()
        {
            this.gameObject.SetActive(false);
            this._life = _sprites.Count;
            this._spriteRenderer.sprite = _sprites.Last();
            this.gameObject.transform.localScale = Vector3.one;
        }
    }
}
