using System.Collections.Generic;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class Block : MonoBehaviour, IPoolable
    {
        public static int count = 0;
        private List<Sprite> _sprites = new List<Sprite>();
        private int _score;
        private SpriteRenderer _spriteRenderer;
        private int _life;
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
                Destroy(gameObject);
            }
            else
            {
                _spriteRenderer.sprite = _sprites[_life - 1];
            }
        }

        private void OnEnable()
        {
            count++;
        }

        private void OnDisable()
        {
            count--;
        }

        public void ResetState()
        {
        }
    }
}
