﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Сущность игрока, осуществляюащя контроль над палкой вдоль трека
/// </summary>
public abstract class BasePlayer : MonoBehaviour
	{
	public delegate void playerInfo (string name, int id);
	event playerInfo PlayerJoined;
	event playerInfo NameChanged;

	int score;
	int Score { get { return score; } }

	int identificator;
	int playerNum;
	string playerName;
	string PlayerName {
		get { return playerName; }
		set {
			if ( value != null )
				{
				playerName = value;
				NameChanged(playerName, identificator);
				}
			}
		}

	PlayerBar Bar;
	BarTrack track;
	public void Move(float direction)
		{
		
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

		playerName = _playerName;
		score = _score;
		playerNum = num;
		}
	}
