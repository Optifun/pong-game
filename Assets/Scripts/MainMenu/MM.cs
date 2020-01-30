using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MM : MonoBehaviour
{
    public Slider PlayerSlider;
    public Text PlayerInfoText;
    public Text BotInfoText;
    public Button Plus;
    public Button Minus;
    private int max = 3;
    private int min = 1;
    private int botval = 1;

    void Start()
    {
        PlayerSlider.onValueChanged.AddListener(SliderChanged);
    }

    public void StartGame()
    {
		Game.SingletonObj.PlayerCount = (int)PlayerSlider.value;
		Game.SingletonObj.BotCount = botval;
        SceneManager.LoadScene("SampleScene");
    }

    public void AddPlus()
    {
        botval = max;
        Plus.enabled = false;
        Minus.enabled = true;
        BotInfoText.text = "" + botval;
    }
    public void AddMinus()
    {
        botval = min;
        Plus.enabled = true;
        Minus.enabled = false;
        BotInfoText.text = "" + botval;
    }

    public void SliderChanged(float val)
    {
        if (PlayerSlider.value == 1)
        {
            if (((botval != 1) && (botval != 3)) || (botval == 1))
            {
                botval = 1;
                max = 3;
                min = 1;
                Minus.enabled = false;
                Plus.enabled = true;
            }
            else if (botval == 3)
            {
                Minus.enabled = true;
                Plus.enabled = false;
                max = 3;
                min = 1;
            }
        }
        else
        if (PlayerSlider.value == 2)
        {
            if (((botval != 2) && (botval != 0)) || (botval == 0))
            {
                botval = 0;
                max = 2;
                min = 0;
                Minus.enabled = false;
                Plus.enabled = true;
            }
            else if (botval == 2)
            {
                min = 0;
                max = 2;
                Minus.enabled = true;
                Plus.enabled = false;
            }
        }
        if (PlayerSlider.value == 3)
        {
            Minus.enabled = false;
            Plus.enabled = false;
            botval = 1;
        }
        else if (PlayerSlider.value == 4)
        {
            Minus.enabled = false;
            Plus.enabled = false;
            botval = 0;
        }
        PlayerInfoText.text = "" + PlayerSlider.value;
        BotInfoText.text = "" + botval;
    }
}
