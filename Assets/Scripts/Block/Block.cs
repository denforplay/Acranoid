using System.Collections.Generic;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class Block : MonoBehaviour, IPoolable
    {
        private List<Sprite> _sprites = new List<Sprite>();
        private int _score;
        private SpriteRenderer _spriteRenderer;
        private int _life;

        public IObjectPool Origin { get; set; }

        public void SetData(BlockConfig blockConfig)
        {
            _sprites = new List<Sprite>(blockConfig._sprites);
            _score = blockConfig.score;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = blockConfig.baseColor;
            _life = blockConfig._sprites.Count;
        }

        public void ApplyDamage()
        {
            _life--;
            if (_life < 1)
            {
                BlocksManager.instance.ReturnBlock(this);
            }
            else
            {
                _spriteRenderer.sprite = _sprites[_life - 1];
            }
        }

        public void Prepare()
        {
        }

        public void ReturnToPool()
        {
            Origin.ReturnToPool(this);
        }
    }
}
