using System;
using Bar;
using UnityEngine;

namespace Ball
{
    [RequireComponent(typeof(Rigidbody))]
    public class BallCollision : MonoBehaviour
    {
        public event Action<PlayerBar> PlayerCollided;
        public event Action<PlayerBar> GateCollided;


        private void OnCollisionEnter(Collision collision)
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