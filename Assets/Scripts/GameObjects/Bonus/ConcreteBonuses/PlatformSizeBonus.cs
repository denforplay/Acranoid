using UnityEngine;
using Assets.Scripts.PlatformMovement;

namespace Assets.Scripts.GameObjects.Bonus.ConcreteBonuses
{
    public class PlatformSizeBonus : BaseBonus
    {
        [SerializeField] private bool _isIncrease = true;
        [SerializeField] private float _size = 0.1f;
        public override void Apply()
        {
            StartTimer();
            SetSize(_isIncrease ? _size : -_size);
            this.gameObject.SetActive(false);
        }

        public override void Remove()
        {
            SetSize(_isIncrease ? -_size : _size);
        }

        public void SetSize(float size)
        {
            Platform platform = BonusManager.GetInstance.Platform;
                if (platform.TryGetComponent<SpriteRenderer>(out SpriteRenderer sprite))
                {
                    sprite.size = new Vector2(sprite.size.x + size, sprite.size.y);
                }
                if (platform.TryGetComponent<BoxCollider2D>(out BoxCollider2D collider2D))
            {
                collider2D.size = new Vector2(collider2D.size.x + size, collider2D.size.y);
            }
        }
    }
}
