using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] PlayerMover pm;
    public float startTime = 10f;
    [SerializeField] bool isGoing = false;
    public bool isEnded = false;
    string[] randomText = { "Go faster! ", "Faster! ", "Faster!!", "Faster!!!" };
    public float endTime = 0f;

    string mainText = "Score: ";
    // Start is called before the first frame update
    void Start()
    {
        endTime = startTime;
        StartCoroutine(BestUbdate());
    }

    // Update is called once per frame
    IEnumerator BestUbdate()
    {
        while (endTime >= 0)
        {
            if (endTime <= 10 && pm.canPlay)
                mainText = randomText[UnityEngine.Random.Range(0, randomText.Length - 1)];
            text.text = mainText + Convert.ToString(endTime);
            if (isGoing && pm.canPlay)
                robScore(1f);
            yield return new WaitForSeconds(1f);
        }

        isGoing = false;
        isEnded = true;
        endTime = 0;
        pm.PlayerDie(false);
    }

    public void StartTimer()
    {
        isGoing = true;
        isEnded = false;
    }

    public void addScore(float addScore)
    {
        endTime += addScore;
    }

    public void robScore(float robScore) // ограбить счетик
    {
        endTime -= robScore;
    }
}
