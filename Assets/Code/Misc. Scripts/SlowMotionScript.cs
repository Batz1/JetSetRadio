using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionScript : MonoBehaviour
{
    public float subtractPerFrame;
    public float timeLimit;

    public bool canTrigger = true;
    public bool isTriggered = false;
    public bool returnToNormal = false;

    bool canSpawnAltMovement = false;
    TutorialMovement altMovement;

    public bool wantToDash = false;
    public bool wantToMove = false;

    public string actionButton;
    public string axisName;
    public KeyCode[] dashKeys;

    public GameObject Button1;

    public bool onlyJump;
    public bool onlyDoubleJump;
    public bool onlyDash;
    public bool onlyStomp;

    SlowMotionScript[] smScripts;

    // Start is called before the first frame update
    void Start()
    {
        altMovement = GameManager.instance.currentPlayer.GetComponent<TutorialMovement>();
        smScripts = new SlowMotionScript[GameManager.instance.slowMotionTriggers.Length];

        for (int i = 0; i < GameManager.instance.slowMotionTriggers.Length; i++)
        {
            smScripts[i] = GameManager.instance.slowMotionTriggers[i].GetComponent<SlowMotionScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && canTrigger)
        {
            SlowTime();

            if (Input.GetButtonDown(actionButton))
            {
                ReturnTime();
            }
            else if (!GameManager.instance.isPlayerAlive)
            {
                ReturnTime();
            }
            else if (wantToDash)
            {
                if (Input.GetKeyDown(dashKeys[0]) || Input.GetKeyDown(dashKeys[1]))
                {
                    ReturnTime();
                }
            }
            else if (wantToMove)
            {
                if (Input.GetAxisRaw(axisName) != 0)
                {
                    ReturnTime();
                }
            }
        }

    }

    void SlowTime()
    {
        if (Time.timeScale > timeLimit)
        {
            Time.timeScale -= subtractPerFrame;
        }
        else
        {
            Time.timeScale = timeLimit;

            if (Button1 != null)
            {
                Button1.SetActive(true);
            }

        }

        SetAllOtherTriggers();
    }

    void ReturnTime()
    {
        Time.timeScale = 1;

        canTrigger = false;

        if (Button1 != null)
        {
            Button1.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true;
        }
    }

    void LockMovement()
    {
        altMovement.enableLock = true;

        if (onlyJump)
        {
            altMovement.toJump = true;
        }
        else if (onlyDoubleJump)
        {
            altMovement.toDoubleJump = true;
        }
        else if (onlyDash)
        {
            altMovement.toDash = true;
        }
        else if (onlyStomp)
        {
            altMovement.toStomp = true;
        }
    }

    public void SetAllOtherTriggers()
    {
        for (int i = 0; i < smScripts.Length; i++)
        {
            if (smScripts[i] != this)
            {
                smScripts[i].isTriggered = false;
                smScripts[i].Button1.SetActive(false);
            }
        }
    }

}
