using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionDelay = 1f;
    public float explosionRate = 1f;
    public float explosionMaxSize = 10f;
    public float explosionSpeed = 1f;
    public float currentRadius = 0f;
    CircleCollider2D explosionRadius;
    [SerializeField] bool exoloded = false;
    public GameObject resetCanvas;

    // Start is called before the first frame update
    void Start()
    {
        explosionRadius = gameObject.GetComponent<CircleCollider2D>();
        resetCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        explosionDelay -= Time.deltaTime;
        if (explosionDelay<=0)
        {
            exoloded = true;
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if(exoloded)
        {
            if(currentRadius<explosionMaxSize)
            {
                currentRadius += explosionRate;
            }
            else
            {
                Object.Destroy(this.gameObject);
            }
            explosionRadius.radius = currentRadius;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            exoloded = true;
            GameManager.instance.isPlayerAlive = false;
            if (!GameManager.instance.isPlayerAlive)
            {
                resetCanvas.SetActive(true);
            }
            //Destroy(collision.gameObject);

        }
    }
}
