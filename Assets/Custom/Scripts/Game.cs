using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Контроллирует очки игроков, сложность уровня
/// </summary>
public class Game : Singleton<Game>
	{
	public int PlayerCount=1;
	public int BotCount;
	BasePlayer[] players;
	RectTransform[] scores;
    public int TotalPlayers;
    public int CountBalls;
    public GameObject BouncePrefab;
    // Use this for initialization
    void Awake ()
		{
		DontDestroyOnLoad(gameObject);
		}

	public void StartGameScene ()
		{
		scores = GameObject.Find("Canvas").GetComponentsInChildren<RectTransform>();
		players = LevelFabric.SingletonObj.CreateLevel(PlayerCount, BotCount);
        TotalPlayers = PlayerCount + BotCount;
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
	private void OnGoal (int id)
		{
		BasePlayer t = FindByID(id);
		t.Score -= 1;
        var r = Random.Range(0, 1);
        if (CountBalls <= 6 && r ==0)
        {
            spawBall();
            CountBalls++;
        }
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

    void spawBall()
    {
        var t = Instantiate(BouncePrefab, new Vector3(0, 0f, 0), Quaternion.identity);
        t.layer = 9;
    }
	// Update is called once per frame
	void Update ()
		{

		}
	}
