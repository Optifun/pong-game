using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Фабрика контролирующая создание объектов
/// </summary>
public class LevelFactory : Singleton<LevelFactory>
	{
	BasePlayer[] players;   //игроки
	PlayerBar[] bars;       //платформы
	BarTrack[] tracks;
	GameObject[] borders;
	Vector3 FieldCenter;

	public int MaxBalls { get; set; } = 6;
    public float SpawnDelay { get; set; } = 3;
    public int FreezeTime { get; set; } = 3;
    public float ballSpeed { get; set; } = 7f;

    public Text text;
	static Color[] colors = {Color.red,Color.blue,Color.green,Color.yellow}; //цвета платформ
    public static Color GetColor(int id) { return colors[id];  }
    //префабы
	public GameObject TrackPrefab;
	public GameObject BarPrefab;
    public GameObject BouncePrefab;
    public GameObject WallPrefab;
    //поля-счетчики
    public GameObject SP1;
    public GameObject SP2;
    public GameObject SP3;
    public GameObject SP4;
    //
	Vector3[] faces = { Vector3.right, Vector3.left, Vector3.back, Vector3.forward };
	Vector2[] places = { new Vector2(1, 2), new Vector2(3, 4), new Vector2(1, 4), new Vector2(3, 2) };
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
        //цвета платформ
		colors[0] = new Color(0.8313726f, 0.2627451f, 0.2f);
		colors[1] = new Color(0.2392157f, 0.6039216f, 1);
		colors[2] = new Color(0.6313726f, 0.8784314f, 0.31764f);
		colors[3] = new Color(0.8862745f, 0.8745098f, 0.2313725f); 
		FieldCenter /= 4;
		}

	private void Start ()
		{
		Game.SingletonObj.StartGameScene();
		}

    public void BackToMenu()
        {
        Game.SingletonObj.CountBalls = 0;
        Application.LoadLevel("MainMenu");
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
			players[i].color = colors[i];
			}

		SP3.SetActive(true);
		SP4.SetActive(true);
		if (plCount+botCount == 2)
            {
            Instantiate(WallPrefab, new Vector3(-3.5f, 1.2f, -4f), Quaternion.Euler(0, 0, 0));
            Instantiate(WallPrefab, new Vector3(-3.5f, 1.2f, 4f), Quaternion.Euler(0, 0, 0));
            SP3.SetActive(false);
            SP4.SetActive(false);
            }
        StartCoroutine(FreezeTimerStart());
        return players;
		}
	/// <summary>
	/// Убирает игровые объекты
	/// </summary>
	public void DestroyLevel()
		{

		}

    IEnumerator FreezeTimerStart()
        {
        for (int i = FreezeTime; i > 0; i--)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        text.text = "";
        StartCoroutine(SpawnBall());
        StartCoroutine(UIInGameManager.SingletonObj.GameTime(120));
        }

    void Spawn()
        {
        if (Game.SingletonObj.CountBalls <= 6)
            {
            var ball = Instantiate(BouncePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
            ball.layer = 9;
            Game.SingletonObj.CountBalls++;
            Debug.Log(Game.SingletonObj.CountBalls);

            Vector3 ballDirection;
            int rand = Random.Range(0, 4);
            if (Game.SingletonObj.TotalPlayers == 4)
                {
                var angle = Mathf.Deg2Rad * (Random.Range(30f, 120f) + rand * 90);
                ballDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
                }
            else
                {
                rand = (rand < 2) ? 0 : 1;
                var angle = Random.Range(-60f, 60f) + rand * 180;
                ballDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle)).normalized;
                }
            Debug.Log(ballDirection);
            BallMovement behaviour = ball.GetComponent<BallMovement>();
            behaviour.InitBall(ballDirection * ballSpeed);
            //Rigidbody ballRigidBody = ball.GetComponent<Rigidbody>();
            //сообщаем шару начальную скорость
            //ballRigidBody.velocity = Vector3.forward * BallSpeed;
            //ballRigidBody.AddForce(Vector3.forward, ForceMode.Impulse);
        }
        }

    IEnumerator SpawnBall()
		{
        while (true)
            {
            yield return new WaitForSeconds(3f);
            Spawn();
            }
        }

	// Update is called once per frame
	void Update ()
		{

		}

	}
