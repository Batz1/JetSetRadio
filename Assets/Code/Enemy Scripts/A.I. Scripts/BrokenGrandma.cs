using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGrandma : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public GameObject resetCanvas;

    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        resetCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if(speed > 0)
        {
            speed += 4 * Time.deltaTime;
        }
        else
        {
            speed -= 4 * Time.deltaTime;
        }
        
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Grandma"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                GameManager.instance.phd.GetHit(collision.collider);
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.1f);
            }
            else if (!collision.gameObject.CompareTag("Floor"))
            {
                Instantiate(Explosion, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.1f);
            }
        }
    }
}
