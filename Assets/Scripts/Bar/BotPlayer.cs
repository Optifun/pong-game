using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BotPlayer : BasePlayer
	{

	// Use this for initialization
	void Start ()
		{

		}

	Vector3 DetectIncoming()
		{
		var balls = GameObject.FindGameObjectsWithTag("Ball");
		var velocity = Vector3.zero;
		//List<Vector3> bPositions = new List<Vector3>();
		Vector3 result = Vector3.zero;
		int count = 0;
		float minDistance = 10;
		float distance = 0;
		foreach ( var item in balls )
			{
			velocity = item.GetComponent<Rigidbody>().velocity;
			if ( Vector3.Dot(velocity, Bar.Front) < 0 )
				{
				distance = Vector3.Distance(item.transform.position, transform.position);
				minDistance = ( minDistance > distance ) ? distance : minDistance;
				result += item.transform.position / distance;
				count++;
				}
			}
		if ( count == 0 )
			{
			count = 1;
			minDistance = 1;
			}
		return result / count * minDistance;
		}

	// Update is called once per frame
	void FixedUpdate ()
		{
		var incoming = DetectIncoming();
		if (incoming!=Vector3.zero)
			{
			var direction = Vector3.Dot(incoming, track.Left);
			if ( direction >= 0.08f )
				Move(-1);
			if ( direction <= -0.08f )
				Move(1);
			}

		}
	}
