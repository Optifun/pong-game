using System;
using Bar;
using UnityEngine;

namespace Ball
{
    /// <summary>
    /// Класс, управляющий поведением шара
    /// </summary>
    [RequireComponent(typeof(BallCollision))]
    public class BallMovement : MonoBehaviour
    {
        private const float SpeedTolerance = 0.05f;

        public Rigidbody Rigidbody;
        public BallCollision Collision;

        /// <summary>
        /// Скорость мяча (постоянная)
        /// </summary>
        private float ConstBallSpeed;

        private void Start()
        {
            Collision.PlayerCollided += AccelerateBall;
        }

        public void FixedUpdate()
        {
            //Контроль скорости мяча
            if (Math.Abs(Rigidbody.velocity.magnitude - ConstBallSpeed) > SpeedTolerance)
                Rigidbody.velocity = Rigidbody.velocity.normalized * ConstBallSpeed;
        }

        private void OnDestroy()
        {
            Collision.PlayerCollided -= AccelerateBall;
        }

        public void Launch(Vector3 velocity)
        {
            ConstBallSpeed = velocity.magnitude;
            Rigidbody.velocity = velocity;
        }

        private void AccelerateBall(PlayerBar bar)
        {
            var barPysics = bar.GetComponent<Rigidbody>();

            Vector3 barLookDirection = bar.transform.forward;
            Vector3 barMovement = barPysics.velocity.normalized;

            Vector3 horizontalAcceleration = Vector3.Project(Rigidbody.velocity, barMovement);
            Vector3 verticalAcceleration = -Vector3.Project(Rigidbody.velocity, barLookDirection);

            Rigidbody.velocity = Rigidbody.velocity + (horizontalAcceleration + verticalAcceleration);
        }
    }
}