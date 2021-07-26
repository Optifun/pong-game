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

        /// <summary>
        /// Скорость мяча (постоянная)
        /// </summary>
        public float ConstBallSpeed = 7f;

        /// <summary>
        /// Компонент физики мяча
        /// </summary>
        public Rigidbody BallRigidBody;

        public MeshRenderer MeshRenderer;

        private void Start()
        {
            BallRigidBody = GetComponent<Rigidbody>();
            MeshRenderer = GetComponent<MeshRenderer>();
        }

        public void Launch(Vector3 velocity)
        {
            ConstBallSpeed = velocity.magnitude;
            BallRigidBody.velocity = velocity;
        }

        public void FixedUpdate()
        {
            //Контроль скорости мяча
            if (Math.Abs(BallRigidBody.velocity.magnitude - ConstBallSpeed) > SpeedTolerance)
                BallRigidBody.velocity = BallRigidBody.velocity.normalized * ConstBallSpeed;
        }
    }
}