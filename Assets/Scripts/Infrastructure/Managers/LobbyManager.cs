using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    /// <summary>
    /// Представление игрока в виде текста с выводом инфы
    /// </summary>
    public Text[] PlayerSlot = new Text[4];
    /// <summary>
    /// Preset, который закрепляется за игроком
    /// </summary>
    private bool[] PlayerPreset = new bool[4];
    /// <summary>
    /// Preset из Input Unity
    /// </summary>
    private bool[] isPresetAvailable = new bool[6];
    private int n = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Инициализируем
        for (int i = 0; i < 4; i++) PlayerPreset[i] = true;
        for (int i = 0; i < 6; i++) isPresetAvailable[i] = true;
    }

    void FixedUpdate()
    {
        //Если не все слоты заполнены
        if (n < 4)
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 4; j++)
                {
                    //
                    if ((PlayerPreset[j] == true) && (isPresetAvailable[i] == true) && (Input.GetAxis($"{i}Preset") != 0))
                    {
                        PlayerSlot[j].text = (i+1) + " Preset";
                        isPresetAvailable[i] = false;
                        PlayerPreset[j] = false;
                        n++;
                    }
                }
        }
    }
}
