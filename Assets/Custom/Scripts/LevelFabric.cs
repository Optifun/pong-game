using UnityEngine;
using System.Collections;

/// <summary>
/// Фабрика контролирующая создание объектов
/// </summary>
public class LevelFabric : Singleton<LevelFabric>
	{
	BasePlayer[] players;
	GameObject borders;
	Vector3 fieldCenter;

	/// <summary>
	/// Создает и расставляет палки и треки
	/// </summary>
	/// <param name="players"></param>
	void CreateLevel(BasePlayer[] p)
		{

		}

	/// <summary>
	/// Убирает игровые объекты
	/// </summary>
	void DestroyLevel()
		{

		}

	void SpawnBall()
		{

		}

	// Use this for initialization
	void Start ()
		{

		}

	// Update is called once per frame
	void Update ()
		{

		}

	}
