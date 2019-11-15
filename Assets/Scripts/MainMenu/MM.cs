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

    }

    // Update is called once per frame
    void Update()
    {
        BotSlider.maxValue = 4 - PlayerSlider.value;
        PlayerInfoText.text = "Количество игроков " + PlayerSlider.value;
        BotInfoText.text = "Количество ботов " + BotSlider.value;
    }
}
