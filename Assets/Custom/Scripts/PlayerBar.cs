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
	float _maxspeed;

	/// <summary>
	/// 
	/// </summary>
	public float Speed { get { return body.velocity.magnitude; }}

	/// <summary>
	/// Скорость разгона палки
	/// </summary>
	float acceleration;

	/// <summary>
	/// Трение палки
	/// </summary>
	float friction;
	#endregion

	// Use this for initialization
	void Start ()
		{

		}

	// Update is called once per frame
	void Update ()
		{

		}

	public void MoveLeft()
		{

		}

	public void MoveRight()
		{

		}
	}
