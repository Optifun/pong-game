using UnityEngine;

namespace Bar
{
    public class BotPlayer : BasePlayer
    {
        private Vector3 DetectIncoming()
        {
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            Vector3 result = Vector3.zero;
            int count = 0;
            float minDistance = float.MaxValue;
            foreach (GameObject item in balls)
            {
                Vector3 velocity = item.GetComponent<Rigidbody>().velocity;
                if (Vector3.Dot(velocity, Bar.Front) < 0)
                {
                    Vector3 position = item.transform.position;
                    float distance = Vector3.Distance(position, transform.position);
                    minDistance = Mathf.Min(distance, minDistance);
                    result += position / distance;
                    count++;
                }
            }

            if (count == 0)
            {
                count = 1;
                minDistance = 1;
            }

            return result / count * minDistance;
        }

        private void FixedUpdate()
        {
            Vector3 incoming = DetectIncoming();
            if (incoming != Vector3.zero)
            {
                float direction = Vector3.Dot(incoming, track.Left);
                if (direction >= 0.08f)
                    Move(-1);
                if (direction <= -0.08f)
                    Move(1);
            }
        }
    }
}