using System.Collections.Generic;
using Bar;
using Infrastructure.Factory;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Infrastructure.Managers
{
    /// <summary>
    /// Контроллирует очки игроков, сложность уровня
    /// </summary>
    public class Game : Singleton<Game>
    {
        public int PlayerCount = 1;
        public int BotCount;
        public BasePlayer[] players;
        public int TotalPlayers;
        public List<Text> GoalsList;
        public Score[] scoresFin;
        public GameObject BouncePrefab;
        Text[] _scores;

        void Awake()
        {
            //Сохраняю объект между сценами
            DontDestroyOnLoad(gameObject);
        }

        public void StartGameScene()
        {
            BasePlayer.PlayersCount = 1;
            _scores = GameObject.Find("Canvas").transform.Find("Scores").GetComponentsInChildren<Text>();
            players = LevelFactory.SingletonObj.CreateLevel(PlayerCount, BotCount);
            TotalPlayers = PlayerCount + BotCount;
            UIInGameManager.SingletonObj.TimeIsUp += GameOver;
            foreach (var item in players)
            {
                item.track.Goal += OnGoal;
                item.track.Goal += LevelFactory.SingletonObj.OnGoal;
                PrintScore(item);
            }
        }

        private void GameOver()
        {
            scoresFin = new Score[players.Length];
            for (int i = 0; i < players.Length; i++)
                scoresFin[i] = new Score(players[i].name, players[i].Score, players[i].color);
            SceneManager.LoadScene(2);
        }

        /// <summary>
        /// Срабатывает, когда кому-то забивают гол
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ball"></param>
        private void OnGoal(int id, GameObject ball)
        {
            BasePlayer t = FindById(id);
            t.Score += 1;
            PrintScore(t);
        }

        private BasePlayer FindById(int id)
        {
            foreach (var item in players)
                if (item.identificator == id)
                    return item;
            return null;
        }

        private void PrintScore(BasePlayer t)
        {
            _scores[t.identificator - 1].GetComponent<Text>().text = t.Score.ToString();
        }
    }
}