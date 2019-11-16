using UnityEngine;
using System.Collections;

/// <summary>
/// Фабрика контролирующая создание объектов
/// </summary>
public class LevelFabric : Singleton<LevelFabric>
	{
	    BasePlayer[] players;   //игроки
	    PlayerBar[] bars;       //платформы
	    BarTrack[] tracks;
	    GameObject[] borders;
		Vector3 FieldCenter;
		float SpawnDelay = 5.5f;
		static Color[] colors = { Color.red, Color.blue, Color.green, Color.yellow }; //цвета платформ
		public static Color GetColor(int id) { return colors[id];  }

	    public GameObject TrackPrefab;
	    public GameObject BarPrefab;
		public GameObject BouncePrefab;
	    Vector3[] faces = { Vector3.right, Vector3.left, Vector3.forward, -Vector3.back };
	    Vector2[] places = { new Vector2(1, 2), new Vector2(3, 4), new Vector2(4, 1), new Vector2(2, 3) };
	    float[] rotations = { 0, 180, 90, -90 };

	void Awake()
		{
		    //нахожу на карте коллонны
		    borders = new GameObject[4];
			FieldCenter = Vector3.zero;
		    for ( int i = 1; i < 5; i++ )
			{
				var obj = transform.Find($"W{i}");
				FieldCenter += obj.position;
			    borders[i-1] = obj.gameObject;
			}
		//colors = new Color[4];
		colors[0] = new Color(0.8313726f, 0.2627451f, 0.2f);
		//0.8313726  0.2627451 0.2
		colors[1] = new Color(0.2392157f, 0.6039216f, 1);
		//0.2392157  0.6039216  1
		colors[2] = new Color(0.6313726f, 0.8784314f, 0.31764f);
		//0.6313726   0.8784314  0.317647
		colors[3] = new Color(0.8862745f, 0.8745098f, 0.2313725f); //цвета платформ
		//0.8862745  0.8745098 0.2313725
		FieldCenter /= 4;
		}

		private void Start ()
			{
				Game.SingletonObj.StartGameScene();
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
			bars[i].Player = players[i];
			}
        StartCoroutine(SpawnBall());
        return players;
		}
	/// <summary>
	/// Убирает игровые объекты
	/// </summary>
	public void DestroyLevel()
		{

		}

    IEnumerator SpawnBall()
		{
        
        Instantiate(BouncePrefab, FieldCenter, Quaternion.identity);
        yield return new WaitForSeconds(SpawnDelay);
        StartCoroutine(SpawnBall());
    }

	// Update is called once per frame
	void Update ()
		{

		}

	}
