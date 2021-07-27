using System;
using Bar;
using UnityEngine;
using Utils;

namespace Ball
{
    [RequireComponent(typeof(CollisionDetector))]
    public class BallCollision : MonoBehaviour
    {
        public event Action<PlayerBar> PlayerCollided;
        public event Action<PlayerBar> GateCollided;

        public CollisionDetector Collider;

        private void Start()
        {
            Collider.CollisionEnter += OnCollided;
        }

        private void OnDestroy()
        {
            Collider.CollisionEnter -= OnCollided;
        }

        private void OnCollided(Collision collision)
        {
            GameObject entity = collision.gameObject;
            if (entity.CompareTag("Bar"))
            {
                PlayerBar playerBar = entity.GetComponent<PlayerBar>();
                PlayerCollided?.Invoke(playerBar);
            }

            if (entity.CompareTag("DeadZone"))
            {
                BarTrack barTrack = entity.GetComponent<BarTrack>();
                GateCollided?.Invoke(barTrack.player);
            }
        }
    }
}