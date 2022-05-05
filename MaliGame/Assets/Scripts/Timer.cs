using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    

    public float startTime = 10f;
    bool isGoing = false;
    public bool isEnded = false;
    float endTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        endTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoing)
            endTime -= 1 * Time.deltaTime;

        if(endTime <= 0)
        {
            isGoing = false;
            isEnded = true;
            endTime = 0;
        }
    }

    public void StartTimer()
    {
        isGoing = true;
        isEnded = false;
    }
}
