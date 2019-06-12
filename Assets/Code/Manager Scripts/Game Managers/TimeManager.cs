using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public float timeStep;

    public float tSeconds;
    public int seconds;
    public int minutes;

    public int nextSec;
    public float nextTsec;

    public TextMeshProUGUI text;
    public TextMeshProUGUI stepText;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 0;
        minutes = 0;
        nextSec = 1;
        nextTsec = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.hasFinishdLevel  && GameManager.instance.isPlayerAlive)
        {
            timeStep += Time.deltaTime;
        }

        if(timeStep >= nextTsec)
        {
            tSeconds += 0.1f;
            nextTsec += 0.1f;
        }

        if (timeStep >= nextSec)
        {
            seconds++;
            tSeconds = 0;
            nextSec++;
        }

        if (seconds >= 60)
        {
            minutes++;
            seconds = 0;
        }

        if (seconds < 10)
        {
            text.text = minutes + ":0" + seconds;
        }
        else
        {
            text.text = minutes + ":" + seconds;
        }

        stepText.text = (tSeconds * 10).ToString("f0");
    }
}
