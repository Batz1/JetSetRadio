using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    EnemySplit enemySplit;
    [SerializeField] GameObject normalGrambot;

    
    void Start()
    {
        enemySplit = GetComponentInParent<EnemySplit>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemySplit.SplitEnemy();
            Destroy(normalGrambot.gameObject, 0.2f);
        }
    }
}