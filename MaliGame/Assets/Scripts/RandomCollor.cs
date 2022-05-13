using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class RandomCollor : MonoBehaviour
{
    Color MyColorNow;
    void Start()
    {
        SetRandomCollor(this.gameObject);
    }
    public void SetRandomCollor(GameObject obg)
    {
        MyColorNow = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1.0f);
        obg.GetComponent<Renderer>().material.color = MyColorNow;
    }

    public void SetColor(Color color, GameObject obg)
    {
        obg.GetComponent<Renderer>().material.color = color;
    }
}
