using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Managers
{
    public class LobbyManager : MonoBehaviour
    {
        /// <summary>
        /// Представление игрока в виде текста с выводом инфы
        /// </summary>
        public Text[] PlayerSlot = new Text[4];

        /// <summary>
        /// Preset, который закрепляется за игроком
        /// </summary>
        private bool[] _playerPreset = new bool[4];

        /// <summary>
        /// Preset из Input Unity
        /// </summary>
        private bool[] _isPresetAvailable = new bool[6];

        private int _busySlots = 0;

        private void Start()
        {
            //Инициализируем
            _playerPreset = Enumerable.Repeat(true, 4).ToArray();
            _isPresetAvailable = Enumerable.Repeat(true, 6).ToArray();
        }

        public void FixedUpdate()
        {
            //Если не все слоты заполнены
            if (_busySlots < 4)
            {
                for (int i = 0; i < 6; i++)
                for (int j = 0; j < 4; j++)
                {
                    if ((_playerPreset[j] == true) && (_isPresetAvailable[i] == true) && (Input.GetAxis($"{i}Preset") != 0))
                    {
                        PlayerSlot[j].text = (i + 1) + " Preset";
                        _isPresetAvailable[i] = false;
                        _playerPreset[j] = false;
                        _busySlots++;
                    }
                }
            }
        }
    }
}