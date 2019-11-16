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
		players = LevelFabric.SingletonObj.CreateLevel(PlayerCount, BotCount);
		foreach ( var item in players )
			{
			item.track.Goal += OnGoal;
			}
		}

	/// <summary>
	/// Срабатывает, когда кому-то забивают гол
	/// </summary>
	/// <param name="id"></param>
	/// <param name="score"></param>
	private void OnGoal (int id, int score)
		{
		BasePlayer t = FindByID(id);
		t.Score -= 1;
		return;
		}

	BasePlayer FindByID(int id)
		{
		foreach ( var item in players )
			{
			if ( item.identificator == id )
				return item;
			}
		return null;
		}

	// Update is called once per frame
	void Update ()
		{

		}
	}
