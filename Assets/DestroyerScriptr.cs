using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerScriptr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this, 400);
    }
}
