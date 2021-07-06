using UnityEngine;
using Assets.Scripts.Health;
using Assets.Scripts.Abstracts.Pool.Interfaces;
using Assets.Scripts.Abstracts.Pool;
using Assets.Scripts.Level;

namespace Assets.Scripts.BallMovement
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private BallConfig _ballConfig;
        private BallInput _ballInput;
        private BallCollisions _ballCollisions;
        private Rigidbody2D _rigidbody2D;
        private CircleCollider2D _circleCollider2D;
        private GameObject _rememberedParent;
        private void Start()
        {
            _circleCollider2D = GetComponent<CircleCollider2D>();
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
            Vector3 positon = _rememberedParent.transform.position;
            transform.position = new Vector3(positon.x, positon.y + _circleCollider2D.radius * 2, positon.z);
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
            HealthManager.GetInstance.SpendHeart(1);
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
                HealthManager.GetInstance.OnHeartSpendEvent += ReturnBallOnPosition;
            };

            LevelManager.OnNextLevelLoaded += ReturnBallOnPosition;
        }

        private void OnDisable()
        {
            BallInput.OnBallActivatingEvent -= BallActivate;
            LevelManager.OnNextLevelLoaded -= ReturnBallOnPosition;
        }
    }
}