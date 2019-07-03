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

            if (levelData.CurrentRank > 1 || levelData.CurrentRank == 0)
            {
                levelData.CurrentRank = 1;
            }
        }
        else if (timer <= levelData.RankA )
        {
            text.text = "A";

            if (levelData.CurrentRank > 2 || levelData.CurrentRank == 0)
            {
                levelData.CurrentRank = 2;
            }
        }
        else if (timer <= levelData.RankB)
        {
            text.text = "B";

            if (levelData.CurrentRank > 3 || levelData.CurrentRank == 0)
            {
                levelData.CurrentRank = 3;
            }
        }
        else
        {
            text.text = "C";

            if (levelData.CurrentRank > 4 || levelData.CurrentRank == 0)
            {
                levelData.CurrentRank = 4;
            }
        }

        
    }

}
