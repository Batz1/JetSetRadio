using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntro : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject winCam;

    public float waitDuration;
    public float waitDuration2;
    public TimeManager timer;
    PlayerController pc;

    public static bool LevelHasStarted;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameManager.instance.currentPlayer.GetComponent<PlayerController>();

        if (!LevelHasStarted)
        {
            StartCoroutine(CameraTransition(waitDuration, waitDuration2));
        }
        else
        {
            playerCam.SetActive(true);
            pc.enabled = true;
            timer.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.hasFinishdLevel)
        {
            LevelHasStarted = false;
        }
    }

    public IEnumerator CameraTransition(float duration, float anotherDuration)
    {
        pc.enabled = false;
        timer.enabled = false;
        LevelHasStarted = true;
        Time.timeScale = 0.0001f;
        yield return new WaitForSecondsRealtime(duration);
        playerCam.SetActive(true);
        yield return new WaitForSecondsRealtime(anotherDuration);
        timer.enabled = true;
        pc.enabled = true;
        Time.timeScale = 1;
    }

    IEnumerator WinTransition(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        winCam.SetActive(true);
    }
}
