using System.Collections.Generic;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class ColorBlock : BaseBlock, IPoolable
    {
        public override void ApplyDamage()
        {
            _life--;
            if (_life < 1)
            {
                BlocksManager.GetInstance.ReturnBlock(this);
            }
            else
            {
                _spriteRenderer.sprite = _sprites[_life - 1];
            }
        }
    }
}
