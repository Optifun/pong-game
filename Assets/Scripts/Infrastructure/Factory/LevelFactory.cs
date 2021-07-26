using System.Collections;
using System.Collections.Generic;
using Ball;
using Bar;
using Infrastructure.Managers;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Infrastructure.Factory
{
    /// <summary>
    /// Фабрика контролирующая создание объектов
    /// </summary>
    public class LevelFactory : Singleton<LevelFactory>
    {
        public GameObject TrackPrefab;
        public GameObject BarPrefab;
        public GameObject BallPrefab;
        public GameObject WallPrefab;

        public Text freezeTimer;

        #region Variables

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
        public float BallSpeed = 7f;

        public readonly Color[] colors = {Color.red, Color.blue, Color.green, Color.yellow}; //цвета платформ

        //*Под сомнением
        public Color GetColor(int id) => colors[id];

        //поля-счетчики
        //*Массив
        public GameObject[] scoreForPlayer = new GameObject[4];

        /// <summary>
        /// Игроки и боты
        /// </summary>
        private BasePlayer[] _players;


        //*Изменить для нового положения камеры
        readonly Vector3[] _faces = {Vector3.right, Vector3.left, Vector3.back, Vector3.forward};

        //*Между какими стенками располагается плита
        readonly Vector2[] _places = {new Vector2(1, 2), new Vector2(3, 4), new Vector2(1, 4), new Vector2(3, 2)};

        //*Сделать универсальным в цикле
        readonly float[] _rotations = {0, 180, 90, -90};

        /// <summary>
        /// Управляемые платформы игроков либо ботов
        /// </summary>
        private PlayerBar[] _bars;

        /// <summary>
        /// Ворота кроме колонн, управляет и следит за платформой
        /// </summary>
        private BarTrack[] _tracks;

        /// <summary>
        /// Колонны по углам
        /// </summary>
        private GameObject[] _columns;

        /// <summary>
        /// Центр игрового поля
        /// </summary>
        private Vector3 _fieldCenter;

        /// <summary>
        /// Мячи
        /// </summary>
        private List<GameObject> _balls;

        /// <summary>
        /// Количество мячей в данный момент
        /// </summary>
        private int CountBalls => _balls?.Count ?? 0;

        #endregion

        public void Awake()
        {
            //нахожу на карте коллонны
            _columns = new GameObject[4];
            _fieldCenter = Vector3.zero;
            for (int i = 0; i < 4; i++)
            {
                var obj = transform.Find($"W{i + 1}");
                _fieldCenter += obj.position;
                _columns[i] = obj.gameObject;
            }

            _fieldCenter /= 4;

            //цвета платформ
            colors[0] = new Color(0.8313726f, 0.2627451f, 0.2f);
            colors[1] = new Color(0.2392157f, 0.6039216f, 1);
            colors[2] = new Color(0.6313726f, 0.8784314f, 0.31764f);
            colors[3] = new Color(0.8862745f, 0.8745098f, 0.2313725f);
        }

        private void Start()
        {
            _balls = new List<GameObject>();
            Game.SingletonObj.StartGameScene();
        }

        public void BackToMenu()
        {
            _balls.Clear();
            SceneManager.LoadScene("MainMenu");
        }

        /// <summary>
        /// Создает и расставляет палки и треки
        /// </summary>
        /// <param name="plCount">Количество игроков</param>
        /// <param name="botCount">Количество ботов</param>
        public BasePlayer[] CreateLevel(int plCount, int botCount)
        {
            //выделяю память
            int totalCount = plCount + botCount;
            _bars = new PlayerBar[totalCount];
            _players = new BasePlayer[totalCount];
            _tracks = new BarTrack[totalCount];

            for (int i = 0; i < totalCount; i++)
            {
                //создаю объекты треков и их инициализация позицией и поворотом
                _tracks[i] = Instantiate(TrackPrefab).GetComponent<BarTrack>();
                var lBorder = _places[i].x - 1;
                var rBorder = _places[i].y - 1;
                _tracks[i].Initialize(_columns[(int) lBorder].transform.position, _columns[(int) rBorder].transform.position, _rotations[i]);

                //создание палок и их инициализация
                _bars[i] = Instantiate(BarPrefab).GetComponent<PlayerBar>();
                _bars[i].Initialize(_tracks[i], _faces[i], _rotations[i]);

                _tracks[i].player = _bars[i];
                //заполняю в массив сначала пользователей, а потом ботов
                if (i < plCount)
                    _players[i] = _bars[i].gameObject.AddComponent<RealPlayer>();
                else
                    _players[i] = _bars[i].gameObject.AddComponent<BotPlayer>();
                //инициализирует игрока именем и номером
                _players[i].Initialize($"Player{i + 1}", _bars[i], i + 1);
                _bars[i].Player = _players[i];
                _players[i].color = colors[i];
            }

            scoreForPlayer[2].SetActive(true);
            scoreForPlayer[3].SetActive(true);
            if (totalCount == 2)
            {
                Instantiate(WallPrefab, new Vector3(-3.5f, 1.2f, -4f), Quaternion.Euler(0, 0, 0));
                Instantiate(WallPrefab, new Vector3(-3.5f, 1.2f, 4f), Quaternion.Euler(0, 0, 0));
                scoreForPlayer[2].SetActive(false);
                scoreForPlayer[3].SetActive(false);
            }

            StartCoroutine(FreezeTimerStart());
            return _players;
        }

        private IEnumerator FreezeTimerStart()
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

        private IEnumerator SpawnBall()
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
            _balls.Remove(ball);
            // с вероятностью 50% спавнится новый мяч
            if (Random.Range(0f, 1f) > 0.5f)
                Spawn();
        }

        private void Spawn()
        {
            if (CountBalls <= MaxBalls)
            {
                GameObject ball = Instantiate(BallPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                ball.layer = 9;
                _balls.Add(ball);
                Vector3 ballDirection;
                int rand = Random.Range(0, 4);
                float angle;
                if (Game.SingletonObj.TotalPlayers == 4)
                {
                    angle = Mathf.Deg2Rad * (Random.Range(30f, 120f) + rand * 90);
                    ballDirection = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)).normalized;
                }
                else
                {
                    rand = (rand < 2) ? 0 : 1;
                    angle = Random.Range(-60f, 60f) + rand * 180;
                    ballDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), 0, Mathf.Sin(Mathf.Deg2Rad * angle)).normalized;
                }

                BallMovement behaviour = ball.GetComponent<BallMovement>();
                //задаём начальную траекторию мячу
                behaviour.Launch(ballDirection * BallSpeed);
            }
        }
    }
}