using System;
using System.Collections;
using System.Collections.Generic;
using Ball;
using Bar;
using Infrastructure.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Factory
{
    public class BallFactory : MonoBehaviour
    {
        public event Action<PlayerBar> Goal;

        public Transform SpawnPoint;
        public GameObject BallPrefab;

        public int MaxBalls = 6;
        public float BallSpeed = 7f;

        private int CountBalls => _balls?.Count ?? 0;
        private List<BallCollision> _balls = new List<BallCollision>();
        private IEnumerator<Vector3> _ballDirection;


        private void Start()
        {
            _ballDirection = Game.SingletonObj.TotalPlayers == 4
                ? FourSidesDirection()
                : TwoSidesDirection();
        }

        private Vector3 NextBallDirection()
        {
            _ballDirection.MoveNext();
            return _ballDirection.Current;
        }

        public void Spawn()
        {
            if (CountBalls <= MaxBalls)
            {
                BallController ball = InstantiateBall(SpawnPoint.position);
                _balls.Add(ball.Collision);

                Vector3 ballDirection = NextBallDirection();
                ball.Construct(ballDirection * BallSpeed);

                ball.Collision.GateCollided += OnGoal;
            }
        }

        private void OnGoal(PlayerBar player, BallCollision ball)
        {
            ball.GateCollided -= OnGoal;
            Goal?.Invoke(player);

            _balls.Remove(ball);
            if (TestProbability(0.5f))
                Spawn();

            Destroy(ball.gameObject, 2f);
        }

        private BallController InstantiateBall(Vector3 position) =>
            Instantiate(BallPrefab, position, Quaternion.identity)
                .GetComponent<BallController>();

        private static bool TestProbability(float p) =>
            Random.Range(0f, 1f) > p;

        private static IEnumerator<Vector3> TwoSidesDirection()
        {
            while (true)
            {
                float angle = PickSide(2) * 180 + SpreadAngle(30f);
                Vector3 ballDirection = AngleToDirection(angle);
                yield return ballDirection;
            }
        }

        private static IEnumerator<Vector3> FourSidesDirection()
        {
            while (true)
            {
                float angle = PickSide(4) * 90 + SpreadAngle(30f);
                Vector3 ballDirection = AngleToDirection(angle);
                yield return ballDirection;
            }
        }

        private static Vector3 AngleToDirection(float deg)
        {
            float radAngle = deg * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(radAngle), 0, Mathf.Sin(radAngle)).normalized;
        }

        private static float SpreadAngle(float spread) =>
            Random.Range(-spread, spread);

        private static int PickSide(int count) =>
            Random.Range(0, count);
    }
}