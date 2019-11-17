using UnityEngine;
using System.Collections;

/// <summary>
/// Трек на котором размещается палка игрока
/// </summary>
public class BarTrack : MonoBehaviour
	{
	#region Variables
	public delegate void ScoreChanged (int id);
	public event ScoreChanged Goal;
	/// <summary>
	/// Левая граница трека
	/// </summary>
	public Vector3 leftPoint { get; protected set; }

	/// <summary>
	/// Правая граница трека
	/// </summary>
	public Vector3 rightPoint { get; protected set; }
	[SerializeField]
	public Vector3 Left { get; protected set; }


	/// <summary>
	/// Срединная точка трека
	/// </summary>
	public Vector3 TrackCenter { get; protected set; }

	float trackWidth;
	/// <summary>
	/// Палка, которую перемещают по треку
	/// </summary>
	public PlayerBar player;

	/// <summary>
	/// Положение палки
	/// </summary>
	public Transform tPlayer;

	/// <summary>
	/// Зона в которой засчитывается гол
	/// </summary>
	Collider deadZone;

	/// <summary>
	/// Положение палки игрока, значение от -1 до 1
	/// </summary>
	public float BarPos {
		get
			{
			return (Vector3.Distance(tPlayer.position, TrackCenter))/trackWidth*2;
			}
		}
	#endregion
	void Awake ()
		{
		//var map = GameObject.Find("Map").transform;
		//leftPoint = map.Find("W1").position;
		//rightPoint = map.Find("W2").position;
		}

	private void OnCollisionEnter (Collision collision)
		{
		if (collision.gameObject.tag=="Ball")
			{
			var t = player.Player;
			if (Goal!=null)
				Goal.Invoke(t.identificator);
			Destroy(collision.gameObject);
            Game.SingletonObj.CountBalls--;

            }
		}

	void Start ()
		{
		trackWidth = Vector3.Distance(leftPoint, rightPoint);
		}

	public void Initialize (Vector3 left, Vector3 right, float yRotation)
		{
		var t = new Quaternion();
		t.eulerAngles = new Vector3(0, yRotation, 0);
		transform.rotation = t;

		leftPoint = left;
		rightPoint = right;
		TrackCenter = ( rightPoint + leftPoint ) / 2;
		Left = (leftPoint - TrackCenter ).normalized;
		Debug.Log(Left);
		transform.position = TrackCenter;
		
		}

	void Update ()
		{

		}
	}


