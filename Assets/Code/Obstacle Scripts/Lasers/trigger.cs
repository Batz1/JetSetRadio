using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class trigger : MonoBehaviour
{
    public GameObject LaserFolder;
    public GameObject canvas;

    public TextMeshProUGUI text;

    public float lTime;
    public float realLtime;
    [SerializeField] float startToFlicker = 1.5f;
    public bool isPressed = false;

    public trigger[] t;

    // Start is called before the first frame update
    void Start()
    {
        realLtime = 0;
        t = new trigger[GameManager.instance.switches.Length];
        for(int i = 0; i < GameManager.instance.switches.Length; i++)
        {
            t[i] = GameManager.instance.switches[i].GetComponent<trigger>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPressed)
        {
            realLtime -= Time.deltaTime;
            canvas.SetActive(true);
        }
        else if (!isPressed)
        {
            realLtime = lTime;
            canvas.SetActive(false);
        }

        text.text = realLtime.ToString("F2");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!isPressed)
            {
                LaserFolder.SetActive(false);
                StartCoroutine(startLaser());
                isPressed = true;
                SetAllOtherTriggers();
            }
            else
            {
                StopCoroutine(startLaser());
                realLtime = lTime;
                StartCoroutine(startLaser());
            }
        }
    }

    IEnumerator startLaser()
    {
        while(realLtime > 0)
        {
            yield return null;
            if(startToFlicker>realLtime)
            {
                LaserFolder.SetActive(!LaserFolder.activeInHierarchy);
                SetAllCollidersStatus(false);
            }
        }

        LaserFolder.SetActive(true);
        SetAllCollidersStatus(true);
        Debug.Log("Alarm is active");
        isPressed = false;
    }

    public void SetAllCollidersStatus(bool active)
    {
        foreach (Collider2D c in LaserFolder.GetComponentsInChildren<Collider2D>(true))
        {
            c.enabled = active;
        }
    }

    public void SetAllOtherTriggers()
    {
        for(int i = 0; i < t.Length; i++)
        {
            if(t[i] != this)
            {
                t[i].isPressed = false;
            }
        }
    }
}
