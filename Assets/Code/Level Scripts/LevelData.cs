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

    [SerializeField]
    private float rankD;

    public float RankD
    {
        get
        {
            return rankD;
        }
    }
    
}
