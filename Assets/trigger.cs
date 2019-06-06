using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public GameObject LaserFolder;
    public float lTime;
    bool isPressed = false;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
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
