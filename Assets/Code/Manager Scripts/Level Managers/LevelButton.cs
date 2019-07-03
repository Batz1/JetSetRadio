using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public LevelData levelData;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI rankText;

    string levelName;
    string levelRank;
    int currentRank;
    int sceneNumber;

    void Awake()
    {
        levelName = levelData.LevelName;
        currentRank = levelData.CurrentRank;
        sceneNumber = levelData.SceneNumber;
    }

    void Start()
    {
        SetText();
    }

    public void SetText()
    {
        nameText.text = levelName;

        SetRank();

        rankText.text = levelRank;
    }

    public void SetRank()
    {
        if (currentRank == 1)
        {
            levelRank = "S";
        }
        else if (currentRank == 2)
        {
            levelRank = "A";
        }
        else if (currentRank == 3)
        {
            levelRank = "B";
        }
        else if (currentRank == 4)
        {
            levelRank = "C";
        }
        else
        {
            levelRank = "NO";
        }
    }

    public void Select()
    {
        SceneManager.LoadSceneAsync(sceneNumber);
    }
}
