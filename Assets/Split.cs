using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    EnemySplit enemySplit;
    void Start()
    {
        enemySplit = FindObjectOfType<EnemySplit>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            enemySplit.SplitEnemy();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(enemySplit.gameObject);
        }
    }
}
