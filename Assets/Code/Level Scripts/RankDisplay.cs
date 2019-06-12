using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankDisplay : MonoBehaviour
{
    public LevelData levelData;
    TextMeshProUGUI text;
    float timer;
    
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameManager.instance.hasFinishdLevel)
        {
            timer = GameManager.instance.timeManager.timeStep;
            GenerateRank();
        }
    }

    void GenerateRank()
    {
        if (timer <= levelData.RankS)
        {
            text.text = "S";
        }
        else if (timer <= levelData.RankA)
        {
            text.text = "A";
        }
        else if (timer <= levelData.RankB)
        {
            text.text = "B";
        }
        else if (timer <= levelData.RankC)
        {
            text.text = "C";
        }
        else if (timer <= levelData.RankD)
        {
            text.text = "D";
        }
        else
        {
            text.text = "E";
        }
    }

}
