using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGameManager : MonoBehaviour
{
    public Text Time;
    private int time = 120;
    //3left
    //4right
    //1bottom
    //2top
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameTime());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GameTime()
    {
        yield return new WaitForSeconds(1f);
        time--;
        if (time % 60 < 10)
        {
            Time.text = time / 60 + ":0" + time % 60;
        }
        else
        Time.text = time / 60+":" + time % 60;
        StartCoroutine(GameTime());
    }
}
