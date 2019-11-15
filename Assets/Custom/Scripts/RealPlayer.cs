using UnityEngine;
using System.Collections;

public class RealPlayer : BasePlayer
	{

	// Use this for initialization
	void Start ()
		{

		}

	// Update is called once per frame
	void FixedUpdate ()
		{
		if (Input.anyKeyDown)
			{
			var direction = Input.GetAxis($"Player{playerNum} Input");
			Move(direction);
			}
		}
	}
