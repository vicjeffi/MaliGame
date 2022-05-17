using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void LoadSchene(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void ResetSchene()
    {
        PlayerPrefs.SetInt("lvl", 0);
    }
}
