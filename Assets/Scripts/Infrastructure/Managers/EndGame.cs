using Bar;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Managers
{
    public class EndGame : MonoBehaviour
    {
        private Score[] _players;

        // Use this for initialization
        private Text[] _scores;
        private Image[] _images;
        private Transform _base;

        private Score[] SelectSort(Score[] arr)
        {
            bool sorted = false;
            while (!sorted)
            {
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    sorted = true;
                    if (arr[i].score > arr[i + 1].score)
                    {
                        Score temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        sorted = false;
                    }
                }
            }

            return arr;
        }

        private void DrawScore()
        {
            _images = _base.GetComponentsInChildren<Image>();
            for (int i = 0; i < _players.Length; i++)
            {
                _images[i].transform.Find("Score").GetComponent<Text>().text = _players[i].score.ToString();
                _images[i].color = _players[i].color;
            }

            if (_players.Length == 2)
            {
                _images[2].gameObject.SetActive(false);
                _images[3].gameObject.SetActive(false);
            }
        }

        public void Start()
        {
            _base = GameObject.Find("Canvas").transform.Find("Scores");
            _players = Game.SingletonObj.scoresFin;
            _players = SelectSort(_players);
            DrawScore();
        }
    }
}