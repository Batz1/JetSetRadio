using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGoal : MonoBehaviour
{
    public GameObject endCanvas;
    public GameObject resetCanvas;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI tsecText;

    bool hasEnteredGoal = false;

    // Start is called before the first frame update
    void Start()
    {
        endCanvas.SetActive(false);
        resetCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasEnteredGoal && GameManager.instance.isPlayerAlive)
        {
            GameManager.instance.hasFinishdLevel = true;
            Time.timeScale = 0.0001f;
            endCanvas.SetActive(true);
            timeText.text = GameManager.instance.timeManager.text.text;
            tsecText.text = GameManager.instance.timeManager.stepText.text;
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
}
