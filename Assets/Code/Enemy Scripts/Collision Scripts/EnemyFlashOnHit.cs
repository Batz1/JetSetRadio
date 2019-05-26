using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlashOnHit : MonoBehaviour
{

    [SerializeField] int health;

    private Material matWhite;
    private Material matDefault;
    MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        matWhite = Resources.Load("WhiteFlash", typeof(Material)) as Material; // when the player gets hit it will flash in white color
        matDefault = mr.material; // default color
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            health--;
            mr.material = matWhite;
            if(health<=0)
            {
                KillEnemy();
            }
            else
            {
                Invoke("ResetMaterial", .1f);
            }
        }
    }

    void ResetMaterial()
    {
        mr.material = matDefault;
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
