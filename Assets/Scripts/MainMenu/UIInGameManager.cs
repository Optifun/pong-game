using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameManager : Singleton<UIInGameManager>
{
	public delegate void TimerEvent ();
	public event TimerEvent TimeIsUp;
    public Text Time;
    //3left
    //4right
    //1bottom
    //2top
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameTime(120));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameTime(int seconds)
    {
		while ( true )
			{
			yield return new WaitForSeconds(1f);
			seconds--;
			if ( seconds % 60 < 10 )
				{
				Time.text = seconds / 60 + ":0" + seconds % 60;
				}
			else
				Time.text = seconds / 60 + ":" + seconds % 60;
			if (seconds==0)
				{
				TimeIsUp();
				yield return null;
				}
			}
    }
}
