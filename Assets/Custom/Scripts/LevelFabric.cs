using UnityEngine;
using System.Collections;

/// <summary>
/// Фабрика контролирующая создание объектов
/// </summary>
public class LevelFabric : Singleton<LevelFabric>
	{
	BasePlayer[] players;
	PlayerBar[] bars;
	BarTrack[] tracks;
	GameObject[] borders;
	Vector3 fieldCenter;
	Color[] colors;
	public GameObject TrackPrefab;
	public GameObject BarPrefab;
	public GameObject BallPrefab;

	Vector3[] faces = { Vector3.right, Vector3.left, Vector3.forward, -Vector3.back };
	Vector2[] places = { new Vector2(1, 2), new Vector2(3, 4), new Vector2(4, 1), new Vector2(2, 3) };
	float[] rotations = { 0, 180, -90, 90 };
	void Awake()
		{
		//нахожу на карте коллонны
		borders = new GameObject[4];
		for ( int i = 1; i < 5; i++ )
			borders[i-1] = transform.Find($"W{i}").gameObject;
		}

	/// <summary>
	/// Создает и расставляет палки и треки
	/// </summary>
	/// <param name="players"></param>
	public BasePlayer[] CreateLevel(BasePlayer[] p)
		{
		return null;
		}

	/// <summary>
	/// Создает и расставляет палки и треки
	/// </summary>
	/// <param name="plCount"></param>
	/// <param name="botCount"></param>
	public BasePlayer[] CreateLevel (int plCount, int botCount)
		{
		//выделяю память
		bars = new PlayerBar[plCount + botCount];
		players = new BasePlayer[plCount+botCount];
		tracks = new BarTrack[plCount + botCount];
		for ( int i = 0; i < plCount + botCount; i++ )
			{
			//создаю объекты треков и их инициализация позицией и поворотом
			tracks[i] = Instantiate(TrackPrefab).GetComponent<BarTrack>();
			var lBorder = places[i].x-1;
			var rBorder = places[i].y-1;
			tracks[i].Initialize(borders[(int)lBorder].transform.position, borders[(int)rBorder].transform.position, rotations[i]);

			//создание палок и их инициализация
			bars[i] = Instantiate(BarPrefab).GetComponent<PlayerBar>();
			bars[i].Initialize(tracks[i], faces[i], rotations[i]);

			tracks[i].player = bars[i];
			//заполняю в массив сначала пользователей, а потом ботов
			if ( i < plCount )
				players[i] = bars[i].gameObject.AddComponent<RealPlayer>();
			else
				players[i] = bars[i].gameObject.AddComponent<BotPlayer>();
			//инициализирует игрока именем и номером
			players[i].Initialize($"Player{i + 1}", bars[i], i + 1, 0);
			}
		return players;
		}
	/// <summary>
	/// Убирает игровые объекты
	/// </summary>
	public void DestroyLevel()
		{

		}

	void SpawnBall()
		{

		}

	// Update is called once per frame
	void Update ()
		{

		}

	}
