using Bar;
using UnityEngine;

namespace Ball
{
    /// <summary>
    /// Класс, управляющий поведением шара
    /// </summary>
    public class BallMovement : MonoBehaviour
    {
        /// <summary>
        /// Префаб эффекта столковения
        /// </summary>
        public GameObject Hit;

        /// <summary>
        /// Компонент физики мяча
        /// </summary>
        private Rigidbody _ballRigidBody;

        /// <summary>
        /// Скорость мяча (постоянная)
        /// </summary>
        private float _constBallSpeed = 7f;

        /// <summary>
        /// Начальное направление мяча
        /// </summary>
        private Vector3 _initialVelocity;

        private MeshRenderer _meshRenderer;

        public void InitBall(Vector3 velocity)
        {
            _constBallSpeed = velocity.magnitude;
            _initialVelocity = velocity;
        }

        private void Start()
        {
            _ballRigidBody = GetComponent<Rigidbody>();
            _meshRenderer = gameObject.GetComponent<MeshRenderer>();
            _ballRigidBody.velocity = _initialVelocity;
        }

        public void FixedUpdate()
        {
            //Контроль скорости мяча
            if (_ballRigidBody.velocity.magnitude != _constBallSpeed)
                _ballRigidBody.velocity = _ballRigidBody.velocity.normalized * _constBallSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bar"))
            {
                Rigidbody bar = collision.rigidbody;
                Rigidbody ball = gameObject.GetComponent<Rigidbody>();

                Vector3 forward = bar.transform.right;
                Vector3 left = bar.GetComponent<PlayerBar>().Track.Left;
                //направлеям вектор скорости платформы вдоль её оси движения
                Vector3 barSpeed = Vector3.Dot(bar.velocity, left) * left;
                Vector3 ballSpeed = ball.velocity;
                ball.velocity += barSpeed.normalized * Vector3.Dot(forward, ballSpeed);

                _meshRenderer.material.color =
                    collision.gameObject.GetComponent<MeshRenderer>().material.color;

                //спавн хита (эффект удара)
                GameObject hit = Instantiate(Hit, collision.contacts[0].point, Quaternion.identity);
                Destroy(hit, 0.5f);
            }
        }
    }
}