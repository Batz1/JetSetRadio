using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    public GameObject currentPlayer;
    public PlayerHitDetection phd;
    public TimeManager timeManager;

    public bool isPlayerAlive;
    public bool hasFinishdLevel;

    public string nameNextLevel = "First Playable Level 2";
    public int nextLevelIndex = 2;

    // Start is called before the first frame update
    void Start()
    {
        hasFinishdLevel = false;
        isPlayerAlive = true;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("reset"))
        {
            ResetScene();
            isPlayerAlive = true;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinLevel() // unlock next level from the level selector
    {
        PlayerPrefs.SetInt("levelReached", nextLevelIndex);
    }
}
