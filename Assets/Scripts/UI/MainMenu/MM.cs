using Infrastructure.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MM : MonoBehaviour
    {
        public Slider PlayerSlider;
        public Text PlayerInfoText;
        public Text BotInfoText;
        public Button Plus;
        public Button Minus;
        private int _max = 3;
        private int _min = 1;
        private int _botval = 1;

        private void Start()
        {
            PlayerSlider.onValueChanged.AddListener(SliderChanged);
        }

        public void StartGame()
        {
            Game.SingletonObj.PlayerCount = (int)PlayerSlider.value;
            Game.SingletonObj.BotCount = _botval;
            SceneManager.LoadScene("SampleScene");
        }

        public void AddPlus()
        {
            _botval = _max;
            Plus.enabled = false;
            Minus.enabled = true;
            BotInfoText.text = "" + _botval;
        }
        public void AddMinus()
        {
            _botval = _min;
            Plus.enabled = true;
            Minus.enabled = false;
            BotInfoText.text = "" + _botval;
        }

        public void SliderChanged(float val)
        {
            if (PlayerSlider.value == 1)
            {
                if (((_botval != 1) && (_botval != 3)) || (_botval == 1))
                {
                    _botval = 1;
                    _max = 3;
                    _min = 1;
                    Minus.enabled = false;
                    Plus.enabled = true;
                }
                else if (_botval == 3)
                {
                    Minus.enabled = true;
                    Plus.enabled = false;
                    _max = 3;
                    _min = 1;
                }
            }
            else
            if (PlayerSlider.value == 2)
            {
                if (((_botval != 2) && (_botval != 0)) || (_botval == 0))
                {
                    _botval = 0;
                    _max = 2;
                    _min = 0;
                    Minus.enabled = false;
                    Plus.enabled = true;
                }
                else if (_botval == 2)
                {
                    _min = 0;
                    _max = 2;
                    Minus.enabled = true;
                    Plus.enabled = false;
                }
            }
            if (PlayerSlider.value == 3)
            {
                Minus.enabled = false;
                Plus.enabled = false;
                _botval = 1;
            }
            else if (PlayerSlider.value == 4)
            {
                Minus.enabled = false;
                Plus.enabled = false;
                _botval = 0;
            }
            PlayerInfoText.text = "" + PlayerSlider.value;
            BotInfoText.text = "" + _botval;
        }
    }
}
