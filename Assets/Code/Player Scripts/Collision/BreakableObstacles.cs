using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObstacles : MonoBehaviour
{

    [SerializeField] int maxObstaclesHits;
    [SerializeField] int timesObstaclesHit;   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesObstaclesHit++;
        if (timesObstaclesHit >= maxObstaclesHits)
        {
            Destroy(gameObject);
        }
    }

}
