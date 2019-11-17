using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMovement : MonoBehaviour
{
    Rigidbody rb;
    public GameObject Hit;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rb = GetComponent<Rigidbody>();
        //сообщаем шару начальную скорость
        var velocity = 0.4f;// Random.Range(0f, 1f);
        Vector3 way;
        if (Game.SingletonObj.TotalPlayers == 4)
        {
            var angle = Mathf.Deg2Rad * Random.Range(-80f,80f);
            way = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
        }
        else
        {
            var angle = Mathf.Deg2Rad*(Random.Range(-80f,10f))*4;
            way = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
        }
        rb.AddForce(way * velocity);
        
    }

    //
    void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= 30)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bar"))
        {
            gameObject.GetComponent<MeshRenderer>().material.color = collision.gameObject.GetComponent<MeshRenderer>().material.color;

            var hit = Instantiate(Hit,new Vector3(collision.contacts[0].point.x,
                                        collision.contacts[0].point.y,
                                        collision.contacts[0].point.z),Quaternion.identity);    //спавн хита
            Destroy(hit, 0.5f);
        }
    }
}
