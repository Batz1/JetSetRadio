using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObstacles : MonoBehaviour
{

    //[SerializeField] int maxObstaclesHits;
    //[SerializeField] int timesObstaclesHit;
    Rigidbody2D playerRB2d;
    [SerializeField] float velocityX;
    [SerializeField] float velocityY;


    // Start is called before the first frame update
    void Start()
    {
        playerRB2d = GameManager.instance.currentPlayer.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        if (playerRB2d.velocity.x >= velocityX || playerRB2d.velocity.x <= -velocityX)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log(playerRB2d.velocity.x);
        }
    }
}
