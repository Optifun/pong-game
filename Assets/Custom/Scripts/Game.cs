using UnityEngine;
using System.Collections;

/// <summary>
/// Контроллирует очки игроков, сложность уровня
/// </summary>
public class Game : Singleton<Game>
	{
	public int PlayerCount=1;
	public int BotCount;
	BasePlayer[] players;
	// Use this for initialization
	void Start ()
		{
		LevelFabric.SingletonObj.CreateLevel(PlayerCount, BotCount);
		}

	// Update is called once per frame
	void Update ()
		{

		}
	}
