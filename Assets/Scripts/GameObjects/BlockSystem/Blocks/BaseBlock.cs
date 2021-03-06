using Assets.Scripts.Abstracts.Pool.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public abstract class BaseBlock : MonoBehaviour, IPoolable
    {
        public List<Sprite> _sprites = new List<Sprite>();
        public SpriteRenderer _spriteOnBlock;
        public SpriteRenderer _spriteRenderer;
        protected int _life;
        public IObjectPool Origin { get ; set; }

        public void SetData(BlockConfig blockConfig)
        {
            _sprites = new List<Sprite>(blockConfig._sprites);
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _sprites.Last();
            _spriteRenderer.color = blockConfig.baseColor;
            _life = blockConfig._sprites.Count;
        }

        public abstract void ApplyDamage(int value);

        public virtual void ReturnToPool()
        {
            _spriteOnBlock.sprite = null;
            this.gameObject.SetActive(false);
            this._life = _sprites.Count;
            this._spriteRenderer.sprite = _sprites.Last();
            this.gameObject.transform.localScale = Vector3.one;
        }
    }
}
