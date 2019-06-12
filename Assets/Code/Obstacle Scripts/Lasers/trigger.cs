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
    float realLtime;
    bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        realLtime = 0;
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
                Debug.Log("Quick! the lasers are down!");
                StartCoroutine(startLaser());
                isPressed = true;
            }
            else
            {
                Debug.Log("Wait a few seconds dumbass");
            }
        }
    }

    IEnumerator startLaser()
    {
        yield return new WaitForSeconds(lTime);
        LaserFolder.SetActive(true);
        Debug.Log("Alarm is active");
        isPressed = false;
    }
    
}
