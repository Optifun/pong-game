using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MM : MonoBehaviour
{

    public Slider PlayerSlider;
    public Text PlayerInfoText;
    public Slider BotSlider;
    public Text BotInfoText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSlider.onValueChanged.AddListener(SliderChanged);
        BotSlider.onValueChanged.AddListener(BotSliderChanged);
    }

    public void StartGame()
    {
        Application.LoadLevel("Game");
        PlayerSlider.minValue = 1;
        SliderChanged(1);
    }

    public void BotSliderChanged(float val)
    {
        //BotSlider.value = val;
        //if (PlayerSlider.value == 3)
        //{
        //    BotSlider.value = 1;
        //}
        //else if ((PlayerSlider.value == 1) && ((BotSlider.value != 1) || (BotSlider.value != 3)))
        //{
        //    BotSlider.value = 1;
        //}
        //else if ((PlayerSlider.value == 2) && (BotSlider.value != 2))
        //{
        //    BotSlider.value = 2;
        //}
        //else BotSlider.value = 0;



        //Дописать
        switch (PlayerSlider.value+val)
        {
            case 2:
                {
                    break;
                }
            case 3:
                {
                    break;
                }
            case 4:
                {
                    break;
                }
            default:
                break;
        }
        BotInfoText.text = "Количество ботов " + BotSlider.value;
    }

    public void SliderChanged(float val)
    {
        PlayerSlider.value = val;
        PlayerInfoText.text = "Количество игроков " + PlayerSlider.value;
        BotSlider.value = val % 2;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
