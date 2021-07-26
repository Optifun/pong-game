using System;
using Bar;
using UnityEngine;

namespace Ball
{
    [RequireComponent(typeof(BallCollision))]
    [RequireComponent(typeof(BallVFX))]
    [RequireComponent(typeof(BallMovement))]
    public class BallController : MonoBehaviour
    {
        public BallCollision Collision;
        public BallVFX Effects;
        public BallMovement Movement;

        public void Construct(Vector3 startVelocity)
        {
            Movement.Launch(startVelocity);
        }

        private void Start()
        {
            Collision.PlayerCollided += ChangeAppearance;
        }

        private void OnDestroy()
        {
            Collision.PlayerCollided -= ChangeAppearance;
        }

        private void ChangeAppearance(PlayerBar player)
        {
            Effects.SetBallColor();
            Effects.PlayHitEffect();
        }
    }
}