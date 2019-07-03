using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "levelRank")]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private string levelName;

    public string LevelName
    {
        get
        {
            return levelName;
        }
    }

    [SerializeField]
    private int sceneNumber;

    public int SceneNumber
    {
        get
        {
            return sceneNumber;
        }
    }

    [SerializeField]
    private string levelRankKey;

    public string LevelRankKey
    {
        get
        {
            return levelRankKey;
        }
    }

    [SerializeField]
    private int currentRank;

    public int CurrentRank
    {
        get
        {
            return PlayerPrefs.GetInt(LevelRankKey);
        }
        set
        {
            PlayerPrefs.SetInt(LevelRankKey, value);
        }
    }

    [SerializeField]
    private float rankS;
    
    public float RankS
    {
        get
        {
            return rankS;
        }
    }

    [SerializeField]
    private float rankA;

    public float RankA
    {
        get
        {
            return rankA;
        }
    }

    [SerializeField]
    private float rankB;

    public float RankB
    {
        get
        {
            return rankB;
        }
    }

    [SerializeField]
    private float rankC;

    public float RankC
    {
        get
        {
            return rankC;
        }
    }

}
