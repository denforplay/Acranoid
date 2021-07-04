using UnityEngine;
using Assets.Scripts.Health;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.Abstracts.Pool;

namespace Assets.Scripts.BallMovement
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallConfig _ballConfig;
        private BallInput _ballInput;
        private BallCollisions _ballCollisions;
        private Rigidbody2D _rigidbody2D;
        private GameObject _rememberedParent;
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            _ballCollisions = new BallCollisions(_ballConfig, _rigidbody2D);
            _ballInput = new BallInput();
        }

        private void Update()
        {
            _ballInput.CheckInput();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            _ballCollisions.Call(collision);
        }

        public void ReturnBallOnPosition()
        {
            this.gameObject.SetActive(true);
            transform.SetParent(_rememberedParent.transform);
            transform.position = _rememberedParent.transform.position;
        }

        private void BallActivate()
        {
            _rememberedParent = transform.parent.gameObject;
            transform.SetParent(null);
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody2D.AddForce(new Vector2(_ballConfig.offSetX, _ballConfig.force));
        }

        private void BallInactivate()
        {
            this.gameObject.SetActive(false);
            HealthManager.instance.SpendHeart(1);
        }

        private void OnBecameInvisible()
        {
            BallInactivate();
        }

        private void OnEnable()
        {
            BallInput.OnBallActivatingEvent += BallActivate;
            HealthManager.OnHealthInitializedEvent += () =>
            {
                HealthManager.instance.OnHeartSpendEvent += ReturnBallOnPosition;
            };
        }
        private void OnDisable()
        {
            BallInput.OnBallActivatingEvent -= BallActivate;
        }

        public void ResetState()
        {
            throw new System.NotImplementedException();
        }
    }
}