using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtManager : MonoBehaviour
{
    int lastLvl = PlayerPrefs.GetInt("lvl");
    [SerializeField] Button[] Buttons;
    [SerializeField] Text[] texts;

    void Start()
    {
        for (int i = 1; i < Buttons.Length + 1; i++)
        {
            if (i < lastLvl)
            {
                Buttons[i - 1].enabled = false;
                texts[i - 1].text = $"{i} Locked";
            }

        }
    }
}
