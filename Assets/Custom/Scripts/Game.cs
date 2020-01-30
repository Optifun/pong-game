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
    public List<Text> GoalsList;
	public Score[] scoresFin;
    public GameObject BouncePrefab;

    void Awake ()
		{
        //Сохраняю объект между сценами
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
            item.track.Goal += LevelFactory.SingletonObj.OnGoal;
            PrintScore(item);

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
	void OnGoal (int id, GameObject ball)
		{
		BasePlayer t = FindByID(id);
		t.Score += 1;
        PrintScore(t);
		return;
		}

	BasePlayer FindByID(int id)
		{
		foreach ( var item in players )
		    if ( item.identificator == id )
			    return item;
		return null;
		}

    void PrintScore(BasePlayer t)
    {
        scores[t.identificator-1].GetComponent<Text>().text = t.Score.ToString();
    }

	// Update is called once per frame
	void Update ()
		{

		}
	}
