using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyScript : MonoBehaviour
{

    [SerializeField] PlayerController playerControl;
    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] float velocityX;
    [SerializeField] float enemyVelocityX;
    [SerializeField] float _distance;
    private Rigidbody2D enemyRB;
    int enemyVelocityY = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandleHit();
        }
    }
    
    void Update()
    {
        //Debug.Log(enemyRB.velocity);
    }

    private void HandleHit()
    {
        if (playerControl.xVelocityCheck >= velocityX)
        {
            enemyRB.AddForce(new Vector2(enemyVelocityX, enemyVelocityY)); 
                if (Vector2.Distance(playerControl.transform.position,enemyRB.transform.position)>=_distance)
                {
                    enemyRB.constraints = RigidbodyConstraints2D.FreezePositionX;
                }
        }
   
        if (playerControl.xVelocityCheck < velocityX)
        {
            enemyRB.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
}
