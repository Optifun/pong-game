using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
	{
	BasePlayer[] players;
	// Use this for initialization

	BasePlayer[] SelectSort(BasePlayer[] arr)
		{
		bool sorted=false;
		BasePlayer temp;
		while ( !sorted )
			{
			for ( int i = 0; i < arr.Length-1; i++ )
				{
				sorted = true;
				if (arr[i].Score>arr[i+1].Score)
					{
					temp = arr[i];
					arr[i] = arr[i + 1];
					arr[i + 1] = temp;
					sorted = false;
					}
				}
			}
		return arr;
		}

	void Start ()
		{

		}

	// Update is called once per frame
	void Update ()
		{

		}
	}
