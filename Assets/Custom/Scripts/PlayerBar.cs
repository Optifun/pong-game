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
	/// Ударная сторона палки [не нужна]
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
	/// Трек по которому двигается палка
	/// </summary>
	public BasePlayer Player;

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
	float acceleration = 2f;

	/// <summary>
	/// Трение палки
	/// </summary>
	float friction = 0.8f;
	#endregion

	public void Initialize(BarTrack _track, Vector3 front, float yRotation)
		{
		var t = new Quaternion();
		t.eulerAngles = new Vector3(0, yRotation, 0);
		transform.rotation = t;
		Front = front;
		Track = _track;
		transform.position = Track.TrackCenter;
		}

	void Start ()
		{
		body = GetComponent<Rigidbody>();
		}

	void FixedUpdate ()
		{
		body.velocity = ( Speed < 0.5f ) ? Vector3.zero : body.velocity*friction;
		}

	public void MoveLeft()
		{
		if ( Speed <= _maxspeed )
			body.AddForce(Track.Left*acceleration, ForceMode.Impulse);
		}

	public void MoveRight()
		{
		if ( Speed <= _maxspeed )
			body.AddForce(-Track.Left*acceleration, ForceMode.Impulse);

		}
}
