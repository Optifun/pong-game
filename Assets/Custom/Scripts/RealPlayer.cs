using UnityEngine;
using System.Collections;

public class RealPlayer : BasePlayer
	{

	void Start ()
		{
		Debug.Log("WDASD");
		Initialize("pl1", GetComponent<PlayerBar>(), 1, 0);
		}

	// Update is called once per frame
	void FixedUpdate ()
		{
		if (Input.anyKey)
			{
			var direction = Input.GetAxis($"Player{playerNum} Input");
			Move(direction);
			}
		}
	}
