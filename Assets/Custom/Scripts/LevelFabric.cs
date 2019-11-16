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
	    Vector3 fieldCenter;
        
	    static Color[] colors = {Color.red,Color.blue,Color.green,Color.yellow}; //цвета платформ
        public static Color GetColor(int id) { return colors[id];  }

	    public GameObject TrackPrefab;
	    public GameObject BarPrefab;
        public GameObject BouncePrefab;
        public GameObject WallPrefab;
	    Vector3[] faces = { Vector3.right, Vector3.left, Vector3.forward, -Vector3.back };
	    Vector2[] places = { new Vector2(1, 2), new Vector2(3, 4), new Vector2(4, 1), new Vector2(2, 3) };
	    float[] rotations = { 0, 180, 90, -90 };

	    void Awake()
		    {
		        //нахожу на карте коллонны
		        borders = new GameObject[4];
		        for ( int i = 1; i < 5; i++ )
			        borders[i-1] = transform.Find($"W{i}").gameObject;
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
        if(plCount+botCount == 2)
        {
            Instantiate(WallPrefab, new Vector3(-3.5f, 1.2f, -4f), Quaternion.Euler(0, 0, 0));
            Instantiate(WallPrefab, new Vector3(-3.5f, 1.2f, 4f), Quaternion.Euler(0, 0, 0));
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
        
        var t = Instantiate(BouncePrefab, new Vector3(0, 0f, 0), Quaternion.identity);
        t.layer = 9;
        yield return new WaitForSeconds(6f);
        StartCoroutine(SpawnBall());
    }

	// Update is called once per frame
	void Update ()
		{

		}

	}
