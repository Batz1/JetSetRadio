using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLetters : MonoBehaviour
{
    public float letterDT;
    public GameObject[] Letter; 
    //public GameObject Letter1;
   // public GameObject Letter2;

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
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
