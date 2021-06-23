using UnityEngine;
using System.Collections;

public class RealPlayer : BasePlayer
	{

	void Start ()
		{
		}

	// Update is called once per frame
	void FixedUpdate ()
		{
		if (Input.anyKey)
			{
			var direction = Input.GetAxis($"Player{playerNum} Input");
			if ( direction == 0 )
				return;
			Move(direction);
			}
		}
	}
