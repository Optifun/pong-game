using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMovement : MonoBehaviour
{
    Rigidbody rb;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //сообщаем шару начальную скорость
        rb.AddForce(-Vector3.right*0.3f);
        
    }

    //
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bar"))
        {
            mat = collision.gameObject.GetComponent<MeshRenderer>().material;
            gameObject.GetComponent<MeshRenderer>().material = mat;
        }
    }
}
