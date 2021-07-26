using System;
using Bar;
using UnityEngine;

namespace Ball
{
    [RequireComponent(typeof(Collider))]
    public class BallCollision : MonoBehaviour
    {
        public event Action<PlayerBar> PlayerCollided;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bar"))
            {
                Rigidbody bar = collision.rigidbody;
                Rigidbody ball = gameObject.GetComponent<Rigidbody>();
                PlayerBar playerBar = collision.gameObject.GetComponent<PlayerBar>();
                PlayerCollided?.Invoke(playerBar);

                Vector3 left = playerBar.Track.Left;
                Vector3 forward = bar.transform.right;
                //направлеям вектор скорости платформы вдоль её оси движения
                Vector3 barSpeed = Vector3.Dot(bar.velocity, left) * left;
                Vector3 ballSpeed = ball.velocity;
                ball.velocity += barSpeed.normalized * Vector3.Dot(forward, ballSpeed);
            }
        }
    }
}