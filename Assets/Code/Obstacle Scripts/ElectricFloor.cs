using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFloor : MonoBehaviour
{
    public ParticleSystem electricP;

    // Start is called before the first frame update
    void Start()
    {
        electricP.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        electricP.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        electricP.Stop();
    }
}
