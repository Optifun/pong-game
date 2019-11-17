using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultButtons : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Application.LoadLevel("MainMenu");
    }
    public void PlayAgain()
    {
        Application.LoadLevel("SampleScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
