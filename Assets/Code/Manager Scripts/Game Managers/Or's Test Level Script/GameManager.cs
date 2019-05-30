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
    public bool isPlayerAlive;

    [HideInInspector] public GameObject[] allProjectiles;
    public int projectileCount;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        CountProjectiles();

        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CountProjectiles()
    {
        allProjectiles = GameObject.FindGameObjectsWithTag("enemyProjectile");

        projectileCount = allProjectiles.Length;
    }
}
