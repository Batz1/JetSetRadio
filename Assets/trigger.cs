using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour
{
    public GameObject[] Laser;
    public float lTime;

    // Start is called before the first frame update
    void Start()
    {
        Laser = GameObject.FindGameObjectsWithTag("Laser");    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(GameObject laser in Laser)
            {
                laser.SetActive(false);
            }
            Debug.Log("Quick! You can now escape!");
            StartCoroutine(startLaser());
          
        }
    }

    IEnumerator startLaser()
    {
        yield return new WaitForSeconds(lTime);
        foreach (GameObject laser in Laser)
        {
            laser.SetActive(true);
        }
        Debug.Log("Alarm is active");
    }
    
}
