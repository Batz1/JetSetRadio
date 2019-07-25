using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObstacles : MonoBehaviour
{

    Rigidbody2D playerRB2d;
    [SerializeField] private ParticleSystem ps;
    public float velocityX;
    public float velocityY;


    // Start is called before the first frame update
    void Start()
    {
        playerRB2d = GameManager.instance.currentPlayer.GetComponent<Rigidbody2D>();
        ps = GetComponent<ParticleSystem>();
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
        if ((playerRB2d.velocity.x >= velocityX || playerRB2d.velocity.x <= -velocityX) || (playerRB2d.velocity.y >= velocityY || playerRB2d.velocity.y <= -velocityY))
        {
            Destroy(gameObject);
        }
        else
        {
            ps.Emit(2);
        }
    }
}
