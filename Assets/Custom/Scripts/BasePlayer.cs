using UnityEngine;
using System.Collections;

/// <summary>
/// Сущность игрока, осуществляюащя контроль над палкой вдоль трека
/// </summary>
public abstract class BasePlayer : MonoBehaviour
	{
	public delegate void playerInfo (string name, int id);
	event playerInfo PlayerJoined;
	event playerInfo NameChanged;
    public Color color;
	int score;
	int Score { get { return score; } }

	/// <summary>
	/// Уникальный идентификатор пользователя
	/// </summary>
	protected int identificator;

	/// <summary>
	/// Номер игрока по счету
	/// 1-4
	/// </summary>
	protected int playerNum = 1;
	protected string playerName;
	public string PlayerName {
		get { return playerName; }
		set {
			if ( value != null )
				{
				playerName = value;
				NameChanged(playerName, identificator);
				}
			}
		}

	public PlayerBar Bar;
	public BarTrack track;
	public void Move(float direction)
		{
		if (direction<0)
			Bar.MoveLeft();
		else
			Bar.MoveRight();
		}
	/// <summary>
	/// Инициализирует сущность игрока
	/// </summary>
	/// <param name="_playerName">Ник игрока</param>
	/// <param name="_bar">Игровая палка игрока</param>
	/// <param name="num">Номер игрока по счету 1-4</param>
	/// <param name="_score">Счет с которого начинает игрок</param>
	public virtual void Initialize(string _playerName, PlayerBar _bar, int num, int _score=0)
		{
		Bar = _bar;
		track = Bar.Track;

        _bar.GetComponent<MeshRenderer>().material.color = LevelFabric.GetColor(num-1);    //Установка уникального цвета игрока

		playerName = _playerName;
		score = _score;
		playerNum = num;
		}
	private void Awake ()
		{
		Debug.Log(1);
		}
	}
