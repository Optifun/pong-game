using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Фабрика контролирующая создание объектов
/// </summary>
public class LevelFactory : Singleton<LevelFactory>
	{
    #region Variables

    /// <summary>
    /// Игроки и боты
    /// </summary>
    BasePlayer[] players;

    /// <summary>
    /// Управляемые платформы игроков либо ботов
    /// </summary>
	PlayerBar[] bars;

    /// <summary>
    /// Ворота кроме колонн, управляет и следит за платформой
    /// </summary>
	BarTrack[] tracks;

    /// <summary>
    /// Колонны по углам
    /// </summary>
	GameObject[] columns;

    /// <summary>
    /// Максимальное количество мячей, одновременно присутствующих на поле
    /// </summary>
	public int MaxBalls { get; set; } = 6;

    /// <summary>
    /// Задержка в секундах между появлением нового мяча
    /// </summary>
    public float SpawnDelay { get; set; } = 3;

    /// <summary>
    /// Время до старта матча
    /// </summary>
    public int FreezeTime { get; set; } = 3;

    /// <summary>
    /// Скорость мяча (постоянная)
    /// </summary>
    public float BallSpeed { get; set; } = 7f;

    /// <summary>
    /// Центр игрового поля
    /// </summary>
	Vector3 FieldCenter;

    /// <summary>
    /// Мячи
    /// </summary>
    List<GameObject> Balls;

    /// <summary>
    /// Количество мячей в данный момент
    /// </summary>
    int CountBalls { get { return Balls?.Count ?? 0; } }

    public Text freezeTimer;
	public Color[] colors = {Color.red,Color.blue,Color.green,Color.yellow}; //цвета платформ
    //*Под сомнением
    public Color GetColor(int id) { return colors[id];  }

    //префабы
	public GameObject TrackPrefab;
	public GameObject BarPrefab;
    public GameObject BallPrefab;
    public GameObject WallPrefab;

    //поля-счетчики
    //*Массив
    public GameObject[] scoreForPlayer = new GameObject[4];
    //*Изменить для нового положения камеры
	Vector3[] faces = { Vector3.right, Vector3.left, Vector3.back, Vector3.forward };
    //*Между какими стенками располагается плита
	Vector2[] places = { new Vector2(1, 2), new Vector2(3, 4), new Vector2(1, 4), new Vector2(3, 2) };
    //*Сделать универсальным в цикле
	float[] rotations = { 0, 180, 90, -90 };

    #endregion

	void Awake()
		{
		//нахожу на карте коллонны
		columns = new GameObject[4];
		FieldCenter = Vector3.zero;
		for ( int i = 0; i < 4; i++ )
		    {
			var obj = transform.Find($"W{i+1}");
			FieldCenter += obj.position;
			columns[i] = obj.gameObject;
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
        Balls = new List<GameObject>();
		Game.SingletonObj.StartGameScene();
		}

    public void BackToMenu()
        {
        Balls.Clear();
        SceneManager.LoadScene("MainMenu");
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
	/// <param name="plCount">Количество игроков</param>
	/// <param name="botCount">Количество ботов</param>
	public BasePlayer[] CreateLevel (int plCount, int botCount)
		{
        //выделяю память
        var TotalCount = plCount + botCount;
        bars = new PlayerBar[TotalCount];
		players = new BasePlayer[TotalCount];
		tracks = new BarTrack[TotalCount];
        
		for ( int i = 0; i < TotalCount; i++ )
			{
			//создаю объекты треков и их инициализация позицией и поворотом
			tracks[i] = Instantiate(TrackPrefab).GetComponent<BarTrack>();
			var lBorder = places[i].x-1;
			var rBorder = places[i].y-1;
			tracks[i].Initialize(columns[(int)lBorder].transform.position, columns[(int)rBorder].transform.position, rotations[i]);

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

        scoreForPlayer[2].SetActive(true);
        scoreForPlayer[3].SetActive(true);
        if (TotalCount == 2)
            {
            Instantiate(WallPrefab, new Vector3(-3.5f, 1.2f, -4f), Quaternion.Euler(0, 0, 0));
            Instantiate(WallPrefab, new Vector3(-3.5f, 1.2f, 4f), Quaternion.Euler(0, 0, 0));
            scoreForPlayer[2].SetActive(false);
            scoreForPlayer[3].SetActive(false);
            //SP3.SetActive(false);
            //SP4.SetActive(false);
        }
        StartCoroutine(FreezeTimerStart());
        return players;
		}

    IEnumerator FreezeTimerStart()
        {
        for (int i = FreezeTime; i > 0; i--)
            {
            freezeTimer.text = i.ToString();
            yield return new WaitForSeconds(1f);
            }
        freezeTimer.text = "";
        StartCoroutine(SpawnBall());
        StartCoroutine(UIInGameManager.SingletonObj.GameTime(120));
        }

    IEnumerator SpawnBall()
		{
        while (true)
            {
            yield return new WaitForSeconds(3f);
            Spawn();
            }
        }

    /// <summary>
    /// Возникает при попадании мяча в ворота
    /// </summary>
    /// <param name="id">Идентификатор игрока</param>
    /// <param name="ball">Объект мяча</param>
    public void OnGoal(int id, GameObject ball)
        {
        Destroy(ball, 2f);
        Balls.Remove(ball);
        // с вероятностью 50% спавнится новый мяч
        if (Random.Range(0f, 1f) > 0.5f)
            Spawn();
        }

    void Spawn()
        {
        if (CountBalls <= MaxBalls)
            {
            var ball = Instantiate(BallPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity) as GameObject;
            ball.layer = 9;
            Balls.Add(ball);
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
            BallMovement behaviour = ball.GetComponent<BallMovement>();
            //задаём начальную траекторию мячу
            behaviour.InitBall(ballDirection * BallSpeed);
            }
        }


	}
