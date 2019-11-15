using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>:MonoBehaviour where T:MonoBehaviour
	{
	static T Instance;
	public static T singleton
		{
		get
			{
			if ( Instance == null )
				Instance = GameObject.FindObjectOfType<T>();
			return Instance;
			}
		set
			{
			if (value!=null)
				Instance = value;
			}
		}
}
