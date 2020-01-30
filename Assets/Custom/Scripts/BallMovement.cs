using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс, управляющий поведением шара
/// </summary>
public class BallMovement : MonoBehaviour
{
    /// <summary>
    /// Компонент физики мяча
    /// </summary>
    Rigidbody ballRigidBody;

    /// <summary>
    /// Префаб эффекта столковения
    /// </summary>
    public GameObject Hit;

    /// <summary>
    /// Скорость мяча (постоянная)
    /// </summary>
    float constBallSpeed = 7f;

    /// <summary>
    /// Начальное направление мяча
    /// </summary>
    Vector3 initialVelocity;

    public void InitBall(Vector3 velocity)
        {
        constBallSpeed = velocity.magnitude;
        initialVelocity = velocity;
        }

    void Start()
        {
        ballRigidBody = GetComponent<Rigidbody>();
        ballRigidBody.velocity = initialVelocity;
        }

    void FixedUpdate()
        {
        //Контроль скорости мяча
        if (ballRigidBody.velocity.magnitude != constBallSpeed)
            ballRigidBody.velocity = ballRigidBody.velocity.normalized * constBallSpeed;
        }

    private void OnCollisionEnter(Collision collision)
        {
        if (collision.gameObject.CompareTag("Bar"))
            {
            Rigidbody bar = collision.rigidbody;
            Rigidbody ball = gameObject.GetComponent<Rigidbody>();

            Vector3 forward = bar.transform.right;
            Vector3 left = bar.GetComponent<PlayerBar>().Track.Left;
            //направлеям вектор скорости платформы вдоль её оси движения
            Vector3 barSpeed = Vector3.Dot(bar.velocity,left)*left;
            Vector3 ballSpeed = ball.velocity;
            ball.velocity += barSpeed.normalized * Vector3.Dot(forward, ballSpeed);

            //LEGACY
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
