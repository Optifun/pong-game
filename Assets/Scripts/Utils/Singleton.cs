using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T>:MonoBehaviour where T:MonoBehaviour
	{
	static T instance;
	public static T SingletonObj
		{
		get
			{
			if ( instance == null )
				instance = GameObject.FindObjectOfType<T>();
			return instance;
			}
		set
			{
			if (value!=null)
				instance = value;
			}
		}
}
