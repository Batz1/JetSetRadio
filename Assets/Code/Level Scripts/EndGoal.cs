using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndGoal : MonoBehaviour
{
    public GameObject endCanvas;
    public GameObject resetCanvas;
    public FadeScript fade;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI tsecText;

    bool hasEnteredGoal = false;
    public bool isLocked = false;
    public int levelIndex;

    // Start is called before the first frame update
    void Start()
    {
        endCanvas.SetActive(false);
        resetCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasEnteredGoal && GameManager.instance.isPlayerAlive && !isLocked)
        {
            GameManager.instance.hasFinishdLevel = true;
            Time.timeScale = 0.0001f;
            endCanvas.SetActive(true);
            timeText.text = GameManager.instance.timeManager.text.text;
            tsecText.text = GameManager.instance.timeManager.stepText.text;
            GameManager.instance.WinLevel();

            if (Input.GetButtonDown("Jump"))
            {
                FadeToLevel(levelIndex);
                Time.timeScale = 1f;
            }
        }

        if (!GameManager.instance.isPlayerAlive)
        {
            resetCanvas.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasEnteredGoal = true;
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        fade.levelToLoad = levelIndex;
        fade.animator.SetTrigger("FadeOut");
    }
}
