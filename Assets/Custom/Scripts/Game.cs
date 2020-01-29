using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Контроллирует очки игроков, сложность уровня
/// </summary>
public class Game : Singleton<Game>
	{
	public int PlayerCount=1;
	public int BotCount;
	public BasePlayer[] players;
	Text[] scores;
    public int TotalPlayers;
    public int CountBalls;
    public List<Text> GoalsList;
	public Score[] scoresFin;
    public GameObject BouncePrefab;
    // Use this for initialization
    void Awake ()
		{
		DontDestroyOnLoad(gameObject);
		}

	public void StartGameScene ()
		{
		BasePlayer.PlayersCount = 1;
		scores = GameObject.Find("Canvas").transform.Find("Scores").GetComponentsInChildren<Text>();
		players = LevelFactory.SingletonObj.CreateLevel(PlayerCount, BotCount);
        TotalPlayers = PlayerCount + BotCount;
		UIInGameManager.SingletonObj.TimeIsUp += GameOver; 
		foreach ( var item in players )
			{
			item.track.Goal += OnGoal;
            printScore(item);

            }
		}

	private void GameOver ()
		{
		scoresFin = new Score[players.Length];
		for ( int i = 0; i < players.Length; i++ )
			scoresFin[i] = new Score(players[i].name, players[i].Score, players[i].color);
		SceneManager.LoadScene(2);
		}

	/// <summary>
	/// Срабатывает, когда кому-то забивают гол
	/// </summary>
	/// <param name="id"></param>
	/// <param name="score"></param>
	private void OnGoal (int id)
		{
		BasePlayer t = FindByID(id);
		t.Score += 1;
        var r = Random.Range(0f, 1f);
        if (CountBalls <= 6 && r>0.5f)
        {
            spawBall();
            CountBalls++;
        }
        printScore(t);
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
    void printScore(BasePlayer t)
    {
        scores[t.identificator-1].GetComponent<Text>().text = t.Score.ToString();
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
