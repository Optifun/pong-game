using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceMovement : MonoBehaviour
{
    Rigidbody rb;
    public GameObject Hit;
    //Скорость шара
    public float constBallSpeed = 7f;
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
        rb.AddForce(way * constBallSpeed, ForceMode.VelocityChange);
        
    }

    void FixedUpdate()
    {
       //Контроль скорости мяча
        //Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude != constBallSpeed)
        {
            rb.velocity = rb.velocity.normalized * constBallSpeed;
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
            Rigidbody bar = collision.rigidbody;
            Rigidbody ball = gameObject.GetComponent<Rigidbody>();

            Vector3 forward = bar.transform.right;
            Vector3 left = bar.GetComponent<PlayerBar>().Track.Left;
            Vector3 barSpeed = Vector3.Dot(bar.velocity,left)*left;
            Vector3 ballSpeed = ball.velocity;
            ball.velocity += barSpeed.normalized * Vector3.Dot(forward, ballSpeed);
            /*
            // Угол альфа между нормалью и шаром, угол падения
            float alphaAngle = Vector3.Angle(forward, ballSpeed);
            // Вектор z
            Vector3 deltaVectorReflection = barSpeed * Mathf.Sin(alphaAngle) * Vector3.Dot(barSpeed, ballSpeed) / ballSpeed.magnitude + ballSpeed;
            
            // Угол бета между нормалью и вектором z, угол отражения
            float angleReflection = (deltaVectorReflection.magnitude>0) ? Vector3.Angle(deltaVectorReflection, forward) : alphaAngle;
            // Дельта угла бета угол бета - угол альфа + он предотвращает ситуации, когда угол будет слишком тупой, т.е. > 160.
            
            //приводим угол к граничным условиям
            angleReflection = (angleReflection < 0) ? 0 : angleReflection;
            angleReflection = (angleReflection >= 80) ? 80 : angleReflection;

            float deltaAngleReflection = angleReflection - alphaAngle;

            float deltaBallSpeed = ballSpeed.magnitude * (Mathf.Cos(deltaAngleReflection) - Mathf.Cos(alphaAngle));

            Vector3 localBallVelocity = (Vector3.Dot(ball.velocity, left)+ deltaBallSpeed) * left;
            ball.velocity = localBallVelocity;
            */
            gameObject.GetComponent<MeshRenderer>().material.color = collision.gameObject.GetComponent<MeshRenderer>().material.color;

            var hit = Instantiate(Hit,new Vector3(collision.contacts[0].point.x,
                                        collision.contacts[0].point.y,
                                        collision.contacts[0].point.z),Quaternion.identity);    //спавн хита (эффект удара)
            Destroy(hit, 0.5f);
        }
    }
}
