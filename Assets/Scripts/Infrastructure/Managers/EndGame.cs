using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
	{
	Score[] players;
	// Use this for initialization
	Text[] scores;
	Image[] imgs;
	Transform Base;

	Score[] SelectSort (Score[] arr)
		{
		bool sorted=false;
		while ( !sorted )
			{
			for ( int i = 0; i < arr.Length - 1; i++ ) 
				{
				sorted = true;
				if ( arr[i].score > arr[i + 1].score ) 
					{
					var temp = arr[i];
					arr[i] = arr[i + 1];
					arr[i + 1] = temp;
					sorted = false;
					}
				}
	
			}
		return arr;
		}

	void DrawScore()
		{
		imgs = Base.GetComponentsInChildren<Image>();
		for ( int i = 0; i < players.Length; i++ )
			{
			imgs[i].transform.Find("Score").GetComponent<Text>().text = players[i].score.ToString();
			imgs[i].color = players[i].color;
			}
		if ( players.Length == 2)
			{
			imgs[2].gameObject.SetActive(false);
			imgs[3].gameObject.SetActive(false);
			}
		}
	void Start ()
		{
		Base = GameObject.Find("Canvas").transform.Find("Scores");
		players = Game.SingletonObj.scoresFin;
		players = SelectSort(players);
		DrawScore();
		}

	// Update is called once per frame
	void Update ()
		{

		}
	}
