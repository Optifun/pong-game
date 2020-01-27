﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMovement : MonoBehaviour
{
    Rigidbody rb;
    public GameObject Hit;
    public float ballspeed = 7f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        rb = GetComponent<Rigidbody>();
        //сообщаем шару начальную скорость
        Vector3 way;
        int r = Random.Range(0, 4);
        float offset = 0;
        if (Game.SingletonObj.TotalPlayers == 4)
        {
            var angle = Mathf.Deg2Rad * (Random.Range(30f,120f)+r*90);
            way = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
        }
        else
        {
            offset = (r > 1) ? 0 : 1;
            var angle = Random.Range(-60f,60f)+offset*180;
            //Debug.Log(angle);
            way = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle)).normalized;
        }
        rb.AddForce(way * ballspeed, ForceMode.VelocityChange);
        
    }

    void FixedUpdate()
    {
       //Контроль скорости мяча
        Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude != ballspeed)
        {
            rb.velocity = rb.velocity.normalized * ballspeed;
        }
    }
    
    void Update()
    {
        //Уничтожение шара по истечении времени
        timer += Time.deltaTime;
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
                                        collision.contacts[0].point.z),Quaternion.identity);    //спавн хита (эффект удара)
            Destroy(hit, 0.5f);
        }
    }
}
