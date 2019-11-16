using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlace : MonoBehaviour
{

    public Canvas GeneralCanvas;
    public Canvas PlayerCanvas;

    // Start is called before the first frame update
    public void SetPlace()
    {
        GeneralCanvas.enabled = false;
        PlayerCanvas.enabled = true;
    }

    public void GoBack()
    {
        GeneralCanvas.enabled = true;
        PlayerCanvas.enabled = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

        // Update is called once per frame
        void Start()
    {
        GeneralCanvas.enabled = true;
        PlayerCanvas.enabled = false;
    }
}
