using System.Collections;
using Infrastructure.Factory;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(BallFactory))]
    public class BallSpawner : MonoBehaviour
    {
        public BallFactory Factory;
        public float SpawnDelay = 3;
        private Coroutine _spawnCoroutine;

        public void EnableSpawning()
        {
            if (_spawnCoroutine != null)
                DisableSpawning();

            _spawnCoroutine = StartCoroutine(SpawnBall());
        }

        public void DisableSpawning()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        private IEnumerator SpawnBall()
        {
            while (true)
            {
                yield return new WaitForSeconds(SpawnDelay);
                Factory.Spawn();
            }
        }
    }
}