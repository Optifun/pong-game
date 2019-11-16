using UnityEngine;
using System.Collections;

// У палки есть направление взгляда, перпендикулярно которому она будет двигаться
// У палки должен быть объект ограничивающий её горизонтальное движение - трек
// Палка должна иметь компонент управления в который можно поместить игрока или бота
//
[RequireComponent(typeof(Rigidbody))]
public class PlayerBar : MonoBehaviour
	{

	#region Variables

	/// <summary>
	/// Физика объекта
	/// </summary>
	public Rigidbody body;

	/// <summary>
	/// Ударная сторона палки
	/// </summary>
	Collider knockSide;
	
	/// <summary>
	/// Ширина палки 
	/// </summary>
	public float Width;

	/// <summary>
	/// Перпендикуляр к палке
	/// </summary>
	public Vector3 Front;

	/// <summary>
	/// Трек по которому двигается палка
	/// </summary>
	public BarTrack Track;

	/// <summary>
	/// Максимальная скорость
	/// </summary>
	float _maxspeed=10;

	/// <summary>
	/// 
	/// </summary>
	public float Speed { get { return body.velocity.magnitude; }}

	/// <summary>
	/// Скорость разгона палки
	/// </summary>
	float acceleration = 0.25f;

	/// <summary>
	/// Трение палки
	/// </summary>
	float friction = 0.55f;
	#endregion

	// Use this for initialization
	void Start ()
		{
		body = GetComponent<Rigidbody>();
		transform.position = Track.TrackCenter;
		body.drag = friction;
		}

	// Update is called once per frame
	void Update ()
		{

		var moving = -body.velocity;
		if ( moving.magnitude < 0.5f )
			body.velocity = Vector3.zero;
		else
			{
			body.AddForce(moving.normalized*friction, ForceMode.Impulse);
			}
		}

	public void MoveLeft()
		{
		if (body.velocity.magnitude<=_maxspeed)
			{
			body.AddForce(Track.Left, ForceMode.Impulse);
			}
		}

	public void MoveRight()
		{
		if ( body.velocity.magnitude <= _maxspeed )
			body.AddForce(-Track.Left, ForceMode.Impulse);

		}
}
